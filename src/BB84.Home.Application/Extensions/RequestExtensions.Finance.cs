using BB84.Home.Application.Contracts.Requests.Finance;
using BB84.Home.Domain.Entities.Finance;

namespace BB84.Home.Application.Extensions;

internal static partial class RequestExtensions
{
	/// <summary>
	/// Converts an <see cref="AccountCreateRequest"/> to an <see cref="AccountEntity"/>.
	/// </summary>
	/// <param name="request">The <see cref="AccountCreateRequest"/> to convert.</param>
	/// <returns>The converted <see cref="AccountEntity"/>.</returns>
	public static AccountEntity ToEntity(this AccountCreateRequest request)
	{
		AccountEntity entity = new()
		{
			IBAN = request.IBAN,
			Type = request.Type,
			Provider = request.Provider
		};

		if (request.Cards is not null)
			entity.Cards = [.. request.Cards.Select(c => c.ToEntity())];

		return entity;
	}

	/// <summary>
	/// Converts an <see cref="AccountUpdateRequest"/> to an <see cref="AccountEntity"/>.
	/// </summary>
	/// <param name="request">The <see cref="AccountUpdateRequest"/> to convert.</param>
	/// <param name="entity">The entity to update.</param>
	/// <returns>The updated <see cref="AccountEntity"/>.</returns>
	public static AccountEntity ToEntity(this AccountUpdateRequest request, AccountEntity entity)
	{
		entity.Type = request.Type;
		entity.Provider = request.Provider;

		return entity;
	}

	/// <summary>
	/// Converts a <see cref="CardCreateRequest"/> to a <see cref="CardEntity"/>.
	/// </summary>
	/// <param name="request">The <see cref="CardCreateRequest"/> to convert.</param>
	/// <returns>The converted <see cref="CardEntity"/>.</returns>
	public static CardEntity ToEntity(this CardCreateRequest request)
	{
		CardEntity entity = new()
		{
			PAN = request.PAN,
			Type = request.Type,
			ValidUntil = request.ValidUntil
		};

		return entity;
	}

	/// <summary>
	/// Converts a <see cref="CardCreateRequest"/> to a <see cref="CardEntity"/>.
	/// </summary>
	/// <param name="request">The <see cref="CardCreateRequest"/> to convert.</param>
	/// <param name="userId">The user ID to associate with the card.</param>
	/// <param name="accountId">The account ID to associate with the card.</param>
	/// <returns>The converted <see cref="CardEntity"/>.</returns>
	public static CardEntity ToEntity(this CardCreateRequest request, Guid userId, Guid accountId)
	{
		CardEntity entity = request.ToEntity();
		entity.UserId = userId;
		entity.AccountId = accountId;

		return entity;
	}

	/// <summary>
	/// Converts a <see cref="CardUpdateRequest"/> to a <see cref="CardEntity"/>.
	/// </summary>
	/// <param name="request">The <see cref="CardUpdateRequest"/> to convert.</param>
	/// <param name="entity">The <see cref="CardEntity"/> to update.</param>
	/// <returns>The updated <see cref="CardEntity"/>.</returns>
	public static CardEntity ToEntity(this CardUpdateRequest request, CardEntity entity)
	{
		entity.Type = request.Type;
		entity.ValidUntil = request.ValidUntil;

		return entity;
	}

	/// <summary>
	/// Converts a <see cref="TransactionCreateRequest"/> to a <see cref="TransactionEntity"/>.
	/// </summary>
	/// <param name="request">The <see cref="TransactionCreateRequest"/> to convert.</param>
	/// <returns>The converted <see cref="TransactionEntity"/>.</returns>
	public static TransactionEntity ToEntity(this TransactionCreateRequest request)
	{
		TransactionEntity entity = new()
		{
			BookingDate = request.BookingDate,
			ValueDate = request.ValueDate,
			PostingText = request.PostingText,
			ClientBeneficiary = request.ClientBeneficiary,
			Purpose = request.Purpose,
			AccountNumber = request.AccountNumber,
			BankCode = request.BankCode,
			AmountEur = request.AmountEur,
			CreditorId = request.CreditorId,
			MandateReference = request.MandateReference,
			CustomerReference = request.CustomerReference
		};

		return entity;
	}

	/// <summary>
	/// Converts a <see cref="TransactionUpdateRequest"/> to a <see cref="TransactionEntity"/>.
	/// </summary>
	/// <param name="request">The <see cref="TransactionUpdateRequest"/> to convert.</param>
	/// <param name="entity">The <see cref="TransactionEntity"/> to update.</param>
	/// <returns>The updated <see cref="TransactionEntity"/>.</returns>
	public static TransactionEntity ToEntity(this TransactionUpdateRequest request, TransactionEntity entity)
	{
		entity.BookingDate = request.BookingDate;
		entity.ValueDate = request.ValueDate;
		entity.PostingText = request.PostingText;
		entity.ClientBeneficiary = request.ClientBeneficiary;
		entity.Purpose = request.Purpose;
		entity.AccountNumber = request.AccountNumber;
		entity.BankCode = request.BankCode;
		entity.AmountEur = request.AmountEur;
		entity.CreditorId = request.CreditorId;
		entity.MandateReference = request.MandateReference;
		entity.CustomerReference = request.CustomerReference;

		return entity;
	}
}
