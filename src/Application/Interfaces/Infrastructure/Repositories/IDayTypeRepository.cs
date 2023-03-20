using Application.Interfaces.Infrastructure.Repositories.BaseTypes;
using Domain.Entities.Enumerator;

namespace Application.Interfaces.Infrastructure.Repositories;

/// <summary>
/// The day type repository interface.
/// </summary>
/// <remarks>
/// Derives from the following interfaces:
/// <list type="bullet">
/// <item>The <see cref="IGenericRepository{TEntity}"/> interface</item>
/// </list>
/// </remarks>
public interface IDayTypeRepository : IGenericRepository<DayType>
{
}
