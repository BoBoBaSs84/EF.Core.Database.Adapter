namespace Domain.Common.EntityBaseTypes.Interfaces;

/// <summary>
/// The soft deleteable interface.
/// </summary>
internal interface ISoftDeleteable
{
	/// <summary>
	/// Indicates whether the data entry was deleted.
	/// </summary>
	bool IsDeleted { get; }
}
