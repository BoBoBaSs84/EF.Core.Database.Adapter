namespace Domain.Common.EntityBaseTypes.Interfaces;

/// <summary>
/// The audited interface.
/// </summary>
/// <remarks>
/// Derives from the following interfaces:
/// <list type="bullet">
/// <item>The <see cref="IIdentity{TKey}"/> interface</item>
/// <item>The <see cref="IConcurrency"/> interface</item>
/// </list>
/// </remarks>
public interface IAudited : IIdentity<int>, IConcurrency
{
	/// <summary>
	/// The created by property.
	/// </summary>
	int CreatedBy { get; }

	/// <summary>
	/// The modified by property.
	/// </summary>
	int? ModifiedBy { get; }
}
