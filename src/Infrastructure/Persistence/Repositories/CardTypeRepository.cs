using Application.Interfaces.Infrastructure.Persistence.Repositories;
using Domain.Entities.Enumerator;
using Infrastructure.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

/// <summary>
/// The card type repository class.
/// </summary>
/// <remarks>
/// Derives from the <see cref="GenericRepository{TEntity}"/> class and implements the interfaces:
/// <list type="bullet">
/// <item>The <see cref="ICardTypeRepository"/> interface</item>
/// </list>
/// </remarks>
internal sealed class CardTypeRepository : GenericRepository<CardType>, ICardTypeRepository
{
	/// <summary>
	/// Initializes a new instance of the <see cref="CardTypeRepository"/> class.
	/// </summary>
	/// <param name="dbContext">The database context.</param>
	public CardTypeRepository(DbContext dbContext) : base(dbContext)
	{
	}
}
