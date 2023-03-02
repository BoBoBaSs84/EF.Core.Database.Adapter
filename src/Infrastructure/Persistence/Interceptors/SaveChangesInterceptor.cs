using Application.Common.Interfaces.Identity;
using Domain.Common.EntityBaseTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Infrastructure.Persistence.Interceptors;

/// <summary>
/// The save changes interceptor class.
/// </summary>
/// <remarks>
/// Derives from the <see cref="Microsoft.EntityFrameworkCore.Diagnostics.SaveChangesInterceptor"/> class.
/// </remarks>
public sealed class SaveChangesInterceptor : Microsoft.EntityFrameworkCore.Diagnostics.SaveChangesInterceptor
{
	private readonly ICurrentUserService _currentUserService;

	/// <summary>
	/// Initializes a new instance of the <see cref="SaveChangesInterceptor"/> class.
	/// </summary>
	/// <param name="currentUserService">The current user service.</param>
	public SaveChangesInterceptor(ICurrentUserService currentUserService) =>
		_currentUserService = currentUserService;

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
			if (entry.State == EntityState.Added)
				entry.Entity.CreatedBy = _currentUserService.UserId;

			if (entry.State == EntityState.Modified)
				entry.Entity.ModifiedBy = _currentUserService.UserId;
		}
	}
}
