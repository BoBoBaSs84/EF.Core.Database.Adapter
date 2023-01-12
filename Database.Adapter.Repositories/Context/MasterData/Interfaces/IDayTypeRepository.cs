using Database.Adapter.Entities.MasterData;
using Database.Adapter.Repositories.BaseTypes.Interfaces;

namespace Database.Adapter.Repositories.Context.MasterData.Interfaces;

/// <summary>
/// The day type repository interface.
/// </summary>
/// <remarks>
/// Inherits from the following interfaces:
/// <list type="bullet">
/// <item>The <see cref="IEnumeratorRepository{TEntity}"/> interface.</item>
/// </list>
/// </remarks>
public interface IDayTypeRepository : IEnumeratorRepository<DayType>
{
}
