﻿using BB84.EntityFrameworkCore.Repositories.Abstractions;

using Domain.Entities.Attendance;

namespace Application.Interfaces.Infrastructure.Persistence.Repositories;

/// <summary>
/// The attendance repository interface.
/// </summary>
/// <inheritdoc/>
public interface IAttendanceRepository : IIdentityRepository<AttendanceEntity>
{ }
