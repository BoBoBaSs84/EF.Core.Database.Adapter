namespace Database.Adapter.Entities.BaseTypes.Interfaces;

internal interface IConcurrencyModel
{
	/// <summary>The <see cref="Timestamp"/> property.</summary>
	public byte[] Timestamp { get; }
}
