using BB84.Home.Domain.Entities.Attendance;
using BB84.Home.Domain.Enumerators.Attendance;
using BB84.Home.Domain.Extensions;

namespace BB84.Home.Domain.Tests.Extensions;

[TestClass]
public class ModelExtensionsTests : DomainTestBase
{
	[TestMethod]
	public void GetResultingWorkingHoursValueTest()
	{
		AttendanceEntity attendance = new()
		{
			Type = AttendanceType.Workday,
			StartTime = new(6, 0, 0),
			EndTime = new(15, 0, 0),
			BreakTime = new(1, 0, 0),
		};

		float? workingHours = attendance.GetResultingWorkingHours();

		Assert.AreEqual(8, workingHours);
	}

	[TestMethod]
	public void GetResultingWorkingHoursNotRelevantTest()
	{
		AttendanceEntity attendance = new()
		{
			Type = AttendanceType.Sickness
		};

		float? workingHours = attendance.GetResultingWorkingHours();

		Assert.IsNull(workingHours);
	}

	[TestMethod]
	public void GetResultingWorkingHoursNoEndTimeTest()
	{
		AttendanceEntity attendance = new()
		{
			Type = AttendanceType.Workday,
			StartTime = new(6, 0, 0)
		};

		float? workingHours = attendance.GetResultingWorkingHours();

		Assert.IsNull(workingHours);
	}

	[TestMethod]
	public void GetResultingWorkingHoursNoStartTimeTest()
	{
		AttendanceEntity attendance = new()
		{
			Type = AttendanceType.Workday,
			EndTime = new(15, 0, 0)
		};

		float? workingHours = attendance.GetResultingWorkingHours();

		Assert.IsNull(workingHours);
	}
}