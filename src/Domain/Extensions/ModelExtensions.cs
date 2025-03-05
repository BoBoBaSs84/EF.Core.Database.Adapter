using BB84.Home.Domain.Entities.Attendance;

namespace BB84.Home.Domain.Extensions;

/// <summary>
/// The model extensions class.
/// </summary>
public static partial class ModelExtensions
{
	/// <summary>
	/// Returns the resulting working hours based on the model information.
	/// </summary>
	/// <param name="model">The attendance model to work with.</param>
	/// <returns>The resulting working hours.</returns>
	public static float? GetResultingWorkingHours(this AttendanceEntity model)
	{
		if (model.Type.IsWorkingHoursRelevant().Equals(false))
			return default;

		if (model.StartTime is null || model.EndTime is null)
			return default;

		float breakTimeInSeconds = default;
		float endTimeInSeconds = (float)model.EndTime.Value.TotalSeconds;
		float startTimeInSeconds = (float)model.StartTime.Value.TotalSeconds;

		if (model.BreakTime is not null)
			breakTimeInSeconds = (float)model.BreakTime.Value.TotalSeconds;

		return (endTimeInSeconds - startTimeInSeconds - breakTimeInSeconds) / 60 / 60;
	}
}
