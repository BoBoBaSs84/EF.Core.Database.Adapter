using Application.Interfaces.Infrastructure.Persistence.Repositories;

using BB84.EntityFrameworkCore.Repositories;

using Domain.Models.Common;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

/// <summary>
/// The calendar day repository class.
/// </summary>
/// <inheritdoc/>
internal sealed class CalendarRepository(DbContext dbContext) : IdentityRepository<CalendarModel>(dbContext), ICalendarRepository
{ }
