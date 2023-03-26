namespace Domain.Common.EntityBaseTypes.Interfaces;

/// <summary>
/// The activatable interface.
/// </summary>
internal interface IActivatable
{
	/// <summary>The <see cref="IsActive"/> property.</summary>
	bool IsActive { get; }
}
