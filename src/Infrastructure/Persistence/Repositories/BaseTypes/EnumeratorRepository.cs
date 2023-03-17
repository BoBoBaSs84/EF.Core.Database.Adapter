using Application.Interfaces.Infrastructure.Repositories.BaseTypes;
using Domain.Common.EntityBaseTypes;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Infrastructure.Persistence.Repositories.BaseTypes;

/// <summary>
/// The enumerator repository class.
/// </summary>
/// <remarks>
/// Derives from the <see cref="GenericRepository{TEntity}"/> class and implements the interfaces:
/// <list type="bullet">
/// <item>The <see cref="IEntityTypeConfiguration{TEntity}"/> interface</item>
/// </list>
/// </remarks>
/// <typeparam name="TEntity">The enumerator entity to work with.</typeparam>
[SuppressMessage("Globalization", "CA1309",
	Justification = "Translation of the 'string.Equals' overload with a 'StringComparison' parameter is not supported.")]
internal abstract class EnumeratorRepository<TEntity> : GenericRepository<TEntity>, IEnumeratorRepository<TEntity> where TEntity : EnumeratorModel
{
	/// <summary>
	/// Initializes a new instance of the <see cref="EnumeratorRepository{TEntity}"/> class.
	/// </summary>
	/// <inheritdoc/>
	protected EnumeratorRepository(DbContext dbContext) : base(dbContext)
	{
	}

	public async Task<IEnumerable<TEntity>> GetAllActiveAsync(bool trackChanges = false, CancellationToken cancellationToken = default) =>
		await GetManyByConditionAsync(
			expression: x => x.IsActive.Equals(true),
			trackChanges: trackChanges,
			cancellationToken: cancellationToken);

	public async Task<TEntity> GetByNameAsync(string name, bool trackChanges = false, CancellationToken cancellationToken = default) =>
		await GetByConditionAsync(
			expression: x => x.Name.Equals(name),
			trackChanges: trackChanges,
			cancellationToken: cancellationToken);

	public async Task<IEnumerable<TEntity>> GetByNamesAsync(IEnumerable<string> names, bool trackChanges = false, CancellationToken cancellationToken = default) =>
		await GetManyByConditionAsync(
			expression: x => names.Contains(x.Name),
			trackChanges: trackChanges,
			cancellationToken: cancellationToken);
}
