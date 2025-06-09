using Nexta.Domain.Abstractions.Services;
using Nexta.Infrastructure.DataBase;
using Nexta.Application.Services;
using Nexta.Domain.Entities;
using Nexta.Web.Extensions;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using FluentValidation;
using Nexta.Application;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services.AddAppOptions();
services.AddAppMapper();
services.AddAppRepositories();
services.AddAppAuth();
services.AddMediatR(config => config.RegisterServicesFromAssemblies(Assembly.GetAssembly(typeof(AssemblyMarker))!));
services.AddScoped<IPasswordHashService, PasswordHashService>();
services.AddScoped<IJwtTokenService, JwtTokenService>();

services.AddValidatorsFromAssembly(Assembly.GetAssembly(typeof(AssemblyMarker))!);

services.AddRouting();
services.AddControllers()
	.AddJsonOptions(options =>
	{
		options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
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
			Name = "�������� �����",
			Article = "A000989210713MBR",
			Description = "����� �������� 229.3/229",
			Status = Nexta.Domain.Enums.DetailStatus.InStock,
			OrderDate = DateOnly.FromDateTime(DateTime.UtcNow),
			DeliveryDate = DateOnly.FromDateTime(DateTime.UtcNow),
			Count = 1,
			NewPrice = 12091
		};
		var detail2 = new DetailEntity
		{
			Name = "�������� �����",
			Article = "A000989220713MBR",
			Description = "����� �������� 229.51 SAE",
			Status = Nexta.Domain.Enums.DetailStatus.InStock,
			OrderDate = DateOnly.FromDateTime(DateTime.UtcNow),
			DeliveryDate = DateOnly.FromDateTime(DateTime.UtcNow),
			Count = 3,
			NewPrice = 12264
		};
		var detail3 = new DetailEntity
		{
			Name = "������� ������",
			Article = "A1666705400MB",
			Description = "������ �������� �������� ��� Mercedes-Benz GLE",
			Status = Nexta.Domain.Enums.DetailStatus.InStock,
			OrderDate = DateOnly.FromDateTime(DateTime.UtcNow),
			DeliveryDate = DateOnly.FromDateTime(DateTime.UtcNow),
			Count = 1,
			NewPrice = 81296
		};
		var detail4 = new DetailEntity
		{
			Name = "����� ���������",
			Article = "Denso-K16RU11",
			Description = "����� ���������",
			Status = Nexta.Domain.Enums.DetailStatus.InStock,
			OrderDate = DateOnly.FromDateTime(DateTime.UtcNow),
			DeliveryDate = DateOnly.FromDateTime(DateTime.UtcNow),
			Count = 5,
			NewPrice = 218,
			OldPrice = 320
		};
		var detail5 = new DetailEntity
		{
			Name = "�������� ������",
			Article = "2630035505",
			Description = "������ ��������",
			Status = Nexta.Domain.Enums.DetailStatus.OutOfStock,
			OrderDate = DateOnly.FromDateTime(DateTime.UtcNow),
			DeliveryDate = DateOnly.FromDateTime(DateTime.UtcNow),
			Count = 2,
			NewPrice = 507
		};
		var detail6 = new DetailEntity
		{
			Name = "���������� ���������",
			Article = "Grass 116100",
			Description = "���������� ��������� Motor Cleaner",
			Status = Nexta.Domain.Enums.DetailStatus.OutOfStock,
			OrderDate = DateOnly.FromDateTime(DateTime.UtcNow),
			DeliveryDate = DateOnly.FromDateTime(DateTime.UtcNow),
			Count = 1,
			NewPrice = 359
		};
		var detail7 = new DetailEntity
		{
			Name = "��������",
			Article = "703141410",
			Description = "��������-��������� Reinzosil ������� ����� �������",
			Status = Nexta.Domain.Enums.DetailStatus.InStock,
			OrderDate = DateOnly.FromDateTime(DateTime.UtcNow),
			DeliveryDate = DateOnly.FromDateTime(DateTime.UtcNow),
			Count = 1,
			NewPrice = 718
		};
		var detail8 = new DetailEntity
		{
			Name = "����",
			Article = "526111",
			Description = "���� ������ ������������ �������� 175/65R14 82T",
			Status = Nexta.Domain.Enums.DetailStatus.InStock,
			OrderDate = DateOnly.FromDateTime(DateTime.UtcNow),
			DeliveryDate = DateOnly.FromDateTime(DateTime.UtcNow),
			Count = 2,
			NewPrice = 24053,
			OldPrice = 28700
		};
		var detail9 = new DetailEntity
		{
			Name = "����",
			Article = "457442784",
			Description = "�������� R13 155/70 Cordiant Road Runner 75T (����)",
			Status = Nexta.Domain.Enums.DetailStatus.InStock,
			OrderDate = DateOnly.FromDateTime(DateTime.UtcNow),
			DeliveryDate = DateOnly.FromDateTime(DateTime.UtcNow),
			Count = 1,
			NewPrice = 5960
		};
		var detail10 = new DetailEntity
		{
			Name = "����",
			Article = "1012050",
			Description = "���� ������ �������� 175/65R14 82H",
			Status = Nexta.Domain.Enums.DetailStatus.InStock,
			OrderDate = DateOnly.FromDateTime(DateTime.UtcNow),
			DeliveryDate = DateOnly.FromDateTime(DateTime.UtcNow),
			Count = 2,
			NewPrice = 9924,
			OldPrice = 11098
		};
		var detail11 = new DetailEntity
		{
			Name = "����",
			Article = "1010711",
			Description = "�������� R15 195/60 Hankook Optimo ME02 K424 88H (����)",
			Status = Nexta.Domain.Enums.DetailStatus.InStock,
			OrderDate = DateOnly.FromDateTime(DateTime.UtcNow),
			DeliveryDate = DateOnly.FromDateTime(DateTime.UtcNow),
			Count = 4,
			NewPrice = 9276
		};
		var detail12 = new DetailEntity
		{
			Name = "����������",
			Article = "CA03014S",
			Description = "���������� X1 (30�/���, 7 ���, ����� STANDARD)",
			Status = Nexta.Domain.Enums.DetailStatus.InStock,
			OrderDate = DateOnly.FromDateTime(DateTime.UtcNow),
			DeliveryDate = DateOnly.FromDateTime(DateTime.UtcNow),
			Count = 7,
			NewPrice = 3113
		};
		var detail13 = new DetailEntity
		{
			Name = "��������",
			Article = "522200",
			Description = "�������� ������ ���������� � ��������. ���������� �� �����-������� ����������. �������� " +
			"��������� �������� 10-50 PSI/ 0,5-3,5 ��/��2. ��� ��������� 1 PSI/ 0,1 ��/��2. ������ ��� ������ ��������� " +
			"��������. ���� ��� ������ ������� ��������.",
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
}
*/

var app = builder.Build();

app.UseCors(builder =>
{
	builder.WithOrigins("http://localhost:5173")
		.AllowAnyMethod()
		.AllowAnyHeader()
		.AllowCredentials();
});
app.UseRouting();
app.UseAppAuth();
app.MapControllers();

app.Run();