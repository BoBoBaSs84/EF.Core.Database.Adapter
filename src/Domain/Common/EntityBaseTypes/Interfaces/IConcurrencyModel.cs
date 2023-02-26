namespace Domain.Common.EntityBaseTypes.Interfaces;

/// <summary>
/// The concurrency model interface.
/// </summary>
internal interface IConcurrencyModel
{
	/// <summary>
	/// The <see cref="Timestamp"/> property.
	/// </summary>
	public byte[] Timestamp { get; }
}
