using Application.Interfaces.Infrastructure.Persistence.Repositories.Base;

using Domain.Models.Common;

namespace Application.Interfaces.Infrastructure.Persistence.Repositories;

/// <summary>
/// The calendar repository interface.
/// </summary>
/// <remarks>
/// Derives from the following interfaces:
/// <list type="bullet">
/// <item>The <see cref="IIdentityRepository{TIdentityEntity}"/> interface</item>
/// </list>
/// </remarks>
public interface ICalendarRepository : IIdentityRepository<CalendarModel>
{
}
