using DA.Models.Contexts.MasterData;
using DA.Repositories.BaseTypes.Interfaces;

namespace DA.Repositories.Contexts.MasterData.Interfaces;

/// <summary>
/// The card type repository interface.
/// </summary>
/// <remarks>
/// Inherits from the following interfaces:
/// <list type="bullet">
/// <item>The <see cref="IEnumeratorRepository{TEntity}"/> interface.</item>
/// </list>
/// </remarks>
public interface ICardTypeRepository : IEnumeratorRepository<CardType>
{
}
