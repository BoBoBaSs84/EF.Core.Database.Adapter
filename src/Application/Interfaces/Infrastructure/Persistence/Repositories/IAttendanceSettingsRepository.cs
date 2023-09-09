using Application.Interfaces.Infrastructure.Persistence.Repositories.Base;

using Domain.Models.Attendance;

namespace Application.Interfaces.Infrastructure.Persistence.Repositories;

/// <summary>
/// The attendance settings repository interface.
/// </summary>
/// <remarks>
/// Derives from the following interfaces:
/// <list type="bullet">
/// <item>The <see cref="IIdentityRepository{TIdentityEntity}"/> interface</item>
/// </list>
/// </remarks>
public interface IAttendanceSettingsRepository : IIdentityRepository<AttendanceSettingsModel>
{
}
