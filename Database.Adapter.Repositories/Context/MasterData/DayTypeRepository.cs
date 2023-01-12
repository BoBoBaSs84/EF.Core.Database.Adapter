using Database.Adapter.Entities.MasterData;
using Database.Adapter.Repositories.BaseTypes;
using Database.Adapter.Repositories.Context.MasterData.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Database.Adapter.Repositories.Context.MasterData;

/// <summary>
/// The day type repository class.
/// </summary>
/// <remarks>
/// Inherits from the following class:
/// <list type="bullet">
/// <item>The <see cref="EnumeratorRepository{TEntity}"/> class</item>
/// </list>
/// </remarks>
internal sealed class DayTypeRepository : EnumeratorRepository<DayType>, IDayTypeRepository
{
	/// <summary>
	/// Initializes a new instance of the <see cref="DayTypeRepository"/> class.
	/// </summary>
	/// <param name="dbContext"></param>
	public DayTypeRepository(DbContext dbContext) : base(dbContext)
	{
	}
}
