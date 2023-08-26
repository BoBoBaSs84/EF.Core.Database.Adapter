using Application.Interfaces.Infrastructure.Persistence.Repositories.Base;

using Domain.Entities.Private;

namespace Application.Interfaces.Infrastructure.Persistence.Repositories;

/// <summary>
/// The attendance repository interface.
/// </summary>
/// <remarks>
/// Derives from the following interfaces:
/// <list type="bullet">
/// <item>The <see cref="IIdentityRepository{TIdentityEntity}"/> interface</item>
/// </list>
/// </remarks>
public interface IAttendanceRepository : IIdentityRepository<Attendance>
{
}
