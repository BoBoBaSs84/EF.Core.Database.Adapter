namespace Domain.Common.EntityBaseTypes.Interfaces;

/// <summary>
/// The concurrency model interface.
/// </summary>
internal interface IConcurrency
{
	/// <summary>
	/// The <see cref="Timestamp"/> property.
	/// </summary>
	public byte[] Timestamp { get; }
}
