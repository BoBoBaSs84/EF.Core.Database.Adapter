using Database.Adapter.Entities.MasterData;
using Database.Adapter.Repositories.BaseTypes.Interfaces;

namespace Database.Adapter.Repositories.MasterData.Interfaces;

/// <summary>
/// The day type repository interface.
/// </summary>
/// <remarks>
/// Inherits from the following interfaces:
/// <list type="bullet">
/// <item>The <see cref="IGenericRepository{TEntity}"/> interface. The entity is <see cref="DayType"/>.</item>
/// </list>
/// </remarks>
public interface IDayTypeRepository : IGenericRepository<DayType>
{
	/// <summary>
	/// The method should return only the active day types.
	/// </summary>
	/// <param name="trackChanges">Should changes be tracked?</param>
	/// <returns>All active day types.</returns>
	IEnumerable<DayType> GetAllActive(bool trackChanges = false);
	/// <summary>
	/// The method should return the day type by its enumerator.
	/// </summary>
	/// <param name="enumerator">The integer value of the day type enum.</param>
	/// <param name="trackChanges">Should changes be tracked?</param>
	/// <returns>The named day type.</returns>
	DayType GetByEnumerator(int enumerator, bool trackChanges = false);
	/// <summary>
	/// The method should return the day type by its unique name.
	/// </summary>
	/// <param name="name">The name of the day type.</param>
	/// <param name="trackChanges">Should changes be tracked?</param>
	/// <returns>The named day type.</returns>
	DayType GetByName(string name, bool trackChanges = false);
}
