using Application.Interfaces.Infrastructure.Persistence.Repositories;

using BB84.EntityFrameworkCore.Repositories;

using Domain.Models.Attendance;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

/// <summary>
/// The attendance repository class.
/// </summary>
/// <inheritdoc/>
internal sealed class AttendanceRepository(DbContext dbContext) : IdentityRepository<AttendanceModel>(dbContext), IAttendanceRepository
{ }
