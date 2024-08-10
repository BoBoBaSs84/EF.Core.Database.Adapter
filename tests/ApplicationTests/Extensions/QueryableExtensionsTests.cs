using Application.Extensions;
using Application.Features.Requests;

using BB84.Extensions;

using Domain.Enumerators;
using Domain.Models.Attendance;
using Domain.Models.Finance;

using FluentAssertions;

using static BaseTests.Helpers.RandomHelper;
using static Domain.Constants.DomainConstants;

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
		IQueryable<AttendanceModel> models = GetAttendances(minDate, maxDate, type);
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
		DateTime minDate = new(2000, 1, 1), maxDate = new(2000, 1, 31);
		AttendanceType type = AttendanceType.HOLIDAY;
		IQueryable<AttendanceModel> models = GetAttendances(minDate, maxDate, type);
		AttendanceParameters parameters = new();

		IQueryable<AttendanceModel> filteredModels = models.FilterByParameters(parameters);

		filteredModels.Should().HaveCount(maxDate.Day);
	}

	[TestMethod]
	[TestCategory(nameof(QueryableExtensions.FilterByParameters))]
	public void FilterByParametersWithTransactionParametersValues()
	{
		DateTime minDate = new(2000, 1, 1), maxDate = new(2000, 1, 31);
		IQueryable<TransactionModel> models = GetTransactions(minDate, maxDate);
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

		IQueryable<TransactionModel> filteredModels = models.FilterByParameters(parameters);

		filteredModels.Should().HaveCount(0);
	}

	[TestMethod]
	[TestCategory(nameof(QueryableExtensions.FilterByParameters))]
	public void FilterByParametersWithoutTransactionParametersValues()
	{
		DateTime minDate = new(2000, 1, 1), maxDate = new(2000, 1, 31);
		IQueryable<TransactionModel> models = GetTransactions(minDate, maxDate);
		TransactionParameters parameters = new();

		IQueryable<TransactionModel> filteredModels = models.FilterByParameters(parameters);

		filteredModels.Should().HaveCount(maxDate.Day);
	}

	private static IQueryable<AttendanceModel> GetAttendances(DateTime minDate, DateTime maxDate, AttendanceType type)
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

	private static IQueryable<TransactionModel> GetTransactions(DateTime minDate, DateTime maxDate)
	{
		DateTime currentDate = minDate;
		List<TransactionModel> models = [];
		while (currentDate <= maxDate)
		{
			TransactionModel transaction = new()
			{
				Id = Guid.NewGuid(),
				CreatedBy = GetString(50),
				ModifiedBy = GetString(50),
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