using BB84.EntityFrameworkCore.Entities.Abstractions.Components;
using BB84.Home.Application.Interfaces.Presentation.Services;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace BB84.Home.Infrastructure.Persistence.Interceptors;

/// <summary>
/// The user auditing save changes interceptor.
/// </summary>
/// <param name="currentUserService">The current user service.</param>
/// <inheritdoc/>
public sealed class UserAuditingInterceptor(ICurrentUserService currentUserService) : SaveChangesInterceptor
{
	private readonly ICurrentUserService _currentUserService = currentUserService;

	/// <inheritdoc/>
	public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
	{
		InterceptEntities(eventData.Context);
		return base.SavingChanges(eventData, result);
	}

	/// <inheritdoc/>
	public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
	{
		InterceptEntities(eventData.Context);
		return base.SavingChangesAsync(eventData, result, cancellationToken);
	}

	private void InterceptEntities(DbContext? context)
	{
		if (context is not null)
		{
			foreach (EntityEntry<IUserAudited> entry in context.ChangeTracker.Entries<IUserAudited>())
			{
				switch (entry.State)
				{
					case EntityState.Added:
						entry.Entity.CreatedBy = _currentUserService.UserName;
						break;
					case EntityState.Modified:
						entry.Entity.EditedBy = _currentUserService.UserName;
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
}
