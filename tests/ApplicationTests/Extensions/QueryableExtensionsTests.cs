using Application.Extensions;
using Application.Features.Requests;

using Domain.Enumerators;
using Domain.Models.Attendance;

using FluentAssertions;

namespace ApplicationTests.Extensions;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, unit testing.")]
public sealed class QueryableExtensionsTests : ApplicationTestBase
{
	[TestMethod]
	[TestCategory(nameof(QueryableExtensions.FilterByParameters))]
	public void FilterByParametersWithAttendanceParametersValues()
	{
		DateTime minDate = new(2000, 1, 1),
			maxDate = new(2000, 1, 31);
		AttendanceType type = AttendanceType.HOLIDAY;
		IQueryable<AttendanceModel> models = GetAttendanceModels(minDate, maxDate, type);
		AttendanceParameters parameters = new()
		{
			Year = minDate.Year,
			Month = minDate.Month,
			MinDate = minDate,
			MaxDate = maxDate,
			Type = AttendanceType.HOLIDAY,
			PageNumber = minDate.Day,
			PageSize = maxDate.Day
		};

		IQueryable<AttendanceModel> filteredModels = models.FilterByParameters(parameters);

		filteredModels.Should().HaveCount(parameters.PageSize);
	}

	[TestMethod]
	[TestCategory(nameof(QueryableExtensions.FilterByParameters))]
	public void FilterByParametersWithoutAttendanceParametersValues()
	{
		DateTime minDate = new(2000, 1, 1),
			maxDate = new(2000, 1, 31);
		AttendanceType type = AttendanceType.HOLIDAY;
		IQueryable<AttendanceModel> models = GetAttendanceModels(minDate, maxDate, type);
		AttendanceParameters parameters = new();

		IQueryable<AttendanceModel> filteredModels = models.FilterByParameters(parameters);

		filteredModels.Should().HaveCount(maxDate.Day);
	}

	private static IQueryable<AttendanceModel> GetAttendanceModels(DateTime minDate, DateTime maxDate, AttendanceType type)
	{
		DateTime currentDate = minDate;
		List<AttendanceModel> models = [];
		while (currentDate <= maxDate)
		{
			AttendanceModel attendance = new()
			{
				Id = Guid.NewGuid(),
				Date = currentDate,
				Type = type
			};

			models.Add(attendance);
			currentDate = currentDate.AddDays(1);
		}
		return models.AsQueryable();
	}
}