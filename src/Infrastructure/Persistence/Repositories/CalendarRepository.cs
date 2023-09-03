using Application.Interfaces.Infrastructure.Persistence.Repositories;

using Domain.Models.Common;

using Infrastructure.Persistence.Repositories.Base;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

/// <summary>
/// The calendar day repository class.
/// </summary>
/// <remarks>
/// Derives from the <see cref="IdentityRepository{TEntity}"/> class
/// and implements the <see cref="ICalendarRepository"/> interface.
/// </remarks>
internal sealed class CalendarRepository : IdentityRepository<CalendarModel>, ICalendarRepository
{
	/// <summary>
	/// Initializes a new instance of the calendar day repository class.
	/// </summary>
	/// <inheritdoc/>
	public CalendarRepository(DbContext dbContext) : base(dbContext)
	{ }
}
