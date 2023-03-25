using Application.Interfaces.Infrastructure.Persistence.Repositories;
using Domain.Entities.Finance;
using Infrastructure.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

/// <summary>
/// The card repository class.
/// </summary>
/// <remarks>
/// Derives from the <see cref="IdentityRepository{TEntity}"/> class and implements the interfaces:
/// <list type="bullet">
/// <item>The <see cref="IAccountRepository"/> interface</item>
/// </list>
/// </remarks>
internal sealed class CardRepository : IdentityRepository<Card>, ICardRepository
{
	/// <summary>
	/// Initializes a new instance of the <see cref="CardRepository"/> class.
	/// </summary>
	/// <param name="dbContext">The database context.</param>
	public CardRepository(DbContext dbContext) : base(dbContext)
	{
	}
}
