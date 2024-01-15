using Application.Interfaces.Presentation.Services;

using Domain.Models.Base;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Infrastructure.Persistence.Interceptors;

/// <summary>
/// The custom save changes interceptor class.
/// </summary>
/// <param name="currentUserService">The current user service.</param>
/// <inheritdoc/>
public sealed class CustomSaveChangesInterceptor(ICurrentUserService currentUserService) : SaveChangesInterceptor
{
	private readonly ICurrentUserService _currentUserService = currentUserService;

	/// <inheritdoc/>
	public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
	{
		UpdateEntities(eventData.Context);
		return base.SavingChanges(eventData, result);
	}

	/// <inheritdoc/>
	public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
	{
		UpdateEntities(eventData.Context);
		return base.SavingChangesAsync(eventData, result, cancellationToken);
	}

	private void UpdateEntities(DbContext? context)
	{
		if (context is null)
			return;

		foreach (EntityEntry<AuditedModel> entry in context.ChangeTracker.Entries<AuditedModel>())
		{
			if (entry.State is EntityState.Deleted or EntityState.Modified)
				entry.Entity.ModifiedBy = _currentUserService.UserId;

			if (entry.State is EntityState.Added)
				entry.Entity.CreatedBy = _currentUserService.UserId;
		}
	}
}
