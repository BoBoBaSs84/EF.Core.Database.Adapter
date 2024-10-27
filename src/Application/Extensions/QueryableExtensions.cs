using Application.Features.Requests;

using Domain.Enumerators.Attendance;
using Domain.Models.Attendance;
using Domain.Models.Documents;
using Domain.Models.Finance;

namespace Application.Extensions;

/// <summary>
/// Contains extension for the <see cref="IQueryable{T}"/> interface.
/// </summary>
public static class QueryableExtensions
{
	#region public methods

	/// <summary>
	/// Filters the <paramref name="query"/> by the provided <paramref name="parameters"/>.
	/// </summary>
	/// <param name="query">The query to filter.</param>
	/// <param name="parameters">The parameters to filter by.</param>
	/// <returns>The filtered <paramref name="query"/>.</returns>
	public static IQueryable<DateTime> FilterByParameters(this IQueryable<DateTime> query, CalendarParameters parameters)
	{
		IQueryable<DateTime> filteredQuery = query
			.FilterByYear(parameters.Year)
			.FilterByMonth(parameters.Month)
			.FilterByDateRange(parameters.MinDate, parameters.MaxDate);

		return filteredQuery;
	}

	/// <summary>
	/// Filters the <paramref name="query"/> by the provided <paramref name="parameters"/>.
	/// </summary>
	/// <param name="query">The query to filter.</param>
	/// <param name="parameters">The parameters to filter by.</param>
	/// <returns>The filtered <paramref name="query"/>.</returns>
	public static IQueryable<AttendanceModel> FilterByParameters(this IQueryable<AttendanceModel> query, AttendanceParameters parameters)
	{
		IQueryable<AttendanceModel> filteredQuery = query
			.FilterByYear(parameters.Year)
			.FilterByMonth(parameters.Month)
			.FilterByDateRange(parameters.MinDate, parameters.MaxDate)
			.FilterByType(parameters.Type);

		return filteredQuery;
	}

	/// <summary>
	/// Filters the <paramref name="query"/> by the provided <paramref name="parameters"/>.
	/// </summary>
	/// <param name="query">The query to filter.</param>
	/// <param name="parameters">The parameters to filter by.</param>
	/// <returns>The filtered <paramref name="query"/>.</returns>
	public static IQueryable<Document> FilterByParameters(this IQueryable<Document> query, DocumentParameters parameters)
	{
		IQueryable<Document> filteredQuery = query
			.FilterByDirectory(parameters.Directory)
			.FilterByExtension(parameters.ExtensionName)
			.FilterByName(parameters.Name);

		return filteredQuery;
	}

	/// <summary>
	/// Filters the <paramref name="query"/> by the provided <paramref name="parameters"/>.
	/// </summary>
	/// <param name="query">The query to filter.</param>
	/// <param name="parameters">The parameters to filter by.</param>
	/// <returns>The filtered <paramref name="query"/>.</returns>
	public static IQueryable<TransactionModel> FilterByParameters(this IQueryable<TransactionModel> query, TransactionParameters parameters)
	{
		IQueryable<TransactionModel> filteredQuery = query
			.FilterByBookingDate(parameters.BookingDate)
			.FilterByValueDate(parameters.ValueDate)
			.FilterByBeneficiary(parameters.Beneficiary)
			.FilterByAmountRange(parameters.MinValue, parameters.MaxValue);

		return filteredQuery;
	}

	#endregion public methods

	#region private methods

	/// <summary>
	/// Filters the <paramref name="query"/> by the document <paramref name="name"/>.
	/// </summary>
	/// <param name="query">The query to filter.</param>
	/// <param name="name">The name to search for.</param>
	/// <returns>The filtered <paramref name="query"/>.</returns>
	private static IQueryable<Document> FilterByName(this IQueryable<Document> query, string? name)
		=> name is not null ? query.Where(x => x.Name.Contains(name)) : query;

	/// <summary>
	/// Filters the <paramref name="query"/> by the document <paramref name="directory"/>.
	/// </summary>
	/// <param name="query">The query to filter.</param>
	/// <param name="directory">The directory to search for.</param>
	/// <returns>The filtered <paramref name="query"/>.</returns>
	private static IQueryable<Document> FilterByDirectory(this IQueryable<Document> query, string? directory)
		=> directory is not null ? query.Where(x => x.Directory.Contains(directory)) : query;

	/// <summary>
	/// Filters the <paramref name="query"/> by the document <paramref name="extension"/>.
	/// </summary>
	/// <param name="query">The query to filter.</param>
	/// <param name="extension">The extension to search for.</param>
	/// <returns>The filtered <paramref name="query"/>.</returns>
	private static IQueryable<Document> FilterByExtension(this IQueryable<Document> query, string? extension)
		=> extension is not null ? query.Where(x => x.Extension.Name.Contains(extension)) : query;

	/// <summary>
	/// Filters the date time values by year.
	/// </summary>
	/// <param name="query">The query to filter.</param>
	/// <param name="year">The year to be filtered.</param>
	/// <returns><see cref="IQueryable{T}"/></returns>
	private static IQueryable<DateTime> FilterByYear(this IQueryable<DateTime> query, int? year)
		=> year.HasValue ? query.Where(x => x.Date.Year.Equals(year)) : query;

	/// <summary>
	/// Filters the date time values by month.
	/// </summary>
	/// <param name="query">The query to filter.</param>
	/// <param name="month">The month to be filtered.</param>
	/// <returns><see cref="IQueryable{T}"/></returns>
	private static IQueryable<DateTime> FilterByMonth(this IQueryable<DateTime> query, int? month)
		=> month.HasValue ? query.Where(x => x.Date.Month.Equals(month)) : query;

	/// <summary>
	/// Filters the date time values by min and max range.
	/// </summary>
	/// <param name="query">The query to filter.</param>
	/// <param name="minDate">The minimum date.</param>
	/// <param name="maxDate">The maximum date.</param>
	/// <returns><see cref="IQueryable{T}"/></returns>
	private static IQueryable<DateTime> FilterByDateRange(this IQueryable<DateTime> query, DateTime? minDate, DateTime? maxDate)
	{
		query = minDate.HasValue ? query.Where(x => x.Date >= minDate.Value.Date) : query;
		query = maxDate.HasValue ? query.Where(x => x.Date <= maxDate.Value.Date) : query;

		return query;
	}

	/// <summary>
	/// Filters the attendance entries by year.
	/// </summary>
	/// <param name="query">The query to filter.</param>
	/// <param name="year">The year to be filtered.</param>
	/// <returns><see cref="IQueryable{T}"/></returns>
	private static IQueryable<AttendanceModel> FilterByYear(this IQueryable<AttendanceModel> query, int? year)
		=> year.HasValue ? query.Where(x => x.Date.Year.Equals(year)) : query;

	/// <summary>
	/// Filter the attendance entries by month.
	/// </summary>
	/// <param name="query">The query to filter.</param>
	/// <param name="month">The month to be filtered.</param>
	/// <returns><see cref="IQueryable{T}"/></returns>
	private static IQueryable<AttendanceModel> FilterByMonth(this IQueryable<AttendanceModel> query, int? month)
		=> month.HasValue ? query.Where(x => x.Date.Month.Equals(month)) : query;

	/// <summary>
	/// Filters the attendance entries by date range.
	/// </summary>
	/// <param name="query">The query to filter.</param>
	/// <param name="minDate">The minimum date.</param>
	/// <param name="maxDate">The maximum date.</param>
	/// <returns><see cref="IQueryable{T}"/></returns>
	private static IQueryable<AttendanceModel> FilterByDateRange(this IQueryable<AttendanceModel> query, DateTime? minDate, DateTime? maxDate)
	{
		query = minDate.HasValue ? query.Where(x => x.Date >= minDate.Value.Date) : query;
		query = maxDate.HasValue ? query.Where(x => x.Date <= maxDate.Value.Date) : query;

		return query;
	}

	/// <summary>
	/// Filter the attendance entries by the attendance type.
	/// </summary>
	/// <param name="query">The query to filter.</param>
	/// <param name="type">The attendance type to be filtered.</param>
	/// <returns><see cref="IQueryable{T}"/></returns>
	private static IQueryable<AttendanceModel> FilterByType(this IQueryable<AttendanceModel> query, AttendanceType? type)
		=> type.HasValue ? query.Where(x => x.Type.Equals(type)) : query;

	/// <summary>
	/// Filters the transaction entries by the booking date.
	/// </summary>
	/// <param name="query">The query to filter.</param>
	/// <param name="dateTime">The date to be filtered by.</param>
	/// <returns><see cref="IQueryable{T}"/></returns>
	private static IQueryable<TransactionModel> FilterByBookingDate(this IQueryable<TransactionModel> query, DateTime? dateTime)
		=> dateTime.HasValue ? query.Where(x => x.BookingDate.Equals(dateTime.Value.Date)) : query;

	/// <summary>
	/// Filters the transaction entries by the value date.
	/// </summary>
	/// <param name="query">The query to filter.</param>
	/// <param name="dateTime">The date to be filtered by.</param>
	/// <returns><see cref="IQueryable{T}"/></returns>
	private static IQueryable<TransactionModel> FilterByValueDate(this IQueryable<TransactionModel> query, DateTime? dateTime)
		=> dateTime.HasValue ? query.Where(x => x.ValueDate.Equals(dateTime.Value.Date)) : query;

	/// <summary>
	/// Filters the transaction entries by the client beneficiary.
	/// </summary>
	/// <param name="query">The query to filter.</param>
	/// <param name="beneficiary">The client beneficiary to be filtered by.</param>
	/// <returns><see cref="IQueryable{T}"/></returns>
	private static IQueryable<TransactionModel> FilterByBeneficiary(this IQueryable<TransactionModel> query, string? beneficiary)
		=> beneficiary is not null ? query.Where(x => x.ClientBeneficiary.Contains(beneficiary)) : query;

	/// <summary>
	/// Filters the transaction entries by amount range.
	/// </summary>
	/// <param name="query">The query to filter.</param>
	/// <param name="minValue">The minimum value to be filtered by.</param>
	/// <param name="maxValue">The maximum value to be filtered by.</param>
	/// <returns><see cref="IQueryable{T}"/></returns>
	private static IQueryable<TransactionModel> FilterByAmountRange(this IQueryable<TransactionModel> query, decimal? minValue, decimal? maxValue)
	{
		query = minValue.HasValue ? query.Where(x => x.AmountEur >= minValue) : query;
		query = maxValue.HasValue ? query.Where(x => x.AmountEur <= maxValue) : query;

		return query;
	}

	#endregion private methods
}
