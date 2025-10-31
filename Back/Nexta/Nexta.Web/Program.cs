using System.Reflection;
using System.Text.Json.Serialization;
using FluentValidation;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Nexta.Application;
using Nexta.Application.Abstractions;
using Nexta.Application.Interfaces;
using Nexta.Application.Services;
using Nexta.Domain.Models.Baskets;
using Nexta.Domain.Models.Products;
using Nexta.Domain.Models.Users;
using Nexta.Domain.Options;
using Nexta.Domain.Services.VerificationCode;
using Nexta.Infrastructure.Persistence;
using Nexta.Infrastructure.Services;
using Nexta.Web.Extensions;
using Nexta.Web.Middlewares;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services.AddAppOptions(builder.Configuration);
services.AddAppMapper();
services.AddDbContext<MainContext>((serviceProvider, optionsBuilder) =>
{
    var dbOptions = serviceProvider.GetRequiredService<IOptions<DataBaseOptions>>().Value;

    optionsBuilder.UseNpgsql(dbOptions.ConnectionString);
});
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

using (var context = services.BuildServiceProvider().GetRequiredService<MainContext>())
{
    context.Database.EnsureCreated();
    var userJij = context.Users.FirstOrDefault();
	if(userJij == null)
	{
		context.Database.EnsureDeleted();
		context.Database.EnsureCreated();
		if (!context.Users.Any())
		{
            var user = new User(
                "TestFName1",
                "TestLName1",
                "TestMName1",
                "Test1@mail.ru",
                "123123123Qq"
            );
			var detail = new Product(
				"Моторное масло",
				"A000989210713MBR",
				"МАСЛО МОТОРНОЕ 229.3/229",
				Nexta.Domain.Enums.ProductStatus.InStock,
				1,
				12091,
                0,
				true
			);
            var detail1 = new Product(
                "Моторное масло-1",
                "A000989210713MBR-1",
                "МАСЛО МОТОРНОЕ 229.3/229-1",
                Nexta.Domain.Enums.ProductStatus.InStock,
                1,
                12091,
                0,
                true
            );
            var detail2 = new Product(
				"Моторное масло",
				"A000989220713MBR",
				"МАСЛО МОТОРНОЕ 229.51 SAE",
				Nexta.Domain.Enums.ProductStatus.InStock,
				3,
				12264,
                0,
				true
			);
            var detail21 = new Product(
                "Моторное масло-1",
                "A000989220713MBR-1",
                "МАСЛО МОТОРНОЕ 229.51 SAE-1",
                Nexta.Domain.Enums.ProductStatus.InStock,
                3,
                12264,
                0,
                true
            );
            var detail3 = new Product(
				"Лобовое стекло",
				"A1666705400MB",
				"СТЕКЛО ВЕТРОВОЕ ПЕРЕДНЕЕ для Mercedes-Benz GLE",
				Nexta.Domain.Enums.ProductStatus.InStock,
				1,
				81296,
                0,
				true
			);
            var detail31 = new Product(
                "Лобовое стекло-1",
                "A1666705400MB-1",
                "СТЕКЛО ВЕТРОВОЕ ПЕРЕДНЕЕ для Mercedes-Benz GLE-1",
                Nexta.Domain.Enums.ProductStatus.InStock,
                1,
                81296,
                null,
                true
            );

            var detail4 = new Product(
                "Свеча зажигания",
                "Denso-K16RU11",
                "СВЕЧА ЗАЖИГАНИЯ",
                Nexta.Domain.Enums.ProductStatus.InStock,
                5,
                218,
                320,
                true
            );

            var detail41 = new Product(
                "Свеча зажигания-1",
                "Denso-K16RU11-1",
                "СВЕЧА ЗАЖИГАНИЯ-1",
                Nexta.Domain.Enums.ProductStatus.InStock,
                5,
                218,
                320,
                true
            );

            var detail5 = new Product(
                "Масляный фильтр",
                "2630035505",
                "ФИЛЬТР МАСЛЯНЫЙ",
                Nexta.Domain.Enums.ProductStatus.OutOfStock,
                2,
                507,
                null,
                true
            );

            var detail51 = new Product(
                "Масляный фильтр-1",
                "2630035505-1",
                "ФИЛЬТР МАСЛЯНЫЙ-1",
                Nexta.Domain.Enums.ProductStatus.OutOfStock,
                2,
                507,
                null,
                true
            );

            var detail6 = new Product(
                "Очиститель двигателя",
                "Grass 116100",
                "Очиститель двигателя Motor Cleaner",
                Nexta.Domain.Enums.ProductStatus.OutOfStock,
                1,
                359,
                null,
                true
            );

            var detail61 = new Product(
                "Очиститель двигателя-1",
                "Grass 116100-1",
                "Очиститель двигателя Motor Cleaner-1",
                Nexta.Domain.Enums.ProductStatus.OutOfStock,
                1,
                359,
                null,
                true
            );

            var detail7 = new Product(
                "Герметик",
                "703141410",
                "Герметик-прокладка Reinzosil силикон серый эластич",
                Nexta.Domain.Enums.ProductStatus.InStock,
                1,
                718,
                null,
                true
            );

            var detail71 = new Product(
                "Герметик-1",
                "703141410-1",
                "Герметик-прокладка Reinzosil силикон серый эластич-1",
                Nexta.Domain.Enums.ProductStatus.InStock,
                1,
                718,
                null,
                true
            );

            var detail8 = new Product(
                "Шина",
                "526111",
                "Шина зимняя нешипованная легковая 175/65R14 82T",
                Nexta.Domain.Enums.ProductStatus.InStock,
                2,
                24053,
                28700,
                true
            );

            var detail81 = new Product(
                "Шина-1",
                "526111-1",
                "Шина зимняя нешипованная легковая 175/65R14 82T-1",
                Nexta.Domain.Enums.ProductStatus.InStock,
                2,
                24053,
                28700,
                true
            );

            var detail9 = new Product(
                "Шина",
                "457442784",
                "Автошина R13 155/70 Cordiant Road Runner 75T (лето)",
                Nexta.Domain.Enums.ProductStatus.InStock,
                1,
                5960,
                null,
                true
            );

            var detail91 = new Product(
                "Шина-1",
                "457442784-1",
                "Автошина R13 155/70 Cordiant Road Runner 75T (лето)-1",
                Nexta.Domain.Enums.ProductStatus.InStock,
                1,
                5960,
                null,
                true
            );

            var detail10 = new Product(
                "Шина",
                "1012050",
                "Шина летняя легковая 175/65R14 82H",
                Nexta.Domain.Enums.ProductStatus.InStock,
                2,
                9924,
                11098,
                false
            );

            var detail101 = new Product(
                "Шина-1",
                "1012050-1",
                "Шина летняя легковая 175/65R14 82H-1",
                Nexta.Domain.Enums.ProductStatus.InStock,
                2,
                9924,
                11098,
                false
            );

            var detail11 = new Product(
                "Шина",
                "1010711",
                "Автошина R15 195/60 Hankook Optimo ME02 K424 88H (лето)",
                Nexta.Domain.Enums.ProductStatus.InStock,
                4,
                9276,
                null,
                true
            );

            var detail111 = new Product(
                "Шина-1",
                "1010711-1",
                "Автошина R15 195/60 Hankook Optimo ME02 K424 88H (лето)-1",
                Nexta.Domain.Enums.ProductStatus.InStock,
                4,
                9276,
                null,
                true
            );

            var detail12 = new Product(
                "Компрессор",
                "CA03014S",
                "Компрессор X1 (30л/мин, 7 АТМ, серия STANDARD)",
                Nexta.Domain.Enums.ProductStatus.InStock,
                7,
                3113,
                null,
                true
            );

            var detail121 = new Product(
                "Компрессор-1",
                "CA03014S-1",
                "Компрессор X1 (30л/мин, 7 АТМ, серия STANDARD)-1",
                Nexta.Domain.Enums.ProductStatus.InStock,
                7,
                3113,
                null,
                true
            );

            var detail13 = new Product(
                "Манометр",
                "522200",
                "Манометр шинный стрелочный в блистере. Изготовлен из ударо-прочной пластмассы. Диапазон " +
                "измерения давления 10-50 PSI/ 0,5-3,5 кг/см2. Шаг измерения 1 PSI/ 0,1 кг/см2. Кнопка для сброса показаний " +
                "давления. Игла для сброса лишнего давления.",
                Nexta.Domain.Enums.ProductStatus.InStock,
                2,
                216,
                null,
                true
            );

            var detail131 = new Product(
                "Манометр-1",
                "522200-1",
                "Манометр шинный стрелочный в блистере. Изготовлен из ударо-прочной пластмассы. Диапазон " +
                "измерения давления 10-50 PSI/ 0,5-3,5 кг/см2. Шаг измерения 1 PSI/ 0,1 кг/см2. Кнопка для сброса показаний " +
                "давления. Игла для сброса лишнего давления.-1",
                Nexta.Domain.Enums.ProductStatus.InStock,
                2,
                216,
                null,
                true
            );
            if (context.Users.FirstOrDefault(u => u.Email == "Test1@mail.ru") == null)
			{
				context.Users.Add(user);
				context.Products.AddRange(detail, detail2, detail3, detail4, detail5, detail6, detail7, detail8, detail9, detail10, detail11, detail12, detail13,
                    detail11, detail21, detail31, detail41, detail51, detail61, detail71, detail81, detail91, detail101, detail111, detail121, detail131);
				context.SaveChanges();
			}
		}
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