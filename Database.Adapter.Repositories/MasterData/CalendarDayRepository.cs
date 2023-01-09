﻿using Database.Adapter.Entities.MasterData;
using Database.Adapter.Repositories.BaseTypes;
using Database.Adapter.Repositories.MasterData.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Database.Adapter.Repositories.MasterData;

/// <summary>
/// The calendar day repository class.
/// </summary>
/// <remarks>
/// Inherits from the following class:
/// <list type="bullet">
/// <item>The <see cref="GenericRepository{TEntity}"/> class</item>
/// </list>
/// Implements the following interfaces:
/// <list type="bullet">
/// <item>The <see cref="ICalendarDayRepository"/> interface</item>
/// </list>
/// </remarks>
internal sealed class CalendarDayRepository : GenericRepository<CalendarDay>, ICalendarDayRepository
{
	/// <summary>
	/// Initializes a new instance of the <see cref="CalendarDayRepository"/> class.
	/// </summary>
	/// <param name="dbContext"></param>
	public CalendarDayRepository(DbContext dbContext) : base(dbContext)
	{
	}
	/// <inheritdoc/>
	public CalendarDay GetByDate(DateTime date, bool trackChanges = false) =>
		GetByCondition(x => x.Date.Equals(date), trackChanges);
	/// <inheritdoc/>
	public IQueryable<CalendarDay> GetByYear(int year, bool trackChanges = false) =>
		GetManyByCondition(x => x.Year.Equals(year), trackChanges);
	/// <inheritdoc/>
	public IQueryable<CalendarDay> GetWithinDateRange(DateTime start, DateTime end, bool trackChanges = false) =>
		GetManyByCondition(x => x.Date >= start && x.Date <= end, trackChanges);
}
