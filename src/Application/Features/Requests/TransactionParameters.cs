using System.ComponentModel.DataAnnotations;

using Application.Features.Requests.Base;

namespace Application.Features.Requests;

/// <summary>
/// The transaction request parameter class.
/// </summary>
/// <remarks>
/// Derives from the <see cref="RequestParameters"/> class.
/// </remarks>
public sealed class TransactionParameters : RequestParameters
{
	/// <summary>
	/// The booking date filter option.
	/// </summary>
	[DataType(DataType.Date)]
	public DateTime? BookingDate { get; set; }

	/// <summary>
	/// The value date filter option.
	/// </summary>
	[DataType(DataType.Date)]
	public DateTime? ValueDate { get; set; }

	/// <summary>
	/// The client beneficiary filter option.
	/// </summary>
	[DataType(DataType.Text)]
	public string? Beneficiary { get; set; }

	/// <summary>
	/// The minimum amount value filter option.
	/// </summary>
	[DataType(DataType.Currency)]
	public decimal? MinValue { get; set; }

	/// <summary>
	/// The maximum amount value filter option.
	/// </summary>
	[DataType(DataType.Currency)]
	public decimal? MaxValue { get; set; }
}
