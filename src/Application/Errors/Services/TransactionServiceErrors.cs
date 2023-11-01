using Application.Errors.Base;

using BB84.Extensions;

using RESX = Application.Properties.ServiceErrors;

namespace Application.Errors.Services;

/// <summary>
/// The static transaction service errors class.
/// </summary>
/// <remarks>
/// Contains errors that are relevant for the transaction service.
/// </remarks>
public static class TransactionServiceErrors
{
	private const string ErrorPrefix = $"{nameof(TransactionServiceErrors)}";

	/// <summary>
	/// Error that indicates an exception during the transaction service.
	/// </summary>
	/// <param name="accountId">The identifier of the bank account.</param>
	public static ApiError CreateForAccountFailed(Guid accountId) =>
		ApiError.CreateFailed($"{ErrorPrefix}.{CreateForAccountFailed}",
			RESX.TransactionServiceErrors_CreateForAccount_Failed.FormatInvariant(accountId));

	/// <summary>
	/// Error that indicates an exception during the transaction service.
	/// </summary>
	/// <param name="cardId">The identifier of the bank card.</param>
	public static ApiError CreateForCardFailed(Guid cardId) =>
		ApiError.CreateFailed($"{ErrorPrefix}.{CreateForCardFailed}",
			RESX.TransactionServiceErrors_CreateForCard_Failed.FormatInvariant(cardId));

	/// <summary>
	/// Error that indicates an exception during the transaction service.
	/// </summary>
	/// <param name="transactionId">The identifier of the transaction.</param>
	public static ApiError DeleteFailed(Guid transactionId) =>
		ApiError.CreateFailed($"{ErrorPrefix}.{DeleteFailed}",
			RESX.TransactionServiceErrors_Delete_Failed.FormatInvariant(transactionId));

	/// <summary>
	/// Error that indicates an exception during the transaction service.
	/// </summary>
	/// <param name="transactionId">The identifier of the transaction.</param>
	public static ApiError GetByIdFailed(Guid transactionId) =>
		ApiError.CreateFailed($"{ErrorPrefix}.{GetByIdFailed}",
			RESX.TransactionServiceErrors_GetById_Failed.FormatInvariant(transactionId));

	/// <summary>
	/// Error that indicates an exception during the transaction service.
	/// </summary>
	/// <param name="transactionId">The identifier of the transaction.</param>
	public static ApiError GetByIdNotFound(Guid transactionId) =>
		ApiError.CreateNotFound($"{ErrorPrefix}.{GetByIdNotFound}",
			RESX.TransactionServiceErrors_GetById_NotFound.FormatInvariant(transactionId));

	/// <summary>
	/// Error that indicates an exception during the transaction service.
	/// </summary>
	/// <param name="cardId">The identifier of the bank card.</param>
	public static ApiError GetByCardIdFailed(Guid cardId) =>
		ApiError.CreateFailed($"{ErrorPrefix}.{GetByCardIdFailed}",
			RESX.TransactionServiceErrors_GetByCardId_Failed.FormatInvariant(cardId));

	/// <summary>
	/// Error that indicates an exception during the transaction service.
	/// </summary>
	/// <param name="cardId">The identifier of the bank card.</param>
	public static ApiError GetByCardIdNotFound(Guid cardId) =>
		ApiError.CreateNotFound($"{ErrorPrefix}.{GetByCardIdNotFound}",
			RESX.TransactionServiceErrors_GetByCardId_NotFound.FormatInvariant(cardId));

	/// <summary>
	/// Error that indicates an exception during the transaction service.
	/// </summary>
	/// <param name="accountId">The identifier of the bank account.</param>
	public static ApiError GetByAccountIdFailed(Guid accountId) =>
		ApiError.CreateFailed($"{ErrorPrefix}.{GetByAccountIdFailed}",
			RESX.TransactionServiceErrors_GetByAccountId_Failed.FormatInvariant(accountId));

	/// <summary>
	/// Error that indicates an exception during the transaction service.
	/// </summary>
	/// <param name="accountId">The identifier of the bank account.</param>
	public static ApiError GetByAccountIdNotFound(Guid accountId) =>
		ApiError.CreateNotFound($"{ErrorPrefix}.{GetByAccountIdNotFound}",
			RESX.TransactionServiceErrors_GetByAccountId_NotFound.FormatInvariant(accountId));

	/// <summary>
	/// Error that indicates an exception during the transaction service.
	/// </summary>
	/// <param name="transactionId">The identifier of the transaction.</param>
	public static ApiError UpdateFailed(Guid transactionId) =>
		ApiError.CreateFailed($"{ErrorPrefix}.{UpdateFailed}",
			RESX.TransactionServiceErrors_Update_Failed.FormatInvariant(transactionId));
}
