﻿using Application.Common.Interfaces.Repositories;
using Domain.Entities.Enumerator;
using Infrastructure.Persistence.Repositories.BaseTypes;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

/// <summary>
/// The day type repository class.
/// </summary>
/// <remarks>
/// Derives from the <see cref="EnumeratorRepository{TEntity}"/> class and implements the interfaces:
/// <list type="bullet">
/// <item>The <see cref="IDayTypeRepository"/> interface</item>
/// </list>
/// </remarks>
internal sealed class DayTypeRepository : EnumeratorRepository<DayType>, IDayTypeRepository
{
	/// <summary>
	/// Initializes a new instance of the <see cref="DayTypeRepository"/> class.
	/// </summary>
	/// <param name="dbContext">The database context.</param>
	public DayTypeRepository(DbContext dbContext) : base(dbContext)
	{
	}
}
