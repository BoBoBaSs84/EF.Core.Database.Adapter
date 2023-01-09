using Database.Adapter.Entities.BaseTypes;
using Database.Adapter.Repositories.BaseTypes.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Database.Adapter.Repositories.BaseTypes;

/// <summary>
/// The enumerator ropository class.
/// </summary>
/// <remarks>
/// Inherits from the following class:
/// <list type="bullet">
/// <item>The <see cref="GenericRepository{TEntity}"/>class</item>
/// </list>
/// Implements the following interfaces:
/// <list type="bullet">
/// <item>The <see cref="IEnumeratorRepository{TEnum}"/> interface</item>
/// </list>
/// </remarks>
/// <typeparam name="TEnum"></typeparam>
[SuppressMessage("Globalization", "CA1309", Justification = "Translation of the 'string.Equals' overload with a 'StringComparison' parameter is not supported.")]
internal abstract class EnumeratorRepository<TEnum> : GenericRepository<TEnum>, IEnumeratorRepository<TEnum> where TEnum : EnumeratorModel
{
	/// <summary>
	/// Initializes a new instance of the <see cref="EnumeratorRepository{TEnum}"/> class.
	/// </summary>
	/// <param name="dbContext">The database context.</param>
	protected EnumeratorRepository(DbContext dbContext) : base(dbContext)
	{
	}
	/// <inheritdoc/>
	public IQueryable<TEnum> GetAllActive(bool trackChanges = false) =>
		GetManyByCondition(x => x.IsActive.Equals(true), trackChanges);
	/// <inheritdoc/>
	public TEnum GetByName(string name, bool trackChanges = false) =>
		GetByCondition(x => x.Name.Equals(name), trackChanges);
}
