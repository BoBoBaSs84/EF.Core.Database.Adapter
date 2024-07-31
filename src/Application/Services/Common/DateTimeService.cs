using Application.Interfaces.Application.Common;

namespace Application.Services.Common;

/// <summary>
/// The date time service class.
/// </summary>
/// <remarks>
/// Implements the members of the <see cref="IDateTimeService"/> interface.
/// </remarks>
internal sealed class DateTimeService : IDateTimeService
{
	public DateTime Today => DateTime.Today;
	public TimeSpan TimeOfDay => DateTime.Now.TimeOfDay;
	public DateTime Now => DateTime.Now;
	public DateTime UtcNow => DateTime.UtcNow;
}
