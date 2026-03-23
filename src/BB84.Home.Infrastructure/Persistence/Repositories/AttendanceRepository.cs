using BB84.EntityFrameworkCore.Repositories;
using BB84.Home.Application.Interfaces.Infrastructure.Persistence;
using BB84.Home.Application.Interfaces.Infrastructure.Persistence.Repositories;
using BB84.Home.Domain.Entities.Attendance;

namespace BB84.Home.Infrastructure.Persistence.Repositories;

/// <summary>
/// Represents the repository for the <see cref="AttendanceEntity"/> entity.
/// </summary>
internal sealed class AttendanceRepository(IRepositoryContext repositoryContext)
	: IdentityRepository<AttendanceEntity>(repositoryContext), IAttendanceRepository
{ }
