using DA.Domain.Models.MasterData;
using DA.Repositories.BaseTypes.Interfaces;

namespace DA.Repositories.Contexts.MasterData.Interfaces;

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
