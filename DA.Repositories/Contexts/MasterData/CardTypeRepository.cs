using DA.Models.Contexts.MasterData;
using DA.Repositories.BaseTypes;
using DA.Repositories.Contexts.MasterData.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DA.Repositories.Contexts.MasterData;

/// <summary>
/// The card type repository class.
/// </summary>
/// <remarks>
/// Inherits from the <see cref="EnumeratorRepository{TEnum}"/> class and implements the interfaces:
/// <list type="bullet">
/// <item>The <see cref="ICardTypeRepository"/> interface</item>
/// </list>
/// </remarks>
internal sealed class CardTypeRepository : EnumeratorRepository<CardType>, ICardTypeRepository
{
	/// <summary>
	/// Initializes a new instance of the <see cref="CardTypeRepository"/> class.
	/// </summary>
	/// <param name="dbContext">The database context.</param>
	public CardTypeRepository(DbContext dbContext) : base(dbContext)
	{
	}
}
