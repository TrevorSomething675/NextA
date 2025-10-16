using System.Reflection;
using System.Text.Json.Serialization;
using FluentValidation;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Options;
using Nexta.Application;
using Nexta.Application.Services;
using Nexta.Domain.Abstractions.Services;
using Nexta.Domain.Options;
using Nexta.Infrastructure.Persistence;
using Nexta.Infrastructure.Services;
using Nexta.Web.Extensions;
using Nexta.Web.Middlewares;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services.AddAppOptions(builder.Configuration);
services.AddAppMapper();
services.AddAppRepositories();
services.AddAppAuth(builder.Configuration);
services.AddMediatR(config => config.RegisterServicesFromAssemblies(Assembly.GetAssembly(typeof(AssemblyMarker))!));
services.AddScoped<IHashService, PasswordHashService>();
services.AddScoped<IJwtTokenService, JwtTokenService>();
services.AddScoped<IEmailService, EmailService>();
services.AddScoped<IVerificationCodeGenerator, VerificationCodeGenerator>();
services.AddScoped<IVerificationCodeService, VerificationCodeService>();
services.AddMemoryCache();

services.AddValidatorsFromAssembly(Assembly.GetAssembly(typeof(AssemblyMarker))!);

services.AddRouting();
services.AddControllers();
services.Configure<JsonOptions>(options =>
{
	options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

services.AddDbContextFactory<MainContext>((serviceProvider, optionsBuilder) =>
{
    var dbOptions = serviceProvider.GetRequiredService<IOptions<DataBaseOptions>>().Value;

    optionsBuilder.UseNpgsql(dbOptions.ConnectionString)
        .EnableSensitiveDataLogging()
        .EnableDetailedErrors()
        .LogTo(
            Console.WriteLine,
            new[] {
                DbLoggerCategory.Database.Command.Name,
                DbLoggerCategory.Database.Transaction.Name,
                DbLoggerCategory.Update.Name
            },
            LogLevel.Error,
            DbContextLoggerOptions.DefaultWithLocalTime |
            DbContextLoggerOptions.SingleLine);
});

using (var context = services.BuildServiceProvider().GetRequiredService<MainContext>())
{
    context.Database.EnsureCreated();
	if(context.Users.FirstOrDefault() == null)
	{
		context.Database.EnsureDeleted();
		context.Database.EnsureCreated();
		if (!context.Users.Any())
		{
			var user = new UserEntity
			{
				Email = "Test1@mail.ru",
				FirstName = "TestFName1",
				LastName = "TestLName1",
				MiddleName = "TestMName1",
				PasswordHash = "123123123Qq",
			};
			var detail = new ProductEntity
			{
				Name = "�������� �����",
				Article = "A000989210713MBR",
				Description = "����� �������� 229.3/229",
				Status = Nexta.Domain.Enums.ProductStatus.InStock,
				OrderDate = DateOnly.FromDateTime(DateTime.UtcNow),
				DeliveryDate = DateOnly.FromDateTime(DateTime.UtcNow),
				Count = 1,
				NewPrice = 12091,
				IsVisible = true
			};
            var detail1 = new ProductEntity
            {
                Name = "�������� �����-1",
                Article = "A000989210713MBR-1",
                Description = "����� �������� 229.3/229-1",
                Status = Nexta.Domain.Enums.ProductStatus.InStock,
                OrderDate = DateOnly.FromDateTime(DateTime.UtcNow),
                DeliveryDate = DateOnly.FromDateTime(DateTime.UtcNow),
                Count = 1,
                NewPrice = 12091,
                IsVisible = true
            };
            var detail2 = new ProductEntity
            {
				Name = "�������� �����",
				Article = "A000989220713MBR",
				Description = "����� �������� 229.51 SAE",
				Status = Nexta.Domain.Enums.ProductStatus.InStock,
				OrderDate = DateOnly.FromDateTime(DateTime.UtcNow),
				DeliveryDate = DateOnly.FromDateTime(DateTime.UtcNow),
				Count = 3,
				NewPrice = 12264,
				IsVisible = true
			};
            var detail21 = new ProductEntity
            {
                Name = "�������� �����-1",
                Article = "A000989220713MBR-1",
                Description = "����� �������� 229.51 SAE-1",
                Status = Nexta.Domain.Enums.ProductStatus.InStock,
                OrderDate = DateOnly.FromDateTime(DateTime.UtcNow),
                DeliveryDate = DateOnly.FromDateTime(DateTime.UtcNow),
                Count = 3,
                NewPrice = 12264,
                IsVisible = true
            };
            var detail3 = new ProductEntity
            {
				Name = "������� ������",
				Article = "A1666705400MB",
				Description = "������ �������� �������� ��� Mercedes-Benz GLE",
				Status = Nexta.Domain.Enums.ProductStatus.InStock,
				OrderDate = DateOnly.FromDateTime(DateTime.UtcNow),
				DeliveryDate = DateOnly.FromDateTime(DateTime.UtcNow),
				Count = 1,
				NewPrice = 81296,
				IsVisible = true
			};
            var detail31 = new ProductEntity
            {
                Name = "������� ������-1",
                Article = "A1666705400MB-1",
                Description = "������ �������� �������� ��� Mercedes-Benz GLE-1",
                Status = Nexta.Domain.Enums.ProductStatus.InStock,
                OrderDate = DateOnly.FromDateTime(DateTime.UtcNow),
                DeliveryDate = DateOnly.FromDateTime(DateTime.UtcNow),
                Count = 1,
                NewPrice = 81296,
                IsVisible = true
            };
            var detail4 = new ProductEntity
            {
				Name = "����� ���������",
				Article = "Denso-K16RU11",
				Description = "����� ���������",
				Status = Nexta.Domain.Enums.ProductStatus.InStock,
				OrderDate = DateOnly.FromDateTime(DateTime.UtcNow),
				DeliveryDate = DateOnly.FromDateTime(DateTime.UtcNow),
				Count = 5,
				NewPrice = 218,
				OldPrice = 320,
				IsVisible = true
			};
            var detail41 = new ProductEntity
            {
                Name = "����� ���������-1",
                Article = "Denso-K16RU11-1",
                Description = "����� ���������-1",
                Status = Nexta.Domain.Enums.ProductStatus.InStock,
                OrderDate = DateOnly.FromDateTime(DateTime.UtcNow),
                DeliveryDate = DateOnly.FromDateTime(DateTime.UtcNow),
                Count = 5,
                NewPrice = 218,
                OldPrice = 320,
                IsVisible = true
            };
            var detail5 = new ProductEntity
            {
				Name = "�������� ������",
				Article = "2630035505",
				Description = "������ ��������",
				Status = Nexta.Domain.Enums.ProductStatus.OutOfStock,
				OrderDate = DateOnly.FromDateTime(DateTime.UtcNow),
				DeliveryDate = DateOnly.FromDateTime(DateTime.UtcNow),
				Count = 2,
				NewPrice = 507,
				IsVisible = true
			};
            var detail51 = new ProductEntity
            {
                Name = "�������� ������-1",
                Article = "2630035505-1",
                Description = "������ ��������-1",
                Status = Nexta.Domain.Enums.ProductStatus.OutOfStock,
                OrderDate = DateOnly.FromDateTime(DateTime.UtcNow),
                DeliveryDate = DateOnly.FromDateTime(DateTime.UtcNow),
                Count = 2,
                NewPrice = 507,
                IsVisible = true
            };
            var detail6 = new ProductEntity
            {
				Name = "���������� ���������",
				Article = "Grass 116100",
				Description = "���������� ��������� Motor Cleaner",
				Status = Nexta.Domain.Enums.ProductStatus.OutOfStock,
				OrderDate = DateOnly.FromDateTime(DateTime.UtcNow),
				DeliveryDate = DateOnly.FromDateTime(DateTime.UtcNow),
				Count = 1,
				NewPrice = 359,
				IsVisible = true
			};
            var detail61 = new ProductEntity
            {
                Name = "���������� ���������-1",
                Article = "Grass 116100-1",
                Description = "���������� ��������� Motor Cleaner-1",
                Status = Nexta.Domain.Enums.ProductStatus.OutOfStock,
                OrderDate = DateOnly.FromDateTime(DateTime.UtcNow),
                DeliveryDate = DateOnly.FromDateTime(DateTime.UtcNow),
                Count = 1,
                NewPrice = 359,
                IsVisible = true
            };
            var detail7 = new ProductEntity
            {
				Name = "��������",
				Article = "703141410",
				Description = "��������-��������� Reinzosil ������� ����� �������",
				Status = Nexta.Domain.Enums.ProductStatus.InStock,
				OrderDate = DateOnly.FromDateTime(DateTime.UtcNow),
				DeliveryDate = DateOnly.FromDateTime(DateTime.UtcNow),
				Count = 1,
				NewPrice = 718,
				IsVisible = true
			};
            var detail71 = new ProductEntity
            {
                Name = "��������-1",
                Article = "703141410-1",
                Description = "��������-��������� Reinzosil ������� ����� �������-1",
                Status = Nexta.Domain.Enums.ProductStatus.InStock,
                OrderDate = DateOnly.FromDateTime(DateTime.UtcNow),
                DeliveryDate = DateOnly.FromDateTime(DateTime.UtcNow),
                Count = 1,
                NewPrice = 718,
                IsVisible = true
            };
            var detail8 = new ProductEntity
            {
				Name = "����",
				Article = "526111",
				Description = "���� ������ ������������ �������� 175/65R14 82T",
				Status = Nexta.Domain.Enums.ProductStatus.InStock,
				OrderDate = DateOnly.FromDateTime(DateTime.UtcNow),
				DeliveryDate = DateOnly.FromDateTime(DateTime.UtcNow),
				Count = 2,
				NewPrice = 24053,
				OldPrice = 28700,
				IsVisible = true,
                Category = "����"
			};
            var detail81 = new ProductEntity
            {
                Name = "����-1",
                Article = "526111-1",
                Description = "���� ������ ������������ �������� 175/65R14 82T-1",
                Status = Nexta.Domain.Enums.ProductStatus.InStock,
                OrderDate = DateOnly.FromDateTime(DateTime.UtcNow),
                DeliveryDate = DateOnly.FromDateTime(DateTime.UtcNow),
                Count = 2,
                NewPrice = 24053,
                OldPrice = 28700,
                IsVisible = true,
                Category = "����"
            };
            var detail9 = new ProductEntity
            {
				Name = "����",
				Article = "457442784",
				Description = "�������� R13 155/70 Cordiant Road Runner 75T (����)",
				Status = Nexta.Domain.Enums.ProductStatus.InStock,
				OrderDate = DateOnly.FromDateTime(DateTime.UtcNow),
				DeliveryDate = DateOnly.FromDateTime(DateTime.UtcNow),
				Count = 1,
				NewPrice = 5960,
                Category = "����"
            };
            var detail91 = new ProductEntity
            {
                Name = "����-1",
                Article = "457442784-1",
                Description = "�������� R13 155/70 Cordiant Road Runner 75T (����)-1",
                Status = Nexta.Domain.Enums.ProductStatus.InStock,
                OrderDate = DateOnly.FromDateTime(DateTime.UtcNow),
                DeliveryDate = DateOnly.FromDateTime(DateTime.UtcNow),
                Count = 1,
                NewPrice = 5960,
                Category = "����"
            };
            var detail10 = new ProductEntity
            {
				Name = "����",
				Article = "1012050",
				Description = "���� ������ �������� 175/65R14 82H",
				Status = Nexta.Domain.Enums.ProductStatus.InStock,
				OrderDate = DateOnly.FromDateTime(DateTime.UtcNow),
				DeliveryDate = DateOnly.FromDateTime(DateTime.UtcNow),
				Count = 2,
				NewPrice = 9924,
				OldPrice = 11098,
				IsVisible = false,
                Category = "����"
            };
            var detail101 = new ProductEntity
            {
                Name = "����-1",
                Article = "1012050-1",
                Description = "���� ������ �������� 175/65R14 82H-1",
                Status = Nexta.Domain.Enums.ProductStatus.InStock,
                OrderDate = DateOnly.FromDateTime(DateTime.UtcNow),
                DeliveryDate = DateOnly.FromDateTime(DateTime.UtcNow),
                Count = 2,
                NewPrice = 9924,
                OldPrice = 11098,
                IsVisible = false,
                Category = "����"
            };
            var detail11 = new ProductEntity
            {
				Name = "����",
				Article = "1010711",
				Description = "�������� R15 195/60 Hankook Optimo ME02 K424 88H (����)",
				Status = Nexta.Domain.Enums.ProductStatus.InStock,
				OrderDate = DateOnly.FromDateTime(DateTime.UtcNow),
				DeliveryDate = DateOnly.FromDateTime(DateTime.UtcNow),
				Count = 4,
				NewPrice = 9276,
				IsVisible = true,
                Category = "����"
            };
            var detail111 = new ProductEntity
            {
                Name = "����-1",
                Article = "1010711-1",
                Description = "�������� R15 195/60 Hankook Optimo ME02 K424 88H (����)-1",
                Status = Nexta.Domain.Enums.ProductStatus.InStock,
                OrderDate = DateOnly.FromDateTime(DateTime.UtcNow),
                DeliveryDate = DateOnly.FromDateTime(DateTime.UtcNow),
                Count = 4,
                NewPrice = 9276,
                IsVisible = true,
                Category = "����"
            };
            var detail12 = new ProductEntity
            {
				Name = "����������",
				Article = "CA03014S",
				Description = "���������� X1 (30�/���, 7 ���, ����� STANDARD)",
				Status = Nexta.Domain.Enums.ProductStatus.InStock,
				OrderDate = DateOnly.FromDateTime(DateTime.UtcNow),
				DeliveryDate = DateOnly.FromDateTime(DateTime.UtcNow),
				Count = 7,
				NewPrice = 3113,
				IsVisible = true
			};
            var detail121 = new ProductEntity
            {
                Name = "����������-1",
                Article = "CA03014S-1",
                Description = "���������� X1 (30�/���, 7 ���, ����� STANDARD)-1",
                Status = Nexta.Domain.Enums.ProductStatus.InStock,
                OrderDate = DateOnly.FromDateTime(DateTime.UtcNow),
                DeliveryDate = DateOnly.FromDateTime(DateTime.UtcNow),
                Count = 7,
                NewPrice = 3113,
                IsVisible = true
            };
            var detail13 = new ProductEntity
            {
				Name = "��������",
				Article = "522200",
				Description = "�������� ������ ���������� � ��������. ���������� �� �����-������� ����������. �������� " +
				"��������� �������� 10-50 PSI/ 0,5-3,5 ��/��2. ��� ��������� 1 PSI/ 0,1 ��/��2. ������ ��� ������ ��������� " +
				"��������. ���� ��� ������ ������� ��������.",
				Status = Nexta.Domain.Enums.ProductStatus.InStock,
				OrderDate = DateOnly.FromDateTime(DateTime.UtcNow),
				DeliveryDate = DateOnly.FromDateTime(DateTime.UtcNow),
				Count = 2,
				NewPrice = 216,
				IsVisible = true
			};
            var detail131 = new ProductEntity
            {
                Name = "��������-1",
                Article = "522200-1",
                Description = "�������� ������ ���������� � ��������. ���������� �� �����-������� ����������. �������� " +
                "��������� �������� 10-50 PSI/ 0,5-3,5 ��/��2. ��� ��������� 1 PSI/ 0,1 ��/��2. ������ ��� ������ ��������� " +
                "��������. ���� ��� ������ ������� ��������.-1",
                Status = Nexta.Domain.Enums.ProductStatus.InStock,
                OrderDate = DateOnly.FromDateTime(DateTime.UtcNow),
                DeliveryDate = DateOnly.FromDateTime(DateTime.UtcNow),
                Count = 2,
                NewPrice = 216,
                IsVisible = true
            };
            if (context.Users.FirstOrDefault(u => u.Email == "Test1@mail.ru") == null)
			{
				context.Users.Add(user);
				context.Products.AddRange(detail, detail2, detail3, detail4, detail5, detail6, detail7, detail8, detail9, detail10, detail11, detail12, detail13,
                    detail11, detail21, detail31, detail41, detail51, detail61, detail71, detail81, detail91, detail101, detail111, detail121, detail131);
				context.SaveChanges();

				var userEntity = context.Users.FirstOrDefault(u => u.Email == "Test1@mail.ru");
				var detailEntity = context.Products.FirstOrDefault();

				var userDetailEntity = new BasketProductEntity
				{
					Product = detailEntity,
					User = userEntity
				};


				context.BasketProducts.Add(userDetailEntity);
				context.SaveChanges();
				var userEntity1 = context.Users.FirstOrDefault(u => u.Email == "Test1@mail.ru");
				var order = new OrderEntity
				{
					Status = Nexta.Domain.Enums.OrderStatus.Accepted,
					User = userEntity1
				};

				context.Orders.Add(order);
				context.SaveChanges();
			}
		}
	}
}

using(var context = services.BuildServiceProvider().GetRequiredService<MainContext>())
{
    if (context.Users.FirstOrDefault() == null)
	{
	    var createdOrder = context.Orders.FirstOrDefault();
	
		var createdDetail = context.Products.FirstOrDefault();
	
		var orderDetail1 = new OrderProductEntity
		{
			Product = createdDetail,
			Order = createdOrder
		};
	
		context.OrderProducts.Add(orderDetail1);
		context.SaveChanges();
	}
}

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();
app.UseCors(builder =>
{
	builder.WithOrigins("http://localhost:5173")
		.AllowAnyMethod()
		.AllowAnyHeader()
		.AllowCredentials();
});
app.UseRouting();
app.UseAppAuth();

app.MapControllerRoute(
	name: "admin",
	pattern: "{area:exists}/{controller}/{action}");

app.MapControllerRoute(
	name: "default",
	pattern: "{controller}/{action}");

app.Run();