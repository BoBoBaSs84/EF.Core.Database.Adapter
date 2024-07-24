using Application.Interfaces.Infrastructure.Persistence.Repositories;

using BB84.EntityFrameworkCore.Repositories;

using Domain.Models.Common;

using Infrastructure.Interfaces.Persistence;

namespace Infrastructure.Persistence.Repositories;

/// <summary>
/// The calendar day repository class.
/// </summary>
/// <param name="context">The database context to work with.</param>
internal sealed class CalendarRepository(IRepositoryContext context) : IdentityRepository<CalendarModel>(context), ICalendarRepository
{ }
