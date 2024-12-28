using Application.Interfaces.Infrastructure.Persistence;
using Application.Interfaces.Infrastructure.Persistence.Repositories;

using BB84.EntityFrameworkCore.Repositories;

using Domain.Entities.Attendance;

namespace Infrastructure.Persistence.Repositories;

/// <summary>
/// The attendance repository class.
/// </summary>
/// <param name="context">The database context to work with.</param>
internal sealed class AttendanceRepository(IRepositoryContext context) : IdentityRepository<AttendanceEntity>(context), IAttendanceRepository
{ }
