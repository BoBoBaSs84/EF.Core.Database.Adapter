using Domain.Enumerators;
using Domain.Extensions;
using Domain.Models.Attendance;

namespace DomainTests.Extensions;

[TestClass]
public class ModelExtensionsTests : DomainTestBase
{
	[TestMethod]
	public void GetResultingWorkingHoursValueTest()
	{
		AttendanceModel attendance = new()
		{
			Type = AttendanceType.WORKDAY,
			StartTime = new(6, 0, 0),
			EndTime = new(15, 0, 0),
			BreakTime = new(1, 0, 0),
		};

		float workingHours = attendance.GetResultingWorkingHours();

		Assert.AreEqual(8, workingHours);
	}

	[TestMethod]
	public void GetResultingWorkingHoursNotRelevantTest()
	{
		AttendanceModel attendance = new()
		{
			Type = AttendanceType.SICKNESS
		};

		float workingHours = attendance.GetResultingWorkingHours();

		Assert.AreEqual(0, workingHours);
	}

	[TestMethod]
	public void GetResultingWorkingHoursNoEndTimeTest()
	{
		AttendanceModel attendance = new()
		{
			Type = AttendanceType.WORKDAY,
			StartTime = new(6, 0, 0)
		};

		float workingHours = attendance.GetResultingWorkingHours();

		Assert.AreEqual(0, workingHours);
	}

	[TestMethod]
	public void GetResultingWorkingHoursNoStartTimeTest()
	{
		AttendanceModel attendance = new()
		{
			Type = AttendanceType.WORKDAY,
			EndTime = new(15, 0, 0)
		};

		float workingHours = attendance.GetResultingWorkingHours();

		Assert.AreEqual(0, workingHours);
	}
}