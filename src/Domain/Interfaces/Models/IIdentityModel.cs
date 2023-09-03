namespace Domain.Interfaces.Models;

/// <summary>
/// The identity model interface.
/// </summary>
public interface IIdentityModel
{
	/// <summary>
	/// The identifier property.
	/// </summary>
	/// <remarks>
	/// This is the primary key of the database table.
	/// </remarks>
	Guid Id { get; }
}
