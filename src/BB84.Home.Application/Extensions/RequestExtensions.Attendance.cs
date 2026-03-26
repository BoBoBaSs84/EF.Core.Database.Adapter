using BB84.Home.Application.Contracts.Requests.Attendance;
using BB84.Home.Domain.Entities.Attendance;

namespace BB84.Home.Application.Extensions;

internal static partial class RequestExtensions
{
	public static AttendanceEntity ToEntity(this AttendanceCreateRequest request)
	{
		AttendanceEntity entity = new()
		{
			Date = request.Date,
			Type = request.Type,
			StartTime = request.StartTime,
			EndTime = request.EndTime,
			BreakTime = request.BreakTime
		};

		return entity;
	}

	public static AttendanceEntity ToEntity(this AttendanceCreateRequest request, Guid userId)
	{
		AttendanceEntity entity = request.ToEntity();
		entity.UserId = userId;

		return entity;
	}

	public static AttendanceEntity ToEntity(this AttendanceUpdateRequest request, AttendanceEntity entity)
	{
		entity.Type = request.Type;
		entity.StartTime = request.StartTime;
		entity.EndTime = request.EndTime;
		entity.BreakTime = request.BreakTime;

		return entity;
	}
}
