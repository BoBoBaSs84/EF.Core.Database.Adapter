namespace Domain.Interfaces.Models;

/// <summary>
/// The concurrency interface.
/// </summary>
public interface IConcurrencyModel
{
	/// <summary>
	/// The timestamp property.
	/// </summary>
	public byte[] Timestamp { get; }
}
