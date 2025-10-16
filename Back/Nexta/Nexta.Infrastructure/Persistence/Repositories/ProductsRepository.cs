using Nexta.Domain.Abstractions.Repositories;
using Nexta.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Nexta.Domain.Filters.Products;
using Nexta.Domain.Exceptions;
using AutoMapper;
using Nexta.Infrastructure.Persistence;
using Nexta.Infrastructure.Persistence.Entities;
using Nexta.Domain.Base;
using Nexta.Domain.Models.Product;

namespace Nexta.Infrastructure.Persistence.Repositories
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly IDbContextFactory<MainContext> _dbContextFactory;
        private readonly IMapper _mapper;

        public ProductsRepository(IDbContextFactory<MainContext> dbContextFactory, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
        }

        public async Task<Product?> GetAsync(Guid id, CancellationToken ct = default)
        {
            await using (var context = await _dbContextFactory.CreateDbContextAsync(ct))
            {
                var productEntity = await context.Products.AsNoTracking()
                    .Include(p => p.Attributes)
                    .Include(p => p.Image)
                    .Include(p => p.BasketProducts)
                    .FirstOrDefaultAsync(p => p.Id == id, ct);

                var product = _mapper.Map<Product>(productEntity);

                return product;
            }
        }

        public async Task<PagedData<Product>> GetAllAsync(GetProductsFilter filter, CancellationToken ct = default)
        {
            await using (var context = await _dbContextFactory.CreateDbContextAsync(ct))
            {
                var searchTerm = filter.SearchTerm.ToLower() ?? "";
                var category = filter.Category.ToLower() ?? "";

                var query = context.Products
                    .Include(p => p.Attributes)
                    .Include(p => p.Image)
                    .WithOrderByTerm(category)
                    .WithCategoryTerm(category)
                    .WithSearchTerm(searchTerm)
                    .WithPriceTerm(filter.MinPrice, filter.MaxPrice)
                    .Where(p => p.IsVisible || filter.WithHidden)
                    .AsNoTracking();

                var productEntities = await query
                    .Skip((filter.PageNumber - 1) * filter.PageSize)
                    .Take(filter.PageSize)
                    .ToListAsync(ct);

                var countProducts = await query.CountAsync(ct);
                var pageCount = (int)Math.Ceiling((double)countProducts / filter.PageSize);

                var pagedProductEntities = new PagedData<ProductEntity>(productEntities, productEntities.Count, pageCount);

                var result = _mapper.Map<PagedData<Product>>(pagedProductEntities);

                return result;
            }
        }

        public async Task<List<Product>> GetRangeAsync(List<Guid> productIds, CancellationToken ct = default)
        {
            await using (var context = await _dbContextFactory.CreateDbContextAsync(ct))
            {
                var productEntities = await context.Products
                    .AsNoTracking()
                    .Where(p => productIds.Contains(p.Id))
                    .ToListAsync(ct);

                var products = _mapper.Map<List<Product>>(productEntities);

                return products;
            }
        }

        public async Task<Guid> CreateAsync(Product product, CancellationToken ct = default)
        {
            await using (var context = await _dbContextFactory.CreateDbContextAsync(ct))
            {
                var productEntity = _mapper.Map<ProductEntity>(product);

                var result = await context.Products.AddAsync(productEntity, ct);
                await context.SaveChangesAsync(ct);

                return result.Entity.Id;
            }
        }

        public async Task<List<Product>> GetBasketProductsAsync(GetBasketProductsFilter filter, CancellationToken ct = default)
        {
            await using (var context = await _dbContextFactory.CreateDbContextAsync(ct))
            {
                var productEntities = await context.Products
                    .AsNoTracking()
                    .Where(p => p.BasketProducts.Any(up => up.UserId == filter.UserId))
                    .Include(p => p.BasketProducts.Where(up => up.UserId == filter.UserId))
                    .ToListAsync(ct);

                var products = _mapper.Map<List<Product>>(productEntities);

                return products;
            }
        }

        public async Task<Product> UpdateAsync(Product product, CancellationToken ct = default)
        {
            await using (var context = await _dbContextFactory.CreateDbContextAsync(ct))
            {
                var productEntity = await context.Products
                    .Include(p => p.Attributes)
                    .Include(p => p.Image)
                    .FirstOrDefaultAsync(d => d.Id == product.Id, ct);

                if (productEntity == null)
                    throw new NotFoundException("Деталь не найдена");

                productEntity.Name = product.Name ?? productEntity.Name;
                productEntity.Article = product.Article ?? productEntity.Article;
                productEntity.Description = product.Description ?? productEntity.Description;
                productEntity.Status = product.Status;
                productEntity.Count = product.Count;
                productEntity.NewPrice = product.NewPrice;
                productEntity.OldPrice = product.OldPrice;
                productEntity.IsVisible = product.IsVisible;
                productEntity.Category = product.Category;

                if (product.Attributes != null)
                    productEntity.Attributes = CreateProductAttributeEntities(product);

                if (DateOnly.TryParse(product.OrderDate, out var orderDate))
                    productEntity.OrderDate = orderDate;

                if (DateOnly.TryParse(product.DeliveryDate, out var deliveryDate))
                    productEntity.DeliveryDate = deliveryDate;

                await context.SaveChangesAsync(ct);

                return _mapper.Map<Product>(productEntity);
            }
        }
        private List<ProductAttributeEntity> CreateProductAttributeEntities(Product product)
        {
            var result = new List<ProductAttributeEntity>();

            foreach (var attribute in product.Attributes)
            {
                var attributeEntity = new ProductAttributeEntity
                {
                    Key = attribute.Key,
                    Value = attribute.Value
                };

                result.Add(attributeEntity);
            }
            return result;
        }
    }
}
