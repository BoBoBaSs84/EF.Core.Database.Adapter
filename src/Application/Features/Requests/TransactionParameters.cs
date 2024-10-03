using System.ComponentModel.DataAnnotations;

using Application.Features.Requests.Base;

namespace Application.Features.Requests;

/// <summary>
/// The parameters fo the transaction request.
/// </summary>
public sealed class TransactionParameters : RequestParameters
{
	/// <summary>
	/// The booking date filter option.
	/// </summary>
	public DateTime? BookingDate { get; init; }

	/// <summary>
	/// The value date filter option.
	/// </summary>
	public DateTime? ValueDate { get; init; }

	/// <summary>
	/// The client beneficiary filter option.
	/// </summary>
	public string? Beneficiary { get; init; }

	/// <summary>
	/// The minimum amount value filter option.
	/// </summary>
	public decimal? MinValue { get; init; }

	/// <summary>
	/// The maximum amount value filter option.
	/// </summary>
	public decimal? MaxValue { get; init; }
}
