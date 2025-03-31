using BB84.Home.Application.Interfaces.Application.Providers;

namespace BB84.Home.Application.Providers;

/// <summary>
/// The date time provider class.
/// </summary>
[ExcludeFromCodeCoverage(Justification = "Wrapper class.")]
internal sealed class DateTimeProvider : IDateTimeProvider
{
	public DateTime Today => DateTime.Today;
	public TimeSpan TimeOfDay => DateTime.Now.TimeOfDay;
	public DateTime Now => DateTime.Now;
	public DateTime UtcNow => DateTime.UtcNow;
}
