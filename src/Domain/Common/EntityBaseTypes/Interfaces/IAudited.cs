namespace Domain.Common.EntityBaseTypes.Interfaces;

/// <summary>
/// The audited model interface.
/// </summary>
internal interface IAudited
{
	/// <summary>
	/// The <see cref="CreatedBy"/> property.
	/// </summary>
	int CreatedBy { get; }

	/// <summary>
	/// The <see cref="ModifiedBy"/> property.
	/// </summary>
	int? ModifiedBy { get; }
}
