using Application.Contracts.Requests.Attendance;
using Application.Contracts.Responses.Attendance;
using Application.Errors.Services;
using Application.Interfaces.Application;

using AutoMapper;

using BaseTests.Helpers;

using Domain.Enumerators;
using Domain.Errors;
using Domain.Models.Attendance;
using Domain.Models.Identity;
using Domain.Results;

using FluentAssertions;

namespace ApplicationTests.Services;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, UnitTest.")]
public sealed class AttendanceServiceTests : ApplicationTestBase
{
	private readonly IAttendanceService _attendanceService;
	private readonly IMapper _mapper;

	public AttendanceServiceTests()
	{
		_attendanceService = GetService<IAttendanceService>();
		_mapper = GetService<IMapper>();
	}

	[TestMethod]
	public async Task CreateBadRequest()
	{
		(Guid userId, _, _, _) = GetUserAttendance();
		AttendanceCreateRequest request = GetCreateRequest();
		request.AttendanceType = AttendanceType.HOLIDAY;

		ErrorOr<Created> result =
			await _attendanceService.Create(userId, request);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeTrue();
			result.Errors.Should().HaveCount(1);
			result.FirstError.Should().Be(AttendanceServiceErrors.CreateBadRequest(request.Date));
		});
	}

	[TestMethod]
	public async Task CreateNotFound()
	{
		(Guid userId, _, _, _) = GetUserAttendance();
		AttendanceCreateRequest request = GetCreateRequest();
		request.Date = DateTime.MinValue;

		ErrorOr<Created> result =
			await _attendanceService.Create(userId, request);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeTrue();
			result.Errors.Should().HaveCount(1);
			result.FirstError.Should().Be(CalendarServiceErrors.GetByDateNotFound(request.Date));
		});
	}

	[TestMethod]
	public async Task CreateConflict()
	{
		(Guid userId, _, _, DateTime date) = GetUserAttendance();
		AttendanceCreateRequest request = GetCreateRequest();
		request.Date = date;

		ErrorOr<Created> result =
			await _attendanceService.Create(userId, request);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeTrue();
			result.Errors.Should().HaveCount(1);
			result.FirstError.Should().Be(AttendanceServiceErrors.CreateConflict(request.Date));
		});
	}

	[TestMethod]
	public async Task CreateSuccess()
	{
		(Guid userId, _, _, _) = GetUserAttendance();
		AttendanceCreateRequest request = GetCreateRequest();

		ErrorOr<Created> result =
			await _attendanceService.Create(userId, request);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().Be(Result.Created);
		});
	}

	[TestMethod]
	public async Task DeleteByCalendarIdNotFound()
	{
		(Guid userId, Guid calendarId, _, _) = GetUserAttendance();
		calendarId = default;

		ErrorOr<Deleted> result =
			await _attendanceService.Delete(userId, calendarId);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeTrue();
			result.Errors.Should().HaveCount(1);
			result.FirstError.Should().Be(AttendanceServiceErrors.DeleteNotFound);
		});
	}

	[TestMethod]
	public async Task DeleteByCalendarIdSuccess()
	{
		(Guid userId, Guid calendarId, _, _) = GetUserAttendance();

		ErrorOr<Deleted> result =
			await _attendanceService.Delete(userId, calendarId);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().Be(Result.Deleted);
		});
	}

	[TestMethod]
	public async Task DeleteByCalendarIdsNotFound()
	{
		(Guid userId, Guid calendarId, _, _) = GetUserAttendance();
		calendarId = default;

		Guid[] guids = new Guid[1] { calendarId };

		ErrorOr<Deleted> result =
			await _attendanceService.Delete(userId, guids);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeTrue();
			result.Errors.Should().HaveCount(1);
			result.FirstError.Should().Be(AttendanceServiceErrors.DeleteManyNotFound);
		});
	}

	[TestMethod]
	public async Task DeleteByCalendarIdsSuccess()
	{
		(Guid userId, Guid calendarId, _, _) = GetUserAttendance();

		Guid[] guids = new Guid[1] { calendarId };

		ErrorOr<Deleted> result =
			await _attendanceService.Delete(userId, guids);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().Be(Result.Deleted);
		});
	}

	[TestMethod]
	public async Task GetByCalendarDateNotFound()
	{
		(Guid userId, _, _, DateTime date) = GetUserAttendance();
		date = DateTime.MinValue;

		ErrorOr<AttendanceResponse> result =
			await _attendanceService.Get(userId, date);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeTrue();
			result.Errors.Should().HaveCount(1);
			result.FirstError.Should().Be(AttendanceServiceErrors.GetByDateNotFound(date));
		});
	}

	[TestMethod]
	public async Task GetByCalendarDateSuccess()
	{
		(Guid userId, _, _, DateTime date) = GetUserAttendance();

		ErrorOr<AttendanceResponse> result =
			await _attendanceService.Get(userId, date);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().NotBeNull();
		});
	}

	[TestMethod]
	public async Task GetByCalendarIdNotFound()
	{
		(Guid userId, Guid calendarId, _, _) = GetUserAttendance();
		calendarId = default;

		ErrorOr<AttendanceResponse> result =
			await _attendanceService.Get(userId, calendarId);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeTrue();
			result.Errors.Should().HaveCount(1);
			result.FirstError.Should().Be(AttendanceServiceErrors.GetByIdNotFound(calendarId));
		});
	}

	[TestMethod]
	public async Task GetByCalendarIdSuccess()
	{
		(Guid userId, Guid calendarId, _, _) = GetUserAttendance();

		ErrorOr<AttendanceResponse> result =
			await _attendanceService.Get(userId, calendarId);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().NotBeNull();
		});
	}

	[TestMethod]
	public async Task UpdateBadRequest()
	{
		AttendanceUpdateRequest request = GetUpdateRequest();
		request.AttendanceType = AttendanceType.HOLIDAY;
		request.StartTime = new(6, 0, 0);

		ErrorOr<Updated> result =
			await _attendanceService.Update(request);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeTrue();
			result.Errors.Should().HaveCount(1);
			result.FirstError.Should().Be(AttendanceServiceErrors.UpdateBadRequest(request.Id));
		});
	}

	[TestMethod]
	public async Task UpdateNotFound()
	{
		AttendanceUpdateRequest request = GetUpdateRequest();
		request.Id = default;

		ErrorOr<Updated> result =
			await _attendanceService.Update(request);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeTrue();
			result.Errors.Should().HaveCount(1);
			result.FirstError.Should().Be(AttendanceServiceErrors.UpdateNotFound);
		});
	}

	[TestMethod]
	public async Task UpdateSuccess()
	{
		AttendanceUpdateRequest request = GetUpdateRequest();
		request.AttendanceType = AttendanceType.WORKDAY;
		request.StartTime = new(6, 0, 0);
		request.EndTime = new(14, 0, 0);
		request.BreakTime = new(0, 30, 0);

		ErrorOr<Updated> result =
			await _attendanceService.Update(request);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().Be(Result.Updated);
		});
	}

	[TestMethod]
	public async Task UpdateMultipleBadRequest()
	{
		AttendanceUpdateRequest request = GetUpdateRequest();
		request.AttendanceType = AttendanceType.HOLIDAY;
		request.StartTime = new(6, 0, 0);

		AttendanceUpdateRequest[] requests = { request };

		ErrorOr<Updated> result =
			await _attendanceService.Update(requests);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeTrue();
			result.Errors.Should().HaveCount(1);
			result.FirstError.Should().Be(AttendanceServiceErrors.UpdateBadRequest(request.Id));
		});
	}

	[TestMethod]
	public async Task UpdateMultipleNotFound()
	{
		AttendanceUpdateRequest request = GetUpdateRequest();
		request.Id = default;

		AttendanceUpdateRequest[] requests = { request };

		ErrorOr<Updated> result =
			await _attendanceService.Update(requests);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeTrue();
			result.Errors.Should().HaveCount(1);
			result.FirstError.Should().Be(AttendanceServiceErrors.UpdateManyNotFound);
		});
	}

	[TestMethod]
	public async Task UpdateMultipleSuccess()
	{
		AttendanceUpdateRequest request = GetUpdateRequest();
		request.AttendanceType = AttendanceType.WORKDAY;
		request.StartTime = new(6, 0, 0);
		request.EndTime = new(14, 0, 0);
		request.BreakTime = new(0, 30, 0);

		AttendanceUpdateRequest[] requests = { request };

		ErrorOr<Updated> result =
			await _attendanceService.Update(requests);

		AssertionHelper.AssertInScope(() =>
		{
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().Be(Result.Updated);
		});
	}

	private static (Guid UserId, Guid CalendarId, Guid AttendanceId, DateTime Date) GetUserAttendance()
	{
		UserModel user = Users[RandomHelper.GetInt(0, Users.Count)];

		AttendanceModel attendance = user.Attendances
			.ToList()[RandomHelper.GetInt(0, user.Attendances.Count)];

		return (user.Id, attendance.CalendarId, attendance.Id, attendance.Calendar.Date);
	}

	private AttendanceUpdateRequest GetUpdateRequest()
	{
		UserModel user = Users[RandomHelper.GetInt(0, Users.Count)];

		AttendanceModel attendance = user.Attendances
			.ToList()[RandomHelper.GetInt(0, user.Attendances.Count)];

		attendance.AttendanceType = AttendanceType.MOBILEWORKING;
		attendance.StartTime = new(6, 0, 0);
		attendance.EndTime = new(14, 0, 0);
		attendance.BreakTime = new(0, 30, 0);

		return _mapper.Map<AttendanceUpdateRequest>(attendance);
	}

	private AttendanceCreateRequest GetCreateRequest()
	{
		AttendanceCreateRequest request = new()
		{
			Date = DateTime.Today,
			AttendanceType = AttendanceType.WORKDAY,
			StartTime = new(8, 0, 0),
			EndTime = new(15, 0, 0),
			BreakTime = new(0, 30, 0)
		};

		return request;
	}
}