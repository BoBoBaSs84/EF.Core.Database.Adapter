namespace Domain.Interfaces.Models.Base;

/// <summary>
/// The audited model base interface.
/// </summary>
/// <typeparam name="CKey">The key to identify.</typeparam>
/// <typeparam name="MKey">The key to identify.</typeparam>
public interface IAuditedModelBase<CKey, MKey>
{
	/// <summary>
	/// The key that uniquely identify who created the data row.
	/// </summary>
	CKey CreatedBy { get; set; }

	/// <summary>
	/// The key that uniquely identify who last modified the data row.
	/// </summary>
	MKey ModifiedBy { get; set; }
}
