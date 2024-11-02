namespace Application.Interfaces.Application.Providers;

/// <summary>
/// The interface for the date time provider.
/// </summary>
public interface IDateTimeProvider
{
	/// <summary>
	/// The current date.
	/// </summary>
	DateTime Today { get; }

	/// <summary>
	/// The current time.
	/// </summary>
	TimeSpan TimeOfDay { get; }

	/// <summary>
	/// The current date time.
	/// </summary>
	DateTime Now { get; }

	/// <summary>
	/// The current utc date time.
	/// </summary>
	DateTime UtcNow { get; }
}
