namespace Domain.Interfaces.Models;

/// <summary>
/// The audited interface.
/// </summary>
public interface IAuditedModel
{
	/// <summary>
	/// The created by property.
	/// </summary>
	Guid CreatedBy { get; }

	/// <summary>
	/// The modified by property.
	/// </summary>
	Guid? ModifiedBy { get; }
}
