namespace Domain.Interfaces.Models.Base;

/// <summary>
/// The soft deleteable base interface.
/// </summary>
public interface ISoftDeleteableBase
{
	/// <summary>
	/// Is the data row in a soft deleted?
	/// </summary>
	bool IsDeleted { get; set; }
}
