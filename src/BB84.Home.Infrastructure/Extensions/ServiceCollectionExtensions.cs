﻿using System.Text;

using BB84.EntityFrameworkCore.Repositories.SqlServer.Interceptors;
using BB84.Home.Application.Interfaces.Infrastructure.Persistence;
using BB84.Home.Application.Interfaces.Infrastructure.Services;
using BB84.Home.Application.Options;
using BB84.Home.Domain.Entities.Identity;
using BB84.Home.Infrastructure.Common;
using BB84.Home.Infrastructure.Persistence;
using BB84.Home.Infrastructure.Persistence.Interceptors;
using BB84.Home.Infrastructure.Services;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BB84.Home.Infrastructure.Extensions;

/// <summary>
/// The <see cref="IServiceCollection"/> extensions class.
/// </summary>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here.")]
internal static class ServiceCollectionExtensions
{
	/// <summary>
	/// Registers the required scoped services to the <paramref name="services"/> collection.
	/// </summary>
	/// <param name="services">The service collection to enrich.</param>
	/// <returns>The enriched service collection.</returns>
	internal static IServiceCollection RegisterScopedServices(this IServiceCollection services)
	{
		services.TryAddScoped<UserAuditingInterceptor>();
		services.TryAddScoped<SoftDeletableInterceptor>();
		services.TryAddScoped<IRepositoryService, RepositoryService>();
		services.TryAddScoped<IUserService, UserService>();
		services.TryAddScoped<IRoleService, RoleService>();

		return services;
	}

	/// <summary>
	/// Registers the required singleton services to the <paramref name="services"/> collection.
	/// </summary>
	/// <param name="services">The service collection to enrich.</param>
	/// <returns>The enriched service collection.</returns>
	internal static IServiceCollection RegisterSingletonServices(this IServiceCollection services)
	{
		services.TryAddSingleton(typeof(ILoggerService<>), typeof(LoggerService<>));

		return services;
	}

	/// <summary>
	/// Registers the required repository context to the <paramref name="services"/> collection.
	/// </summary>
	/// <param name="services">The service collection to enrich.</param>
	/// <param name="configuration">The current configuration.</param>
	/// <param name="environment">The hosting environment.</param>
	/// <returns>The enriched service collection.</returns>
	internal static IServiceCollection RegisterRepositoryContext(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
	{
		services.AddDbContext<IRepositoryContext, RepositoryContext>(options =>
		{
			options.UseSqlServer(configuration.GetConnectionString("SqlServerConnection"),
				builder =>
				{
					builder.MaxBatchSize(1000);
					builder.MigrationsHistoryTable("MigrationsHistory", InfrastructureConstants.SqlSchema.Private);
					builder.MigrationsAssembly(typeof(IInfrastructureAssemblyMarker).Assembly.FullName);
				});

			if (environment.IsDevelopment() || environment.IsTesting())
			{
				options.EnableSensitiveDataLogging(true);
				options.EnableDetailedErrors(true);

				if (environment.IsTesting())
					options.LogTo(Console.WriteLine, LogLevel.Warning);

				if (environment.IsDevelopment())
					options.LogTo(Console.WriteLine, LogLevel.Debug);
			}
		});

		return services;
	}

	/// <summary>
	/// Registers the required identity services to the <paramref name="services"/> collection.
	/// </summary>
	/// <param name="services">The service collection to enrich.</param>
	/// <returns>The enriched service collection.</returns>
	internal static IServiceCollection RegisterIdentityService(this IServiceCollection services)
	{
		BearerSettingsOption settings = services.BuildServiceProvider()
			.GetRequiredService<IOptions<BearerSettingsOption>>().Value;

		services.AddIdentity<UserEntity, RoleEntity>(options =>
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
		}
		).AddEntityFrameworkStores<RepositoryContext>()
		.AddTokenProvider<DataProtectorTokenProvider<UserEntity>>(settings.Issuer)
		.AddUserManager<UserService>()
		.AddRoleManager<RoleService>();

		return services;
	}

	/// <summary>
	/// Registers the required jwt bearer configuration to the <paramref name="services"/> collection.
	/// </summary>
	/// <param name="services">The service collection to enrich.</param>
	/// <returns>The enriched service collection.</returns>
	internal static IServiceCollection RegisterJwtBearerConfiguration(this IServiceCollection services)
	{
		BearerSettingsOption settings = services.BuildServiceProvider()
			.GetRequiredService<IOptions<BearerSettingsOption>>().Value;

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
			ValidIssuer = settings.Issuer,
			ValidAudience = settings.Audience,
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.SecurityKey))
		});

		return services;
	}

	/// <summary>
	/// Registers the required cross-origin resource sharing services to the <paramref name="services"/> collection.
	/// </summary>
	/// <param name="services">The service collection to enrich.</param>
	/// <returns>The enriched service collection.</returns>
	internal static IServiceCollection RegisterCors(this IServiceCollection services)
	{
		services.AddCors(options
			=> options.AddPolicy("EnableCors", policy
				=> policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

		return services;
	}
}
