namespace Domain.Common.EntityBaseTypes.Interfaces;

/// <summary>
/// The soft deleteable interface.
/// </summary>
public interface ISoftDeleteable
{
	/// <summary>
	/// Indicates whether the data entry was deleted or not.
	/// </summary>
	bool IsDeleted { get; }
}
