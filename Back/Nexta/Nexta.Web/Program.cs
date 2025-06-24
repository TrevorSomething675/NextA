using Nexta.Domain.Abstractions.Services;
using Nexta.Infrastructure.DataBase;
using Nexta.Application.Services;
using Nexta.Web.Extensions;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using Nexta.Application;
using Microsoft.AspNetCore.Http.Json;
using System.Text.Json.Serialization;
using Nexta.Web.Middlewares;
using Nexta.Domain.Entities;
using Nexta.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services.AddAppOptions(builder.Configuration);
services.AddAppMapper();
await services.AddAppMinio(builder.Configuration);
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

services.AddDbContextFactory<MainContext>();

/*
using (var context = services.BuildServiceProvider().GetRequiredService<MainContext>())
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
		var detail = new DetailEntity
		{
			Name = "Моторное масло",
			Article = "A000989210713MBR",
			Description = "МАСЛО МОТОРНОЕ 229.3/229",
			Status = Nexta.Domain.Enums.DetailStatus.InStock,
			OrderDate = DateOnly.FromDateTime(DateTime.UtcNow),
			DeliveryDate = DateOnly.FromDateTime(DateTime.UtcNow),
			Count = 1,
			NewPrice = 12091
		};
		var detail2 = new DetailEntity
		{
			Name = "Моторное масло",
			Article = "A000989220713MBR",
			Description = "МАСЛО МОТОРНОЕ 229.51 SAE",
			Status = Nexta.Domain.Enums.DetailStatus.InStock,
			OrderDate = DateOnly.FromDateTime(DateTime.UtcNow),
			DeliveryDate = DateOnly.FromDateTime(DateTime.UtcNow),
			Count = 3,
			NewPrice = 12264
		};
		var detail3 = new DetailEntity
		{
			Name = "Лобовое стекло",
			Article = "A1666705400MB",
			Description = "СТЕКЛО ВЕТРОВОЕ ПЕРЕДНЕЕ для Mercedes-Benz GLE",
			Status = Nexta.Domain.Enums.DetailStatus.InStock,
			OrderDate = DateOnly.FromDateTime(DateTime.UtcNow),
			DeliveryDate = DateOnly.FromDateTime(DateTime.UtcNow),
			Count = 1,
			NewPrice = 81296
		};
		var detail4 = new DetailEntity
		{
			Name = "Свеча зажигания",
			Article = "Denso-K16RU11",
			Description = "СВЕЧА ЗАЖИГАНИЯ",
			Status = Nexta.Domain.Enums.DetailStatus.InStock,
			OrderDate = DateOnly.FromDateTime(DateTime.UtcNow),
			DeliveryDate = DateOnly.FromDateTime(DateTime.UtcNow),
			Count = 5,
			NewPrice = 218,
			OldPrice = 320
		};
		var detail5 = new DetailEntity
		{
			Name = "Масляный фильтр",
			Article = "2630035505",
			Description = "ФИЛЬТР МАСЛЯНЫЙ",
			Status = Nexta.Domain.Enums.DetailStatus.OutOfStock,
			OrderDate = DateOnly.FromDateTime(DateTime.UtcNow),
			DeliveryDate = DateOnly.FromDateTime(DateTime.UtcNow),
			Count = 2,
			NewPrice = 507
		};
		var detail6 = new DetailEntity
		{
			Name = "Очиститель двигателя",
			Article = "Grass 116100",
			Description = "Очиститель двигателя Motor Cleaner",
			Status = Nexta.Domain.Enums.DetailStatus.OutOfStock,
			OrderDate = DateOnly.FromDateTime(DateTime.UtcNow),
			DeliveryDate = DateOnly.FromDateTime(DateTime.UtcNow),
			Count = 1,
			NewPrice = 359
		};
		var detail7 = new DetailEntity
		{
			Name = "Герметик",
			Article = "703141410",
			Description = "Герметик-прокладка Reinzosil силикон серый эластич",
			Status = Nexta.Domain.Enums.DetailStatus.InStock,
			OrderDate = DateOnly.FromDateTime(DateTime.UtcNow),
			DeliveryDate = DateOnly.FromDateTime(DateTime.UtcNow),
			Count = 1,
			NewPrice = 718
		};
		var detail8 = new DetailEntity
		{
			Name = "Шина",
			Article = "526111",
			Description = "Шина зимняя нешипованная легковая 175/65R14 82T",
			Status = Nexta.Domain.Enums.DetailStatus.InStock,
			OrderDate = DateOnly.FromDateTime(DateTime.UtcNow),
			DeliveryDate = DateOnly.FromDateTime(DateTime.UtcNow),
			Count = 2,
			NewPrice = 24053,
			OldPrice = 28700
		};
		var detail9 = new DetailEntity
		{
			Name = "Шина",
			Article = "457442784",
			Description = "Автошина R13 155/70 Cordiant Road Runner 75T (лето)",
			Status = Nexta.Domain.Enums.DetailStatus.InStock,
			OrderDate = DateOnly.FromDateTime(DateTime.UtcNow),
			DeliveryDate = DateOnly.FromDateTime(DateTime.UtcNow),
			Count = 1,
			NewPrice = 5960
		};
		var detail10 = new DetailEntity
		{
			Name = "Шина",
			Article = "1012050",
			Description = "Шина летняя легковая 175/65R14 82H",
			Status = Nexta.Domain.Enums.DetailStatus.InStock,
			OrderDate = DateOnly.FromDateTime(DateTime.UtcNow),
			DeliveryDate = DateOnly.FromDateTime(DateTime.UtcNow),
			Count = 2,
			NewPrice = 9924,
			OldPrice = 11098
		};
		var detail11 = new DetailEntity
		{
			Name = "Шина",
			Article = "1010711",
			Description = "Автошина R15 195/60 Hankook Optimo ME02 K424 88H (лето)",
			Status = Nexta.Domain.Enums.DetailStatus.InStock,
			OrderDate = DateOnly.FromDateTime(DateTime.UtcNow),
			DeliveryDate = DateOnly.FromDateTime(DateTime.UtcNow),
			Count = 4,
			NewPrice = 9276
		};
		var detail12 = new DetailEntity
		{
			Name = "Компрессор",
			Article = "CA03014S",
			Description = "Компрессор X1 (30л/мин, 7 АТМ, серия STANDARD)",
			Status = Nexta.Domain.Enums.DetailStatus.InStock,
			OrderDate = DateOnly.FromDateTime(DateTime.UtcNow),
			DeliveryDate = DateOnly.FromDateTime(DateTime.UtcNow),
			Count = 7,
			NewPrice = 3113
		};
		var detail13 = new DetailEntity
		{
			Name = "Манометр",
			Article = "522200",
			Description = "Манометр шинный стрелочный в блистере. Изготовлен из ударо-прочной пластмассы. Диапазон " +
			"измерения давления 10-50 PSI/ 0,5-3,5 кг/см2. Шаг измерения 1 PSI/ 0,1 кг/см2. Кнопка для сброса показаний " +
			"давления. Игла для сброса лишнего давления.",
			Status = Nexta.Domain.Enums.DetailStatus.InStock,
			OrderDate = DateOnly.FromDateTime(DateTime.UtcNow),
			DeliveryDate = DateOnly.FromDateTime(DateTime.UtcNow),
			Count = 2,
			NewPrice = 216
		};
		if (context.Users.FirstOrDefault(u => u.Email == "Test1@mail.ru") == null)
		{
			context.Users.Add(user);
			context.Details.AddRange(detail, detail2, detail3, detail4, detail5, detail6, detail7, detail8, detail9, detail10, detail11, detail12, detail13);
			context.SaveChanges();

			var userEntity = context.Users.FirstOrDefault(u => u.Email == "Test1@mail.ru");
			var detailEntity = context.Details.FirstOrDefault();

			var userDetailEntity = new UserDetailEntity
			{
				Detail = detailEntity,
				User = userEntity
			};


			context.UserDetails.Add(userDetailEntity);
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

using(var context = services.BuildServiceProvider().GetRequiredService<MainContext>())
{
	var createdOrder = context.Orders.FirstOrDefault();

	var createdDetail = context.Details.FirstOrDefault();

	var orderDetail1 = new OrderDetailEntity
	{
		Detail = createdDetail,
		Order = createdOrder
	};

	context.OrderDetails.Add(orderDetail1);
	context.SaveChanges();

	var images = new List<ImageEntity>
	{
		new ImageEntity
		{
			Name = "news1.jpg",
			Bucket = "news"
		},
		new ImageEntity
		{
			Name = "news2.jpg",
			Bucket = "news"
		},
		new ImageEntity
		{
			Name = "news3.jpg",
			Bucket = "news"
		},
		new ImageEntity
		{
			Name = "hWL_1j5KS8U.jpg",
			Bucket = "test"
		}
	};
	context.Images.AddRange(images);
	context.SaveChanges();

	var newsImages = context.Images.Where(i => i.Bucket == "news");

	var news = new List<NewsEntity>
	{
		new NewsEntity
		{
			Header = "news1",
			Description = "Description1",
			Image = images[0],
		},
		new NewsEntity
		{
			Header = "news2",
			Description = "Description2",
			Image = images[1],
		},
		new NewsEntity
		{
			Header = "news3",
			Description = "Description3",
			Image = images[2],
		},
		new NewsEntity
		{
			Header = "test",
			Description = "desc",
			Image = images[3],
		}
	};
	context.News.AddRange(news);
	context.SaveChanges();

	var detail = context.Details.FirstOrDefault();
	var image = context.Images.FirstOrDefault();

	detail.Image = image;
	context.Details.Update(detail);
	context.SaveChanges();
}
*/

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