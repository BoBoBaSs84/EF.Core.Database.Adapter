using Application.Interfaces.Infrastructure.Persistence.Repositories;

using Domain.Models.Attendance;

using Infrastructure.Persistence.Repositories.Base;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

/// <summary>
/// The attendance repository class.
/// </summary>
/// <remarks>
/// Derives from the <see cref="IdentityRepository{TEntity}"/> class
/// and implements the <see cref="IAttendanceRepository"/> interface.
/// </remarks>
/// <remarks>
/// Initializes a new instance of the attendance repository class.
/// </remarks>
/// <inheritdoc/>
internal sealed class AttendanceRepository(DbContext dbContext) : IdentityRepository<AttendanceModel>(dbContext), IAttendanceRepository
{
}
