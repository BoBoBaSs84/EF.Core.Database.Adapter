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
}
