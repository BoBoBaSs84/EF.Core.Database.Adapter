using BB84.EntityFrameworkCore.Repositories.Abstractions;
using BB84.Home.Domain.Entities.Attendance;

namespace BB84.Home.Application.Interfaces.Infrastructure.Persistence.Repositories;

/// <summary>
/// The attendance repository interface.
/// </summary>
/// <inheritdoc/>
public interface IAttendanceRepository : IIdentityRepository<AttendanceEntity>
{ }
