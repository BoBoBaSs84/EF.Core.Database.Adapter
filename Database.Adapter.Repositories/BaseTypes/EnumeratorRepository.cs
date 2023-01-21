using Database.Adapter.Entities.BaseTypes;
using Database.Adapter.Repositories.BaseTypes.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Database.Adapter.Repositories.BaseTypes;

/// <summary>
/// The enumerator repository class.
/// </summary>
/// <remarks>
/// Inherits from the <see cref="GenericRepository{TEntity}"/> class and implements the interfaces:
/// <list type="bullet">
/// <item>The <see cref="IEntityTypeConfiguration{TEntity}"/> interface</item>
/// </list>
/// </remarks>
/// <typeparam name="TEntity"></typeparam>
[SuppressMessage("Globalization", "CA1309", Justification = "Translation of the 'string.Equals' overload with a 'StringComparison' parameter is not supported.")]
internal abstract class EnumeratorRepository<TEntity> : GenericRepository<TEntity>, IEnumeratorRepository<TEntity> where TEntity : EnumeratorModel
{
	/// <summary>
	/// Initializes a new instance of the <see cref="EnumeratorRepository{TEnum}"/> class.
	/// </summary>
	/// <param name="dbContext">The database context.</param>
	protected EnumeratorRepository(DbContext dbContext) : base(dbContext)
	{
	}

	public async Task<IEnumerable<TEntity>> GetAllActiveAsync(bool trackChanges = false, CancellationToken cancellationToken = default) =>
		await GetManyByConditionAsync(
			expression: x =>x.IsActive.Equals(true),
			trackChanges: trackChanges,
			cancellationToken: cancellationToken);

	public async Task<TEntity> GetByNameAsync(string name, bool trackChanges = false, CancellationToken cancellationToken = default) =>
		await GetByConditionAsync(
			expression: x => x.Name.Equals(name),
			trackChanges: trackChanges,
			cancellationToken: cancellationToken);
}
