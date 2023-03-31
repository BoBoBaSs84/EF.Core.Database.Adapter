using Application.Interfaces.Infrastructure.Logging;
using Application.Interfaces.Infrastructure.Services;
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
using Jwt = Infrastructure.Constants.InfrastructureConstants.BearerJwt;

namespace Infrastructure.Extensions;

/// <summary>
/// The service collection extensions class.
/// </summary>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here.")]
internal static class ServiceCollectionExtensions
{
	/// <summary>
	/// Enriches a service collection with the infrastructure scoped services.
	/// </summary>
	/// <param name="services">The service collection to enrich.</param>
	/// <returns>The enriched service collection.</returns>
	internal static IServiceCollection ConfigureScopedServices(this IServiceCollection services)
	{
		services.TryAddScoped<CustomSaveChangesInterceptor>();
		services.TryAddScoped<IRepositoryService, RepositoryService>();
		services.TryAddScoped<IUserService, UserService>();
		services.TryAddScoped<IRoleService, RoleService>();

		return services;
	}

	/// <summary>
	/// Enriches a service collection with the transient singleton services.
	/// </summary>
	/// <param name="services">The service collection to enrich.</param>
	/// <returns>The enriched service collection.</returns>
	internal static IServiceCollection ConfigureTransientServices(this IServiceCollection services)
	{
		services.TryAddTransient<IDateTimeService, DateTimeService>();
		services.TryAddTransient<IAuthenticationService, AuthenticationService>();

		return services;
	}

	/// <summary>
	/// Registers the application context.
	/// </summary>
	/// <param name="services">The service collection to enrich.</param>
	/// <param name="configuration">The current configuration.</param>
	/// <param name="environment">The hosting environment.</param>
	/// <returns>The enriched service collection.</returns>
	internal static IServiceCollection AddRepositoryContext(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
	{
		services.AddDbContext<RepositoryContext>(options =>
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
	internal static IServiceCollection AddIdentityService(this IServiceCollection services)
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
			.AddEntityFrameworkStores<RepositoryContext>()
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
	internal static IServiceCollection ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
	{
		IConfigurationSection jwtSettings = configuration.GetRequiredSection(Jwt.JwtSettings);

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
			ValidIssuer = jwtSettings[Jwt.ValidIssuer],
			ValidAudience = jwtSettings[Jwt.ValidAudience],
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings[Jwt.SecurityKey]))
		});

		return services;
	}

	/// <summary>
	/// Registers the <see cref="MicrosoftLoggerWrapper{T}"></see> as <b>Singleton</b>
	/// </summary>
	/// <param name="services">The service collection to enrich.</param>
	/// <returns>The enriched service collection.</returns>
	internal static IServiceCollection AddMicrosoftLogger(this IServiceCollection services)
	{
		services.TryAddSingleton(typeof(ILoggerWrapper<>), typeof(MicrosoftLoggerWrapper<>));

		return services;
	}
}
