namespace DA.Models.BaseTypes.Interfaces;

/// <summary>
/// The <see cref="IEnumeratorModel"/> interface.
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
