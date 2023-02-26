using Application.Common.Interfaces.Repositories.BaseTypes;
using Domain.Entities.Enumerator;

namespace Application.Common.Interfaces.Repositories;

/// <summary>
/// The day type repository interface.
/// </summary>
/// <remarks>
/// Derives from the following interfaces:
/// <list type="bullet">
/// <item>The <see cref="IEnumeratorRepository{TEntity}"/> interface</item>
/// </list>
/// </remarks>
public interface IDayTypeRepository : IEnumeratorRepository<DayType>
{
}
