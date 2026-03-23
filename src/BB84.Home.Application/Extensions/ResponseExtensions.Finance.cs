using BB84.Home.Application.Contracts.Responses.Finance;
using BB84.Home.Domain.Entities.Finance;

namespace BB84.Home.Application.Extensions;

internal static partial class ResponseExtensions
{
	/// <summary>
	/// Converts a <see cref="CardEntity"/> to a <see cref="CardResponse"/>.
	/// </summary>
	/// <param name="entity">The <see cref="CardEntity"/> to convert.</param>
	/// <returns>The converted <see cref="CardResponse"/>.</returns>
	public static CardResponse ToResponse(this CardEntity entity)
	{
		CardResponse response = new()
		{
			Id = entity.Id,
			PAN = entity.PAN,
			Type = entity.Type,
			ValidUntil = entity.ValidUntil
		};

		return response;
	}

	/// <summary>
	/// Converts a <see cref="AccountEntity"/> to a <see cref="AccountResponse"/>.
	/// </summary>
	/// <param name="entity">The <see cref="AccountEntity"/> to convert.</param>
	/// <returns>The converted <see cref="AccountResponse"/>.</returns>
	public static AccountResponse ToResponse(this AccountEntity entity)
	{
		AccountResponse response = new()
		{
			Id = entity.Id,
			IBAN = entity.IBAN,
			Type = entity.Type,
			Provider = entity.Provider,
			Cards = entity.Cards?.Select(x => x.ToResponse()).ToArray()
		};

		return response;
	}

	/// <summary>
	/// Converts a <see cref="TransactionEntity"/> to a <see cref="TransactionResponse"/>.
	/// </summary>
	/// <param name="entity">The <see cref="TransactionEntity"/> to convert.</param>
	/// <returns>The converted <see cref="TransactionResponse"/>.</returns>
	public static TransactionResponse ToResponse(this TransactionEntity entity)
	{
		TransactionResponse response = new()
		{
			Id = entity.Id,
			BookingDate = entity.BookingDate,
			ValueDate = entity.ValueDate,
			PostingText = entity.PostingText,
			ClientBeneficiary = entity.ClientBeneficiary,
			Purpose = entity.Purpose,
			AccountNumber = entity.AccountNumber,
			BankCode = entity.BankCode,
			AmountEur = entity.AmountEur,
			CreditorId = entity.CreditorId,
			MandateReference = entity.MandateReference,
			CustomerReference = entity.CustomerReference
		};

		return response;
	}
}
