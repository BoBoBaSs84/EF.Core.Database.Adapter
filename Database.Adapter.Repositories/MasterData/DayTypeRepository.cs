using Database.Adapter.Entities.MasterData;
using Database.Adapter.Repositories.BaseTypes;
using Microsoft.EntityFrameworkCore;

namespace Database.Adapter.Repositories.MasterData;

/// <summary>
/// The day type repository class.
/// </summary>
/// <remarks>
/// Inherits from the following class:
/// <list type="bullet">
/// <item>The <see cref="GenericRepository{TEntity}"/>(<see cref="DayType"/>) class</item>
/// </list>
/// </remarks>
internal sealed class DayTypeRepository : EnumeratorRepository<DayType>
{
	/// <summary>
	/// Initializes a new instance of the <see cref="DayTypeRepository"/> class.
	/// </summary>
	/// <param name="dbContext"></param>
	public DayTypeRepository(DbContext dbContext) : base(dbContext)
	{
	}
}
