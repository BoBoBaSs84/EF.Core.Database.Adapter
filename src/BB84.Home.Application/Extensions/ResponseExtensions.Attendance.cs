using BB84.Home.Application.Contracts.Responses.Attendance;
using BB84.Home.Domain.Entities.Attendance;
using BB84.Home.Domain.Extensions;

namespace BB84.Home.Application.Extensions;

internal static partial class ResponseExtensions
{
	/// <summary>
	/// Converts an <see cref="AttendanceEntity"/> to an <see cref="AttendanceResponse"/>.
	/// </summary>
	/// <param name="entity">The <see cref="AttendanceEntity"/> to convert.</param>
	/// <returns>The converted <see cref="AttendanceResponse"/>.</returns>
	public static AttendanceResponse ToResponse(this AttendanceEntity entity)
	{
		AttendanceResponse response = new()
		{
			Id = entity.Id,
			Date = entity.Date,
			Type = entity.Type,
			StartTime = entity.StartTime,
			EndTime = entity.EndTime,
			BreakTime = entity.BreakTime,
			WorkingHours = entity.GetResultingWorkingHours()
		};

		return response;
	}
}
