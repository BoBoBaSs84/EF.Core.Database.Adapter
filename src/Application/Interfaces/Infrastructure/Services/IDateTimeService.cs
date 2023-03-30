namespace Application.Interfaces.Infrastructure.Services;

/// <summary>
/// The date time service interface.
/// </summary>
public interface IDateTimeService
{
	/// <summary>
	/// The current date.
	/// </summary>
	DateTime Today { get; }

	/// <summary>
	/// The current date time.
	/// </summary>
	DateTime Now { get; }

	/// <summary>
	/// The current utc date time.
	/// </summary>
	DateTime UtcNow { get; }
}
