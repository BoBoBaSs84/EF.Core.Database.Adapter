using BB84.EntityFrameworkCore.Entities.Abstractions.Components;
using BB84.Home.Application.Interfaces.Application.Providers;
using BB84.Home.Application.Interfaces.Presentation.Services;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace BB84.Home.Infrastructure.Persistence.Interceptors;

/// <summary>
/// Represents an interceptor for auditing changes in the database context.
/// </summary>
/// <param name="userService">The user service to retrieve the current user information.</param>
/// <param name="dateTimeProvider">The provider service to retrieve the current date and time.</param>
internal sealed class AuditingInterceptor(ICurrentUserService userService, IDateTimeProvider dateTimeProvider) : SaveChangesInterceptor
{
	public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
	{
		InterceptEntities(eventData.Context);
		return base.SavingChanges(eventData, result);
	}

	public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
	{
		InterceptEntities(eventData.Context);
		return base.SavingChangesAsync(eventData, result, cancellationToken);
	}

	private void InterceptEntities(DbContext? context)
	{
		if (context is not null)
		{
			ApplyUserAudition(context);
			ApplyTimeAudition(context);
		}
	}

	private void ApplyTimeAudition(DbContext context)
	{
		foreach (EntityEntry<ITimeAudited> entry in context.ChangeTracker.Entries<ITimeAudited>())
		{
			switch (entry.State)
			{
				case EntityState.Added:
					entry.Entity.CreatedAt = dateTimeProvider.UtcNow;
					break;
				case EntityState.Modified:
					entry.Entity.EditedAt = dateTimeProvider.UtcNow;
					break;
				case EntityState.Detached:
				case EntityState.Unchanged:
				case EntityState.Deleted:
				default:
					break;
			}
		}
	}

	private void ApplyUserAudition(DbContext context)
	{
		foreach (EntityEntry<IUserAudited> entry in context.ChangeTracker.Entries<IUserAudited>())
		{
			switch (entry.State)
			{
				case EntityState.Added:
					entry.Entity.CreatedBy = userService.UserName;
					break;
				case EntityState.Modified:
					entry.Entity.EditedBy = userService.UserName;
					break;
				case EntityState.Detached:
				case EntityState.Unchanged:
				case EntityState.Deleted:
				default:
					break;
			}
		}
	}
}