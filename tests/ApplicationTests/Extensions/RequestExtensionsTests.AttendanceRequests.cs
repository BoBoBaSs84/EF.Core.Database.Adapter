using Application.Contracts.Requests.Attendance;
using Application.Extensions;

using Domain.Enumerators;

namespace ApplicationTests.Extensions;

public partial class RequestExtensionsTests
{
	[TestMethod]
	public void AttendanceCreateRequestIsValidTest()
	{
		AttendanceCreateRequest request = new()
		{
			Type = AttendanceType.WORKDAY,
			StartTime = new(6, 0, 0),
			EndTime = new(14, 30, 0),
			BreakTime = new(0, 45, 0)
		};

		bool valid = request.IsValid();

		Assert.IsTrue(valid);
	}

	[TestMethod]
	public void AttendanceCreateRequestNoStartTimeTest()
	{
		AttendanceCreateRequest request = new()
		{
			Type = AttendanceType.WORKDAY,
			EndTime = new(14, 30, 0),
			BreakTime = new(0, 45, 0)
		};

		bool valid = request.IsValid();

		Assert.IsFalse(valid);
	}

	[TestMethod]
	public void AttendanceCreateRequestNoEndTimeTest()
	{
		AttendanceCreateRequest request = new()
		{
			Type = AttendanceType.BUISNESSTRIP,
			StartTime = new(6, 0, 0),
			BreakTime = new(0, 45, 0)
		};

		bool valid = request.IsValid();

		Assert.IsFalse(valid);
	}

	[TestMethod]
	public void AttendanceUpdateRequestIsValidTest()
	{
		AttendanceUpdateRequest request = new()
		{
			Type = AttendanceType.WORKDAY,
			StartTime = new(6, 0, 0),
			EndTime = new(14, 30, 0),
			BreakTime = new(0, 45, 0)
		};

		bool valid = request.IsValid();

		Assert.IsTrue(valid);
	}

	[TestMethod]
	public void AttendanceUpdateRequestWrongTypeTest()
	{
		AttendanceUpdateRequest request = new()
		{
			Type = AttendanceType.VACATION,
			StartTime = new(6, 0, 0),
			EndTime = new(14, 30, 0),
			BreakTime = new(0, 45, 0)
		};

		bool valid = request.IsValid();

		Assert.IsFalse(valid);
	}

	[TestMethod]
	public void AttendanceUpdateRequestWrongTimeTest()
	{
		AttendanceUpdateRequest request = new()
		{
			Type = AttendanceType.MOBILEWORKING,
			EndTime = new(6, 0, 0),
			StartTime = new(14, 30, 0),
			BreakTime = new(0, 45, 0)
		};

		bool valid = request.IsValid();

		Assert.IsFalse(valid);
	}
}