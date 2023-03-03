namespace Domain.Common.EntityBaseTypes.Interfaces;

/// <summary>
/// The activatable model interface.
/// </summary>
internal interface IActivatableModel
{
	/// <summary>The <see cref="IsActive"/> property.</summary>
	bool IsActive { get; }
}
