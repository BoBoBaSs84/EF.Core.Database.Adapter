using Application.Features.Requests;

using Domain.Entities.Attendance;
using Domain.Entities.Documents;
using Domain.Entities.Finance;
using Domain.Enumerators.Attendance;

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
	public static IQueryable<AttendanceEntity> FilterByParameters(this IQueryable<AttendanceEntity> query, AttendanceParameters parameters)
	{
		IQueryable<AttendanceEntity> filteredQuery = query
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
	public static IQueryable<DocumentEntity> FilterByParameters(this IQueryable<DocumentEntity> query, DocumentParameters parameters)
	{
		IQueryable<DocumentEntity> filteredQuery = query
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
	public static IQueryable<TransactionEntity> FilterByParameters(this IQueryable<TransactionEntity> query, TransactionParameters parameters)
	{
		IQueryable<TransactionEntity> filteredQuery = query
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
	private static IQueryable<DocumentEntity> FilterByName(this IQueryable<DocumentEntity> query, string? name)
		=> name is not null ? query.Where(x => x.Name.Contains(name)) : query;

	/// <summary>
	/// Filters the <paramref name="query"/> by the document <paramref name="directory"/>.
	/// </summary>
	/// <param name="query">The query to filter.</param>
	/// <param name="directory">The directory to search for.</param>
	/// <returns>The filtered <paramref name="query"/>.</returns>
	private static IQueryable<DocumentEntity> FilterByDirectory(this IQueryable<DocumentEntity> query, string? directory)
		=> directory is not null ? query.Where(x => x.Directory.Contains(directory)) : query;

	/// <summary>
	/// Filters the <paramref name="query"/> by the document <paramref name="extension"/>.
	/// </summary>
	/// <param name="query">The query to filter.</param>
	/// <param name="extension">The extension to search for.</param>
	/// <returns>The filtered <paramref name="query"/>.</returns>
	private static IQueryable<DocumentEntity> FilterByExtension(this IQueryable<DocumentEntity> query, string? extension)
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
	private static IQueryable<AttendanceEntity> FilterByYear(this IQueryable<AttendanceEntity> query, int? year)
		=> year.HasValue ? query.Where(x => x.Date.Year.Equals(year)) : query;

	/// <summary>
	/// Filter the attendance entries by month.
	/// </summary>
	/// <param name="query">The query to filter.</param>
	/// <param name="month">The month to be filtered.</param>
	/// <returns><see cref="IQueryable{T}"/></returns>
	private static IQueryable<AttendanceEntity> FilterByMonth(this IQueryable<AttendanceEntity> query, int? month)
		=> month.HasValue ? query.Where(x => x.Date.Month.Equals(month)) : query;

	/// <summary>
	/// Filters the attendance entries by date range.
	/// </summary>
	/// <param name="query">The query to filter.</param>
	/// <param name="minDate">The minimum date.</param>
	/// <param name="maxDate">The maximum date.</param>
	/// <returns><see cref="IQueryable{T}"/></returns>
	private static IQueryable<AttendanceEntity> FilterByDateRange(this IQueryable<AttendanceEntity> query, DateTime? minDate, DateTime? maxDate)
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
	private static IQueryable<AttendanceEntity> FilterByType(this IQueryable<AttendanceEntity> query, AttendanceType? type)
		=> type.HasValue ? query.Where(x => x.Type.Equals(type)) : query;

	/// <summary>
	/// Filters the transaction entries by the booking date.
	/// </summary>
	/// <param name="query">The query to filter.</param>
	/// <param name="dateTime">The date to be filtered by.</param>
	/// <returns><see cref="IQueryable{T}"/></returns>
	private static IQueryable<TransactionEntity> FilterByBookingDate(this IQueryable<TransactionEntity> query, DateTime? dateTime)
		=> dateTime.HasValue ? query.Where(x => x.BookingDate.Equals(dateTime.Value.Date)) : query;

	/// <summary>
	/// Filters the transaction entries by the value date.
	/// </summary>
	/// <param name="query">The query to filter.</param>
	/// <param name="dateTime">The date to be filtered by.</param>
	/// <returns><see cref="IQueryable{T}"/></returns>
	private static IQueryable<TransactionEntity> FilterByValueDate(this IQueryable<TransactionEntity> query, DateTime? dateTime)
		=> dateTime.HasValue ? query.Where(x => x.ValueDate.Equals(dateTime.Value.Date)) : query;

	/// <summary>
	/// Filters the transaction entries by the client beneficiary.
	/// </summary>
	/// <param name="query">The query to filter.</param>
	/// <param name="beneficiary">The client beneficiary to be filtered by.</param>
	/// <returns><see cref="IQueryable{T}"/></returns>
	private static IQueryable<TransactionEntity> FilterByBeneficiary(this IQueryable<TransactionEntity> query, string? beneficiary)
		=> beneficiary is not null ? query.Where(x => x.ClientBeneficiary.Contains(beneficiary)) : query;

	/// <summary>
	/// Filters the transaction entries by amount range.
	/// </summary>
	/// <param name="query">The query to filter.</param>
	/// <param name="minValue">The minimum value to be filtered by.</param>
	/// <param name="maxValue">The maximum value to be filtered by.</param>
	/// <returns><see cref="IQueryable{T}"/></returns>
	private static IQueryable<TransactionEntity> FilterByAmountRange(this IQueryable<TransactionEntity> query, decimal? minValue, decimal? maxValue)
	{
		query = minValue.HasValue ? query.Where(x => x.AmountEur >= minValue) : query;
		query = maxValue.HasValue ? query.Where(x => x.AmountEur <= maxValue) : query;

		return query;
	}

	#endregion private methods
}
