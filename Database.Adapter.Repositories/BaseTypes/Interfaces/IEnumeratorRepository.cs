namespace Database.Adapter.Repositories.BaseTypes.Interfaces;

/// <summary>
/// The enumerator interface.
/// </summary>
/// <remarks>
/// Inherits from the following interfaces:
/// <list type="bullet">
/// <item>The <see cref="IGenericRepository{TEntity}"/> interface.</item>
/// </list>
/// </remarks>
public interface IEnumeratorRepository<TEnum> : IGenericRepository<TEnum> where TEnum : class
{
	/// <summary>
	/// The method should return only the active enumerators.
	/// </summary>
	/// <param name="trackChanges">Should changes be tracked?</param>
	/// <returns>All active enumerators.</returns>
	IQueryable<TEnum> GetAllActive(bool trackChanges = false);
	/// <summary>
	/// The method should return the enumerator by its unique name.
	/// </summary>
	/// <param name="name">The name of the enumerator.</param>
	/// <param name="trackChanges">Should changes be tracked?</param>
	/// <returns>The named enumerator.</returns>
	TEnum GetByName(string name, bool trackChanges = false);
}
