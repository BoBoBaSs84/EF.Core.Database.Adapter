using System.Text;

using Application.Interfaces.Infrastructure.Persistence;
using Application.Interfaces.Infrastructure.Services;

using BB84.EntityFrameworkCore.Repositories.SqlServer.Interceptors;

using Domain.Models.Identity;

using Infrastructure.Common;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Generators;
using Infrastructure.Persistence.Interceptors;
using Infrastructure.Services;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

using Jwt = Infrastructure.Constants.InfrastructureConstants.BearerJwt;
using SqlSchema = Domain.Constants.DomainConstants.Sql.Schema;

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
		services.TryAddScoped<AuditingInterceptor>();
		services.TryAddScoped<SoftDeletableInterceptor>();
		services.TryAddScoped<IRepositoryService, RepositoryService>();
		services.TryAddScoped<UserService>();
		services.TryAddScoped<RoleService>();

		return services;
	}

	/// <summary>
	/// Enriches a service collection with the transient singleton services.
	/// </summary>
	/// <param name="services">The service collection to enrich.</param>
	/// <returns>The enriched service collection.</returns>
	internal static IServiceCollection ConfigureTransientServices(this IServiceCollection services)
	{
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
		services.AddDbContext<IRepositoryContext, RepositoryContext>(options =>
		{
			options.ReplaceService<IMigrationsSqlGenerator, RepositorySqlGenerator>();
			options.UseSqlServer(configuration.GetConnectionString("SqlServerConnection"),
				builder =>
				{
					builder.MigrationsHistoryTable("Migration", SqlSchema.Migration);
					builder.MigrationsAssembly(typeof(IInfrastructureAssemblyMarker).Assembly.FullName);
				});

			if (environment.IsDevelopment() || environment.IsTesting())
			{
				options.EnableSensitiveDataLogging(true);
				options.EnableDetailedErrors(true);

				if (environment.IsTesting())
					options.LogTo(Console.WriteLine, LogLevel.Error);

				if (environment.IsDevelopment())
					options.LogTo(Console.WriteLine, LogLevel.Debug);
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
		services.AddIdentity<UserModel, RoleModel>(options =>
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
	/// Registers the <see cref="LoggerService{T}"></see> as <b>Singleton</b>
	/// </summary>
	/// <param name="services">The service collection to enrich.</param>
	/// <returns>The enriched service collection.</returns>
	internal static IServiceCollection AddMicrosoftLogger(this IServiceCollection services)
	{
		services.TryAddSingleton(typeof(ILoggerService<>), typeof(LoggerService<>));

		return services;
	}
}
