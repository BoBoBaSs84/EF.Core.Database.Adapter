namespace Domain.Common.EntityBaseTypes.Interfaces;

/// <summary>
/// The activatable interface.
/// </summary>
public interface IActivatable
{
	/// <summary>
	/// Indicates whether the data entry is active or not.
	/// </summary>
	bool IsActive { get; }
}
