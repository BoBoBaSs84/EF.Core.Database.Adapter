using Application.Interfaces.Application;
using Application.Interfaces.Infrastructure;
using Application.Interfaces.Infrastructure.Identity;
using Application.Interfaces.Infrastructure.Logging;
using Domain.Constants;
using Domain.Entities.Identity;
using Infrastructure.Common;
using Infrastructure.Logging;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Interceptors;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using IC = Infrastructure.Constants.InfrastructureConstants;

namespace Infrastructure.Installer;

/// <summary>
/// Helper class for infrastructure dependency injection.
/// </summary>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here.")]
public static class DependencyInjectionHelper
{
	/// <summary>
	/// Enriches a service collection with the infrastructure services.
	/// </summary>
	/// <param name="services">The service collection to enrich.</param>
	/// <param name="configuration">The current configuration.</param>
	/// <param name="environment">The hosting environment.</param>
	/// <returns>The enriched service collection.</returns>
	public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
	{
		services.AddMicrosoftLogger();

		services.AddApplicationContext(configuration, environment);
		services.AddIdentityService();
		services.ConfigureJWT(configuration);

		services.TryAddScoped<CustomSaveChangesInterceptor>();
		services.TryAddScoped<IUnitOfWork, UnitOfWork>();
		services.TryAddScoped<IUserService, UserService>();
		services.TryAddScoped<IRoleService, RoleService>();
		services.TryAddScoped<IAuthenticationService, AuthenticationService>();

		return services;
	}

	/// <summary>
	/// Registers the application context.
	/// </summary>
	/// <param name="services">The service collection to enrich.</param>
	/// <param name="configuration">The current configuration.</param>
	/// <param name="environment">The hosting environment.</param>
	/// <returns>The enriched service collection.</returns>
	private static IServiceCollection AddApplicationContext(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
	{
		services.AddDbContext<ApplicationContext>(options =>
		{
			options.UseSqlServer(configuration.GetConnectionString("SqlServerConnection"),
				builder =>
				{
					builder.MigrationsHistoryTable("Migration", DomainConstants.Sql.Schema.PRIVATE);
					builder.MigrationsAssembly(typeof(IInfrastructureAssemblyMarker).Assembly.FullName);
				});

			if (environment.IsDevelopment())
				options.LogTo(Console.WriteLine, LogLevel.Debug);

			if (environment.IsEnvironment(DomainConstants.Environment.Testing))
				options.LogTo(Console.WriteLine, LogLevel.Error);

			if (!environment.IsProduction())
			{
				options.EnableSensitiveDataLogging(true);
				options.EnableDetailedErrors(true);
			}
		});

		return services;
	}

	/// <summary>
	/// Registers the identity service.
	/// </summary>
	/// <param name="services">The service collection to enrich.</param>
	/// <returns>The enriched service collection.</returns>
	private static IServiceCollection AddIdentityService(this IServiceCollection services)
	{
		services.AddIdentity<User, Role>(options =>
		{
			options.SignIn.RequireConfirmedAccount = true;
			options.SignIn.RequireConfirmedEmail = true;
			options.Password.RequireDigit = true;
			options.Password.RequireLowercase = true;
			options.Password.RequireUppercase = true;
			options.Password.RequiredLength = 12;
			options.Lockout.MaxFailedAccessAttempts = 3;
			options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
			options.User.RequireUniqueEmail = true;
		})
			.AddEntityFrameworkStores<ApplicationContext>()
			.AddDefaultTokenProviders()
			.AddUserManager<UserService>()
			.AddRoleManager<RoleService>();

		return services;
	}

	/// <summary>
	/// Registers the identity jwt bearer authentication.
	/// </summary>
	/// <param name="services">The service collection to enrich.</param>
	/// <param name="configuration">The current configuration.</param>
	/// <returns>The enriched service collection.</returns>
	private static IServiceCollection ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
	{
		IConfigurationSection jwtSettings = configuration.GetRequiredSection(IC.JwtSettings);

		services.AddAuthentication(options =>
		{
			options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
		}).AddJwtBearer(options => options.TokenValidationParameters = new()
		{
			ValidateIssuer = true,
			ValidateAudience = true,
			ValidateLifetime = true,
			ValidateIssuerSigningKey = true,
			ValidIssuer = jwtSettings[IC.ValidIssuer],
			ValidAudience = jwtSettings[IC.ValidAudience],
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings[IC.SecurityKey]))
		});

		return services;
	}

	/// <summary>
	/// Registers the <see cref="MicrosoftLoggerWrapper{T}"></see> as <b>Singleton</b>
	/// </summary>
	/// <param name="services">The service collection to enrich.</param>
	/// <returns>The enriched service collection.</returns>
	private static IServiceCollection AddMicrosoftLogger(this IServiceCollection services)
	{
		services.TryAddSingleton(typeof(ILoggerWrapper<>), typeof(MicrosoftLoggerWrapper<>));
		return services;
	}
}
