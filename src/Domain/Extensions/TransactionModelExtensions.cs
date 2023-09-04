using Domain.Models.Finance;

namespace Domain.Extensions;

/// <summary>
/// The transaction model extensions class.
/// </summary>
public static class TransactionModelExtensions
{
	/// <summary>
	/// Filters the transaction entries by the booking date.
	/// </summary>
	/// <param name="query">The query to filter.</param>
	/// <param name="dateTime">The date to be filtered by.</param>
	/// <returns><see cref="IQueryable{T}"/></returns>
	public static IQueryable<TransactionModel> FilterByBookingDate(this IQueryable<TransactionModel> query, DateTime? dateTime)
		=> dateTime.HasValue ? query.Where(x => x.BookingDate.Equals(dateTime.ToSqlDate())) : query;

	/// <summary>
	/// Filters the transaction entries by the value date.
	/// </summary>
	/// <param name="query">The query to filter.</param>
	/// <param name="dateTime">The date to be filtered by.</param>
	/// <returns><see cref="IQueryable{T}"/></returns>
	public static IQueryable<TransactionModel> FilterByValueDate(this IQueryable<TransactionModel> query, DateTime? dateTime)
		=> dateTime.HasValue ? query.Where(x => x.ValueDate.Equals(dateTime.ToSqlDate())) : query;

	/// <summary>
	/// Filters the transaction entries by the client beneficiary.
	/// </summary>
	/// <param name="query">The query to filter.</param>
	/// <param name="beneficiary">The client beneficiary to be filtered by.</param>
	/// <returns><see cref="IQueryable{T}"/></returns>
	public static IQueryable<TransactionModel> FilterByBeneficiary(this IQueryable<TransactionModel> query, string? beneficiary)
		=> !string.IsNullOrWhiteSpace(beneficiary) ? query.Where(x => x.ClientBeneficiary == beneficiary) : query;

	/// <summary>
	/// Filters the transaction entries by amount range.
	/// </summary>
	/// <param name="query">The query to filter.</param>
	/// <param name="minValue">The minimum value to be filtered by.</param>
	/// <param name="maxValue">The maximum value to be filtered by.</param>
	/// <returns><see cref="IQueryable{T}"/></returns>
	public static IQueryable<TransactionModel> FilterByAmountRange(this IQueryable<TransactionModel> query, decimal? minValue, decimal? maxValue)
	{
		query = minValue.HasValue ? query.Where(x => x.AmountEur >= minValue) : query;
		query = maxValue.HasValue ? query.Where(x => x.AmountEur <= maxValue) : query;
		return query;
	}
}
