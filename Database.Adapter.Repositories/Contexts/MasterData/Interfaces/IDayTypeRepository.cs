using Database.Adapter.Entities.Contexts.MasterData;
using Database.Adapter.Repositories.BaseTypes.Interfaces;

namespace Database.Adapter.Repositories.Contexts.MasterData.Interfaces;

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
