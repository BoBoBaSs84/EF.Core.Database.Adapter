﻿using Application.Interfaces.Infrastructure.Repositories;
using Domain.Entities.Private;
using Infrastructure.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

/// <summary>
/// The calendar day repository class.
/// </summary>
/// <remarks>
/// Derives from the <see cref="IdentityRepository{TEntity}"/> class and implements the interfaces:
/// <list type="bullet">
/// <item>The <see cref="IAttendanceRepository"/> interface</item>
/// </list>
/// </remarks>
internal sealed class AttendanceRepository : IdentityRepository<Attendance>, IAttendanceRepository
{
	/// <summary>
	/// Initializes a new instance of the <see cref="AttendanceRepository"/> class.
	/// </summary>
	/// <param name="dbContext">The database context.</param>
	public AttendanceRepository(DbContext dbContext) : base(dbContext)
	{
	}
}
