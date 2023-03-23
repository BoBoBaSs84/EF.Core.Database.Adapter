using Application.Contracts.Responses.Base;

namespace Application.Contracts.Responses.Finance;

/// <summary>
/// The account response class.
/// </summary>
/// <remarks>
/// Derives from the <see cref="ResponseModel"/> class.
/// </remarks>
public sealed class AccountResponse : ResponseModel
{
	/// <summary>
	/// The iban number.
	/// </summary>
	public string IBAN { get; set; } = default!;

	/// <summary>
	/// The account provider.
	/// </summary>
	public string Provider { get; set; } = default!;
}
