using Application.Interfaces.Infrastructure.Repositories;
using Domain.Entities.Finance;
using Domain.Extensions;
using Infrastructure.Persistence.Repositories.BaseTypes;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

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
[SuppressMessage("Globalization", "CA1309",
	Justification = "Translation of the 'string.Equals' overload with a 'StringComparison' parameter is not supported.")]
internal sealed class CardRepository : IdentityRepository<Card>, ICardRepository
{
	/// <summary>
	/// Initializes a new instance of the <see cref="CardRepository"/> class.
	/// </summary>
	/// <param name="dbContext">The database context.</param>
	public CardRepository(DbContext dbContext) : base(dbContext)
	{
	}

	public async Task<Card> GetCardAsync(string pan,
		bool trackChanges = false, CancellationToken cancellationToken = default) =>
		await GetByConditionAsync(expression: x => x.PAN.Equals(pan.RemoveWhitespace()),
			trackChanges: trackChanges,
			cancellationToken: cancellationToken);

	public async Task<IEnumerable<Card>> GetCardsAsync(int userId,
		bool trackChanges = false, CancellationToken cancellationToken = default) =>
		await GetManyByConditionAsync(
			expression: x => x.UserId.Equals(userId),
			trackChanges: trackChanges,
			cancellationToken: cancellationToken);
}
