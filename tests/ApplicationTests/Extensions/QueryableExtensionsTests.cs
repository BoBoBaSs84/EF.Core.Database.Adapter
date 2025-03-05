using Application.Extensions;
using Application.Features.Requests;

using BB84.Extensions;
using BB84.Home.Domain.Entities.Attendance;
using BB84.Home.Domain.Entities.Finance;
using BB84.Home.Domain.Enumerators.Attendance;

using FluentAssertions;

using static Application.Common.ApplicationConstants;
using static BaseTests.Helpers.RandomHelper;
using static BB84.Home.Domain.Common.DomainConstants;

namespace ApplicationTests.Extensions;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, unit testing.")]
public sealed class QueryableExtensionsTests : ApplicationTestBase
{
	[TestMethod]
	[TestCategory(nameof(QueryableExtensions.FilterByParameters))]
	public void FilterByParametersWithAttendanceParametersValues()
	{
		DateTime minDate = new(2000, 1, 1), maxDate = new(2000, 1, 31);
		AttendanceType type = AttendanceType.HOLIDAY;
		IQueryable<AttendanceEntity> models = GetAttendances(minDate, maxDate, type);
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

		IQueryable<AttendanceEntity> filteredModels = models.FilterByParameters(parameters);

		filteredModels.Should().HaveCount(parameters.PageSize);
	}

	[TestMethod]
	[TestCategory(nameof(QueryableExtensions.FilterByParameters))]
	public void FilterByParametersWithoutAttendanceParametersValues()
	{
		DateTime minDate = new(2000, 1, 1), maxDate = new(2000, 1, 31);
		AttendanceType type = AttendanceType.HOLIDAY;
		IQueryable<AttendanceEntity> models = GetAttendances(minDate, maxDate, type);
		AttendanceParameters parameters = new();

		IQueryable<AttendanceEntity> filteredModels = models.FilterByParameters(parameters);

		filteredModels.Should().HaveCount(maxDate.Day);
	}

	[TestMethod]
	[TestCategory(nameof(QueryableExtensions.FilterByParameters))]
	public void FilterByParametersWithTransactionParametersValues()
	{
		DateTime minDate = new(2000, 1, 1), maxDate = new(2000, 1, 31);
		IQueryable<TransactionEntity> models = GetTransactions(minDate, maxDate);
		TransactionParameters parameters = new()
		{
			Beneficiary = GetString(250),
			BookingDate = minDate,
			ValueDate = minDate,
			MinValue = 0,
			MaxValue = 0,
			PageNumber = minDate.Day,
			PageSize = maxDate.Day
		};

		IQueryable<TransactionEntity> filteredModels = models.FilterByParameters(parameters);

		filteredModels.Should().HaveCount(0);
	}

	[TestMethod]
	[TestCategory(nameof(QueryableExtensions.FilterByParameters))]
	public void FilterByParametersWithoutTransactionParametersValues()
	{
		DateTime minDate = new(2000, 1, 1), maxDate = new(2000, 1, 31);
		IQueryable<TransactionEntity> models = GetTransactions(minDate, maxDate);
		TransactionParameters parameters = new();

		IQueryable<TransactionEntity> filteredModels = models.FilterByParameters(parameters);

		filteredModels.Should().HaveCount(maxDate.Day);
	}

	private static IQueryable<AttendanceEntity> GetAttendances(DateTime minDate, DateTime maxDate, AttendanceType type)
	{
		DateTime currentDate = minDate;
		List<AttendanceEntity> models = [];
		while (currentDate <= maxDate)
		{
			AttendanceEntity attendance = new()
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

	private static IQueryable<TransactionEntity> GetTransactions(DateTime minDate, DateTime maxDate)
	{
		DateTime currentDate = minDate;
		List<TransactionEntity> models = [];
		while (currentDate <= maxDate)
		{
			TransactionEntity transaction = new()
			{
				Id = Guid.NewGuid(),
				Creator = GetString(50),
				Editor = GetString(50),
				BookingDate = currentDate,
				ValueDate = currentDate.AddDays(1),
				PostingText = GetString(100),
				ClientBeneficiary = GetString(250),
				Purpose = GetString(400),
				AccountNumber = GetString(RegexPatterns.IBAN).RemoveWhitespace(),
				BankCode = GetString(25),
				AmountEur = GetInt(-100, 250),
				CreditorId = GetString(25),
				MandateReference = GetString(50),
				CustomerReference = GetString(50)
			};

			models.Add(transaction);
			currentDate = currentDate.AddDays(1);
		}
		return models.AsQueryable();
	}
}