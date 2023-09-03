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
internal sealed class AttendanceRepository : IdentityRepository<AttendanceModel>, IAttendanceRepository
{
	/// <summary>
	/// Initializes a new instance of the attendance repository class.
	/// </summary>
	/// <inheritdoc/>
	public AttendanceRepository(DbContext dbContext) : base(dbContext)
	{ }
}
