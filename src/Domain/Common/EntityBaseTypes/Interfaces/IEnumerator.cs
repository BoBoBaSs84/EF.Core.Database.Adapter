namespace Domain.Common.EntityBaseTypes.Interfaces;

/// <summary>
/// The enumerator interface.
/// </summary>
/// <remarks>
/// Derives from the following interfaces:
/// <list type="bullet">
/// <item>The <see cref="IIdentity{TKey}"/> interface</item>
/// <item>The <see cref="IActivatable"/> interface</item>
/// </list>
/// </remarks>
public interface IEnumerator : IIdentity<int>, IActivatable
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
