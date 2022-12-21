using Database.Adapter.Entities.MasterData;
using Database.Adapter.Repositories.BaseTypes;
using Database.Adapter.Repositories.MasterData.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Database.Adapter.Repositories.MasterData;

/// <summary>
/// The day type repository class.
/// </summary>
/// <remarks>
/// Inherits from the following classes:
/// <list type="bullet">
/// <item>The <see cref="GenericRepository{TEntity}"/>(<see cref="DayType"/>) class</item>
/// </list>
/// Implements the following interfaces:
/// <list type="bullet">
/// <item>The <see cref="IDayTypeRepository"/> interface</item>
/// </list>
/// </remarks>
internal sealed class DayTypeRepository : GenericRepository<DayType>, IDayTypeRepository
{
	/// <summary>
	/// Initializes a new instance of the <see cref="DayTypeRepository"/> class.
	/// </summary>
	/// <param name="dbContext"></param>
	public DayTypeRepository(DbContext dbContext) : base(dbContext)
	{
	}

	/// <inheritdoc/>
	public IEnumerable<DayType> GetAllActive(bool trackChanges = false) =>
		GetManyByCondition(x => x.IsActive.Equals(true), trackChanges);
	/// <inheritdoc/>
	public DayType GetByEnumerator(int enumerator, bool trackChanges = false) =>
		GetByCondition(x => x.Enumerator.Equals(enumerator), trackChanges);
	/// <inheritdoc/>
	public DayType GetByName(string name, bool trackChanges = false) =>
		GetByCondition(x => x.Name.Equals(name, StringComparison.Ordinal), trackChanges);
}
