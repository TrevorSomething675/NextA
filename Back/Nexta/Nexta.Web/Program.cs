using Nexta.Domain.Abstractions.Services;
using Nexta.Infrastructure.DataBase;
using Nexta.Application.Services;
using Nexta.Domain.Entities;
using Nexta.Web.Extensions;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services.AddAppOptions();
services.AddAppMapper();
services.AddAppRepositories();
services.AddAppAuth();
services.AddMediatR(config => config.RegisterServicesFromAssemblies(Assembly.GetAssembly(typeof(Nexta.Application.AssemblyMarker))!));
services.AddScoped<IPasswordHashService, PasswordHashService>();
services.AddScoped<IJwtTokenService, JwtTokenService>();

services.AddRouting();
services.AddControllers();
services.AddDbContextFactory<MainContext>();

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
			PasswordHash = "123",
		};
		var detail = new DetailEntity
		{
			Name = "TestDetail1",
			Article = "TestArticle1",
			Description = "TestDescription1",
			Status = Nexta.Domain.Enums.DetailStatus.Accepted,
			OrderDate = DateTime.UtcNow,
			DeliveryDate = DateTime.UtcNow,
			Count = 0,
			NewPrice = 1000
		};
		var detail2 = new DetailEntity
		{
			Name = "TestDetail2",
			Article = "TestArticle2",
			Description = "TestDescription2",
			Status = Nexta.Domain.Enums.DetailStatus.AtWork,
			OrderDate = DateTime.UtcNow,
			DeliveryDate = DateTime.UtcNow,
			Count = 0,
			NewPrice = 700
		};
		var detail3 = new DetailEntity
		{
			Name = "TestDetail3",
			Article = "TestArticle3",
			Description = "TestDescription3",
			Status = Nexta.Domain.Enums.DetailStatus.AtWork,
			OrderDate = DateTime.UtcNow,
			DeliveryDate = DateTime.UtcNow,
			Count = 1,
			NewPrice = 700
		};
		var detail4 = new DetailEntity
		{
			Name = "TestDetail4",
			Article = "TestArticle4",
			Description = "TestDescription4",
			Status = Nexta.Domain.Enums.DetailStatus.AtWork,
			OrderDate = DateTime.UtcNow,
			DeliveryDate = DateTime.UtcNow,
			Count = 5,
			NewPrice = 500,
			OldPrice = 700
		};
		var detail5 = new DetailEntity
		{
			Name = "TestDetail5",
			Article = "TestArticle5",
			Description = "TestDescription5",
			Status = Nexta.Domain.Enums.DetailStatus.Accepted,
			OrderDate = DateTime.UtcNow,
			DeliveryDate = DateTime.UtcNow,
			Count = 2,
			NewPrice = 700,
			OldPrice = 700
		};
		var detail6 = new DetailEntity
		{
			Name = "TestDetail6",
			Article = "TestArticle6",
			Description = "TestDescription6",
			Status = Nexta.Domain.Enums.DetailStatus.Accepted,
			OrderDate = DateTime.UtcNow,
			DeliveryDate = DateTime.UtcNow,
			Count = 1,
			NewPrice = 700
		};
		var detail7 = new DetailEntity
		{
			Name = "TestDetail7",
			Article = "TestArticle7",
			Description = "TestDescription7",
			Status = Nexta.Domain.Enums.DetailStatus.Accepted,
			OrderDate = DateTime.UtcNow,
			DeliveryDate = DateTime.UtcNow,
			Count = 1,
			NewPrice = 700
		};
		var detail8 = new DetailEntity
		{
			Name = "TestDetail8",
			Article = "TestArticle8",
			Description = "TestDescription8",
			Status = Nexta.Domain.Enums.DetailStatus.Accepted,
			OrderDate = DateTime.UtcNow,
			DeliveryDate = DateTime.UtcNow,
			Count = 4,
			NewPrice = 700,
			OldPrice = 700
		};
		var detail9 = new DetailEntity
		{
			Name = "TestDetail9",
			Article = "TestArticle9",
			Description = "TestDescription9",
			Status = Nexta.Domain.Enums.DetailStatus.Accepted,
			OrderDate = DateTime.UtcNow,
			DeliveryDate = DateTime.UtcNow,
			Count = 0,
			NewPrice = 700,
			OldPrice = 700
		};
		var detail10 = new DetailEntity
		{
			Name = "TestDetail10",
			Article = "TestArticle10",
			Description = "TestDescription10",
			Status = Nexta.Domain.Enums.DetailStatus.Accepted,
			OrderDate = DateTime.UtcNow,
			DeliveryDate = DateTime.UtcNow,
			Count = 2,
			NewPrice = 700,
			OldPrice = 700
		};
		if (context.Users.FirstOrDefault(u => u.Email == "Test1@mail.ru") == null)
		{
			context.Users.Add(user);
			context.Details.AddRange(detail, detail2, detail3, detail4, detail5, detail6, detail7, detail8, detail9, detail10);
			context.SaveChanges();

			var userEntity = context.Users.FirstOrDefault(u => u.Email == "Test1@mail.ru");
			var detailEntity = context.Details.FirstOrDefault(d => d.Name == "TestDetail1");

			var userDetailEntity = new UserDetailEntity
			{
				Detail = detailEntity,
				User = userEntity,
			};
			context.UserDetails.Add(userDetailEntity);
			context.SaveChanges();
		}
	}
}

var app = builder.Build();

app.UseCors(builder =>
{
	builder.WithOrigins("http://localhost:5173")
		.AllowAnyMethod()
		.AllowAnyHeader()
		.AllowCredentials();
});
app.UseAppAuth();
app.UseRouting();
app.MapControllers();

app.Run();