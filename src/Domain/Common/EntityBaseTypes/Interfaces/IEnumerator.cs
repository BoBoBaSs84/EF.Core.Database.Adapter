namespace Domain.Common.EntityBaseTypes.Interfaces;

/// <summary>
/// The enumerator interface.
/// </summary>
/// <remarks>
/// Derives from the <see cref="IIdentity{TKey}"/> interface.
/// </remarks>
public interface IEnumerator : IIdentity<int>
{
	/// <summary>
	/// The enumerator name property.
	/// </summary>
	string Name { get; }

	/// <summary>
	/// The enumerator description property.
	/// </summary>
	string? Description { get; }
}
