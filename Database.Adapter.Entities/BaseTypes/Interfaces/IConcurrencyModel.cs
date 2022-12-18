namespace Database.Adapter.Entities.BaseTypes.Interfaces;

internal interface IConcurrencyModel
{
	/// <summary>The <see cref="Timestamp"/> property.</summary>
	public byte[] Timestamp { get; }
	/// <summary>
	/// Should return false.
	/// </summary>
	/// <returns><see langword="false"/></returns>	
	bool ShouldSerializeTimestamp();
}
