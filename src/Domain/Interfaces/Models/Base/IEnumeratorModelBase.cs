namespace Domain.Interfaces.Models.Base;

/// <summary>
/// The enumerator model base interface.
/// </summary>
public interface IEnumeratorModelBase
{
	/// <summary>
	/// The name of the enumerator.
	/// </summary>
	string Name { get; }

	/// <summary>
	/// The description of the enumerator.
	/// </summary>
	string Description { get; }
}
