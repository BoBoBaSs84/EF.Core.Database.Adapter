namespace Database.Adapter.Entities.BaseTypes.Interfaces;

/// <summary>
/// The <see cref="IEnumeratorModel"/> interface.
/// </summary>
internal interface IEnumeratorModel
{
	/// <summary>
	/// The <see cref="Id"/> property.
	/// </summary>
	public int Id { get; set; }
	/// <summary>
	/// The <see cref="Name"/> property.
	/// </summary>
	string Name { get; }
	/// <summary>
	/// The <see cref="Description"/> property.
	/// </summary>
	string? Description { get; }
	/// <summary>
	/// Should return false, if the <see cref="Description"/> property is <see langword="null"/>.
	/// </summary>
	/// <returns><see langword="true"/> or <see langword="false"/></returns>
	bool ShouldSerializeDescription();
}
