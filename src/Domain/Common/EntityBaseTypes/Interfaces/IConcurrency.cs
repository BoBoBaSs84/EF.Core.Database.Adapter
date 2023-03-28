namespace Domain.Common.EntityBaseTypes.Interfaces;

/// <summary>
/// The concurrency interface.
/// </summary>
public interface IConcurrency
{
	/// <summary>
	/// The timestamp property.
	/// </summary>
	public byte[] Timestamp { get; }
}
