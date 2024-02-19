using DeliveryApp.Domain.Entity;
using DeliveryApp.Domain.ValueObjects;
using DeliveryApp.IoC.Extensions;
using DeliveryApp.Repository.Base;
using DeliveryApp.Repository.Interfaces;
using DeliveryApp.Repository.Interfaces.Base;
using DeliveryApp.Repository.Repositories;
using DeliveryApp.Repository.Repositories.Base;
using DeliveryApp.Service;
using DeliveryApp.Service.Interfaces;
using DeliveryApp.Service.Interfaces.Queue;
using DeliveryApp.Service.Interfaces.Storage;
using DeliveryApp.Service.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Diagnostics.CodeAnalysis;

namespace DeliveryApp.IoC
{
	[ExcludeFromCodeCoverage]
	public static class Startup
	{
		private static IConfiguration Configuration;

		public static void Setup(this IServiceCollection services)
		{
			if (services == null)
			{
				throw new ArgumentNullException(nameof(services));
			}

			Configuration = services.AddConfiguration();

			services.AddSingleton(Configuration);
			services.AddDbContext();

			AddServices(services);
			AddSecurity(services);
		}

		private static void AddDbContext(this IServiceCollection services)
		{
			services.AddScoped<DeliveryAppContext>();
			services.AddDbContext<DeliveryAppContext>((serviceProvider, builder) =>
			{
				var connectionString = Configuration.GetSection("ConnectionStrings:DeliveryAppConnection").Value;
				builder.UseNpgsql(connectionString);
			});

			services.AddGenericRepositories();
			services.AddRepository();
			services.AddThirdPartyServices();
		}

		public static void AddGenericRepositories(this IServiceCollection services)
		{
			services.AddScoped<IGenericRepository<DeliveryAppContext, User>, GenericRepository<DeliveryAppContext, User>>();
			services.AddScoped<IGenericRepository<DeliveryAppContext, Vehicle>, GenericRepository<DeliveryAppContext, Vehicle>>();
			services.AddScoped<IGenericRepository<DeliveryAppContext, VehicleBrand>, GenericRepository<DeliveryAppContext, VehicleBrand>>();
			services.AddScoped<IGenericRepository<DeliveryAppContext, VehicleModel>, GenericRepository<DeliveryAppContext, VehicleModel>>();
			services.AddScoped<IGenericRepository<DeliveryAppContext, VehicleRent>, GenericRepository<DeliveryAppContext, VehicleRent>>();
			services.AddScoped<IGenericRepository<DeliveryAppContext, DeliveryMan>, GenericRepository<DeliveryAppContext, DeliveryMan>>();
			services.AddScoped<IGenericRepository<DeliveryAppContext, Plans>, GenericRepository<DeliveryAppContext, Plans>>();
			services.AddScoped<IGenericRepository<DeliveryAppContext, PlanVersion>, GenericRepository<DeliveryAppContext, PlanVersion>>();
			services.AddScoped<IGenericRepository<DeliveryAppContext, Order>, GenericRepository<DeliveryAppContext, Order>>();
		}

		private static void AddRepository(this IServiceCollection services)
		{
			services.AddScoped<IUserRepository, UserRepository>();
			services.AddScoped<IVehicleRepository, VehicleRepository>();
			services.AddScoped<IVehicleBrandRepository, VehicleBrandRepository>();
			services.AddScoped<IVehicleModelRepository, VehicleModelRepository>();
			services.AddScoped<IVehicleRentRepository, VehicleRentRepository>();
			services.AddScoped<IDeliveryManRepository, DeliveryManRepository>();
			services.AddScoped<IPlanRepository, PlanRepository>();
			services.AddScoped<IPlanVersionRepository, PlanVersionRepository>();
			services.AddScoped<IOrderRepository, OrderRepository>();
		}

		private static void AddServices(IServiceCollection services)
		{
			services.AddScoped<IAuthService, AuthService>();
			services.AddScoped<IVehicleService, VehicleService>();
			services.AddScoped<IVehicleBrandService, VehicleBrandService>();
			services.AddScoped<IVehicleModelService, VehicleModelService>();
			services.AddScoped<IVehicleRentService, VehicleRentService>();
			services.AddScoped<IDeliveryManService, DeliveryManService>();
			services.AddScoped<IPlanService, PlanService>();
			services.AddScoped<IOrderService, OrderService>();
			services.AddScoped<IUserService, UserService>();

		}

		private static void AddThirdPartyServices(this IServiceCollection services)
		{
			services.AddScoped<IFireBaseStorage, FireBaseStorage>(p =>
			{
				return new FireBaseStorage("delivery-app-4c9cb.appspot.com");
			});

			services.AddScoped<IRabbitMqService, RabbitMqService>(p =>
			{
				var connectionFactory = new ConnectionFactory();

				new ConfigureFromConfigurationOptions<ConnectionFactory>(
					Configuration
					.GetSection("RabbitMqConfiguration"))
					.Configure(connectionFactory);

				return new RabbitMqService(connectionFactory);
			});
		}

		private static void AddSecurity(IServiceCollection services)
		{
			var tokenConfigurations = new TokenConfigurations();
			new ConfigureFromConfigurationOptions<TokenConfigurations>(
				Configuration
				.GetSection("JwtConfiguration"))
				.Configure(tokenConfigurations);

			services.AddJwtSecurity(tokenConfigurations);
		}
	}
}
