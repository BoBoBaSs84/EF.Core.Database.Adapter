using Application.Errors.Base;

using BB84.Extensions;

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
	private const string ErrorPrefix = nameof(AccountServiceErrors);

	/// <summary>
	/// Error that indicates an exception during the account service.
	/// </summary>
	public static ApiError GetByUserIdFailed(Guid id)
		=> ApiError.CreateFailed($"{ErrorPrefix}.{GetByUserIdFailed}",
			RESX.AccountService_GetByUserId_Failed.FormatInvariant(id));

	/// <summary>
	/// Error that indicates an exception during the account service.
	/// </summary>
	public static ApiError GetByIdFailed(Guid id)
		=> ApiError.CreateFailed($"{ErrorPrefix}.{GetByIdFailed}",
			RESX.AccountService_GetByAccountId_Failed.FormatInvariant(id));

	/// <summary>
	/// Error that indicates an exception during the account service.
	/// </summary>
	public static ApiError GetByIdNotFound(Guid id)
		=> ApiError.CreateNotFound($"{ErrorPrefix}.{GetByIdNotFound}",
			RESX.AccountService_GetByAccountId_NotFound.FormatInvariant(id));

	/// <summary>
	/// Error that indicates an exception during the account service.
	/// </summary>
	public static ApiError CreateAccountNumberConflict(string iban)
		=> ApiError.CreateConflict($"{ErrorPrefix}.{CreateAccountNumberConflict}",
			RESX.AccountService_AccountNumber_Conflict.FormatInvariant(iban));

	/// <summary>
	/// Error that indicates an exception during the account service.
	/// </summary>
	public static ApiError CreateCardNumberConflict(string pan)
		=> ApiError.CreateConflict($"{ErrorPrefix}.{CreateCardNumberConflict}",
			RESX.AccountService_CardNumber_Conflict.FormatInvariant(pan));

	/// <summary>
	/// Error that indicates an exception during the account service.
	/// </summary>
	public static ApiError CreateAccountFailed(string iban)
		=> ApiError.CreateFailed($"{ErrorPrefix}.{CreateAccountFailed}",
			RESX.AccountService_Create_Failed.FormatInvariant(iban));

	/// <summary>
	/// Error that indicates an exception during the account service.
	/// </summary>
	public static ApiError DeleteAccountNotFound(Guid id)
		=> ApiError.CreateNotFound($"{ErrorPrefix}.{nameof(DeleteAccountNotFound)}",
			RESX.AccountService_Delete_NotFound.FormatInvariant(id));

	/// <summary>
	/// Error that indicates an exception during the account service.
	/// </summary>
	public static ApiError DeleteAccountFailed(Guid id)
		=> ApiError.CreateFailed($"{ErrorPrefix}.{DeleteAccountFailed}",
			RESX.AccountService_Delete_NotFound.FormatInvariant(id));

	/// <summary>
	/// Error that indicates an exception during the account service.
	/// </summary>
	public static ApiError UpdateAccountNotFound(Guid id)
		=> ApiError.CreateBadRequest($"{ErrorPrefix}.{UpdateAccountNotFound}",
			RESX.AccountService_Update_NotFound.FormatInvariant(id));

	/// <summary>
	/// Error that indicates an exception during the account service.
	/// </summary>
	public static ApiError UpdateAccountFailed(Guid id)
		=> ApiError.CreateFailed($"{ErrorPrefix}.{UpdateAccountFailed}",
			RESX.AccountService_Update_Failed.FormatInvariant(id));
}
