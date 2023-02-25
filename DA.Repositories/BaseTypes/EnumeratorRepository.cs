using DA.Domain.Models.BaseTypes;
using DA.Repositories.BaseTypes.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace DA.Repositories.BaseTypes;

/// <summary>
/// The enumerator repository class.
/// </summary>
/// <remarks>
/// Inherits from the <see cref="GenericRepository{TEntity}"/> class and implements the interfaces:
/// <list type="bullet">
/// <item>The <see cref="IEntityTypeConfiguration{TEntity}"/> interface</item>
/// </list>
/// </remarks>
/// <typeparam name="TEnumeratorEntity"></typeparam>
[SuppressMessage("Globalization", "CA1309", Justification = "Translation of the 'string.Equals' overload with a 'StringComparison' parameter is not supported.")]
internal abstract class EnumeratorRepository<TEnumeratorEntity> : GenericRepository<TEnumeratorEntity>, IEnumeratorRepository<TEnumeratorEntity> where TEnumeratorEntity : EnumeratorModel
{
	/// <summary>
	/// Initializes a new instance of the <see cref="EnumeratorRepository{TEnum}"/> class.
	/// </summary>
	/// <inheritdoc/>
	protected EnumeratorRepository(DbContext dbContext) : base(dbContext)
	{
	}

	public async Task<IEnumerable<TEnumeratorEntity>> GetAllActiveAsync(bool trackChanges = false, CancellationToken cancellationToken = default) =>
		await GetManyByConditionAsync(
			expression: x => x.IsActive.Equals(true),
			trackChanges: trackChanges,
			cancellationToken: cancellationToken);

	public async Task<TEnumeratorEntity> GetByNameAsync(string name, bool trackChanges = false, CancellationToken cancellationToken = default) =>
		await GetByConditionAsync(
			expression: x => x.Name.Equals(name),
			trackChanges: trackChanges,
			cancellationToken: cancellationToken);

	public async Task<IEnumerable<TEnumeratorEntity>> GetByNamesAsync(IEnumerable<string> names, bool trackChanges = false, CancellationToken cancellationToken = default) =>
		await GetManyByConditionAsync(
			expression: x => names.Contains(x.Name),
			trackChanges: trackChanges,
			cancellationToken: cancellationToken);
}
