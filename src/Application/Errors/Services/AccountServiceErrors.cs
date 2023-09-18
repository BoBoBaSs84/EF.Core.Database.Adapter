using Application.Errors.Base;

using Domain.Extensions;

using RESX = Application.Properties.ServiceErrors;

namespace Application.Errors.Services;

/// <summary>
/// The static account service errors class.
/// </summary>
/// <remarks>
/// Contains errors that are relevant for the account service.
/// </remarks>
public static class AccountServiceErrors
{
	private const string ErrorPrefix = $"{nameof(AccountServiceErrors)}";

	/// <summary>
	/// Error that indicates an exception during the account service.
	/// </summary>
	public static readonly ApiError GetAllFailed =
		ApiError.CreateFailed($"{ErrorPrefix}.{GetAllFailed}",
			RESX.AccountService_GetAll_Failed);

	/// <summary>
	/// Error that indicates an exception during the account service.
	/// </summary>
	public static readonly ApiError GetAllNotFound =
		ApiError.CreateNotFound($"{ErrorPrefix}.{GetAllNotFound}",
			RESX.AccountService_GetAll_NotFound);

	/// <summary>
	/// Error that indicates an exception during the account service.
	/// </summary>
	public static ApiError GetByIdFailed(Guid accountId) =>
		ApiError.CreateFailed($"{ErrorPrefix}.{GetByIdFailed}",
			RESX.AccountService_GetById_Failed.ToInvariant(accountId));

	/// <summary>
	/// Error that indicates an exception during the account service.
	/// </summary>
	public static ApiError GetByIdNotFound(Guid accountId) =>
		ApiError.CreateNotFound($"{ErrorPrefix}.{GetByIdNotFound}",
			RESX.AccountService_AccountId_NotFound.ToInvariant(accountId));

	/// <summary>
	/// Error that indicates an exception during the account service.
	/// </summary>
	public static ApiError GetByNumberFailed(string iban) =>
		ApiError.CreateFailed($"{ErrorPrefix}.{GetByNumberFailed}",
			RESX.AccountService_GetByNumber_Failed.ToInvariant(iban));

	/// <summary>
	/// Error that indicates an exception during the account service.
	/// </summary>
	public static ApiError GetByNumberNotFound(string iban) =>
		ApiError.CreateNotFound($"{ErrorPrefix}.{GetByNumberNotFound}",
			RESX.AccountService_AccountNumber_NotFound.ToInvariant(iban));

	/// <summary>
	/// Error that indicates an exception during the account service.
	/// </summary>
	public static ApiError CreateAccountNumberConflict(string iban) =>
		ApiError.CreateConflict($"{ErrorPrefix}.{CreateAccountNumberConflict}",
			RESX.AccountService_AccountNumber_Conflict.ToInvariant(iban));

	/// <summary>
	/// Error that indicates an exception during the account service.
	/// </summary>
	public static ApiError CreateCardNumberConflict(string pan) =>
		ApiError.CreateConflict($"{ErrorPrefix}.{CreateCardNumberConflict}",
			RESX.AccountService_CardNumber_Conflict.ToInvariant(pan));

	/// <summary>
	/// Error that indicates an exception during the account service.
	/// </summary>
	public static readonly ApiError CreateAccountFailed =
		ApiError.CreateFailed($"{ErrorPrefix}.{CreateAccountFailed}",
			RESX.AccountService_Create_Failed);

	/// <summary>
	/// Error that indicates an exception during the account service.
	/// </summary>
	public static ApiError DeleteAccountNotFound(Guid accountId) =>
		ApiError.CreateNotFound($"{ErrorPrefix}.{nameof(DeleteAccountNotFound)}",
			RESX.AccountService_AccountId_NotFound.ToInvariant(accountId));

	/// <summary>
	/// Error that indicates an exception during the account service.
	/// </summary>
	public static readonly ApiError DeleteAccountFailed =
		ApiError.CreateFailed($"{ErrorPrefix}.{DeleteAccountFailed}",
			RESX.AccountService_Delete_Failed);

	/// <summary>
	/// Error that indicates an exception during the account service.
	/// </summary>
	public static ApiError UpdateAccountNotFound(Guid accountId) =>
		ApiError.CreateBadRequest($"{ErrorPrefix}.{UpdateAccountNotFound}",
			RESX.AccountService_AccountId_NotFound.ToInvariant(accountId));

	/// <summary>
	/// Error that indicates an exception during the account service.
	/// </summary>
	public static ApiError UpdateCardNotFound(Guid cardId) =>
		ApiError.CreateBadRequest($"{ErrorPrefix}.{UpdateCardNotFound}",
			RESX.AccountService_CardId_NotFound.ToInvariant(cardId));

	/// <summary>
	/// Error that indicates an exception during the account service.
	/// </summary>
	public static ApiError UpdateCardTypeNotFound(Guid cardTypeId) =>
		ApiError.CreateBadRequest($"{ErrorPrefix}.{UpdateCardTypeNotFound}",
			RESX.AccountService_CardTypeId_NotFound.ToInvariant(cardTypeId));

	/// <summary>
	/// Error that indicates an exception during the account service.
	/// </summary>
	public static readonly ApiError UpdateAccountFailed =
		ApiError.CreateFailed($"{ErrorPrefix}.{UpdateAccountFailed}",
			RESX.AccountService_Update_Failed);
}
