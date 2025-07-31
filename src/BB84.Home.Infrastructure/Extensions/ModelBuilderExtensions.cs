using BB84.Home.Application.Interfaces.Presentation.Services;
using BB84.Home.Domain.Entities.Attendance;
using BB84.Home.Domain.Entities.Documents;
using BB84.Home.Domain.Entities.Finance;
using BB84.Home.Infrastructure.Common;

using Microsoft.EntityFrameworkCore;

namespace BB84.Home.Infrastructure.Extensions;

/// <summary>
/// Provides extension methods for the <see cref="ModelBuilder"/> class.
/// </summary>
internal static class ModelBuilderExtensions
{
	/// <summary>
	/// Applies global filters to the entity model based on the current user's context.
	/// </summary>
	/// <param name="builder">The <see cref="ModelBuilder"/> instance to which the filters will be applied.</param>
	/// <param name="userService">The service providing information about the current user.</param>
	/// <returns>The modified <see cref="ModelBuilder"/> instance with the global user filters applied.</returns>
	public static ModelBuilder ApplyGlobalUserFilters(this ModelBuilder builder, ICurrentUserService userService)
	{
		builder.Entity<AccountEntity>()
			.HasQueryFilter(x => x.AccountUsers.Select(x => x.UserId).Contains(userService.UserId));

		builder.Entity<AccountUserEntity>()
			.HasQueryFilter(x => x.UserId == userService.UserId);

		builder.Entity<AttendanceEntity>()
			.HasQueryFilter(x => x.UserId == userService.UserId);

		builder.Entity<CardEntity>()
			.HasQueryFilter(x => x.UserId == userService.UserId);

		builder.Entity<DocumentEntity>()
			.HasQueryFilter(x => x.UserId == userService.UserId);

		return builder;
	}

	/// <summary>
	/// Applies all infrastructure-related entity configurations from the assembly containing the
	/// <see cref="IInfrastructureAssemblyMarker"/> type.
	/// </summary>
	/// <param name="builder">The <see cref="ModelBuilder"/> instance to which the configurations will be applied.</param>
	/// <returns>The same <see cref="ModelBuilder"/> instance with the configurations applied.</returns>
	public static ModelBuilder ApplyInfrastructureConfigurations(this ModelBuilder builder)
		=> builder.ApplyConfigurationsFromAssembly(typeof(IInfrastructureAssemblyMarker).Assembly);
}
