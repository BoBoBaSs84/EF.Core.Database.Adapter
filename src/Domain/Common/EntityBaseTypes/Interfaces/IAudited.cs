namespace Domain.Common.EntityBaseTypes.Interfaces;

/// <summary>
/// The audited interface.
/// </summary>
/// <remarks>
/// Derives from the <see cref="ISoftDeleteable"/> interface.
/// </remarks>
public interface IAudited
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
