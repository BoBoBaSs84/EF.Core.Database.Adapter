namespace Domain.Common.EntityBaseTypes.Interfaces;

/// <summary>
/// The enumerator model interface.
/// </summary>
internal interface IEnumeratorModel
{
	/// <summary>
	/// The <see cref="Name"/> property.
	/// </summary>
	string Name { get; }
	/// <summary>
	/// The <see cref="Description"/> property.
	/// </summary>
	string? Description { get; }
}
