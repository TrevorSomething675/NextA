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

/*
using (var context = services.BuildServiceProvider().GetRequiredService<MainContext>())
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
		Count = 1,
		NewPrice = 800,
		OldPrice = 1000,
	};
	if (context.Users.FirstOrDefault(u => u.Email == "Test1@mail.ru") == null)
	{
		context.Users.Add(user);
		context.Details.Add(detail);
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
*/

var app = builder.Build();

app.UseCors(builder =>
{
	builder.WithOrigins("http://localhost:3000")
		.AllowAnyMethod()
		.AllowAnyHeader()
		.AllowCredentials();
});
app.UseAppAuth();
app.UseRouting();
app.MapControllers();

app.Run();