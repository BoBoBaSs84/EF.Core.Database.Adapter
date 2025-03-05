using BB84.EntityFrameworkCore.Repositories;
using BB84.EntityFrameworkCore.Repositories.Abstractions;
using BB84.Home.Application.Interfaces.Infrastructure.Persistence.Repositories;
using BB84.Home.Domain.Entities.Attendance;

namespace BB84.Home.Infrastructure.Persistence.Repositories;

/// <summary>
/// The attendance repository class.
/// </summary>
/// <inheritdoc/>
internal sealed class AttendanceRepository(IDbContext dbContext) : IdentityRepository<AttendanceEntity>(dbContext), IAttendanceRepository
{ }
