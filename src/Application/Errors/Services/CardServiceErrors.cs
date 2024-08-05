using Application.Errors.Base;

using BB84.Extensions;

using RESX = Application.Properties.ServiceErrors;

namespace Application.Errors.Services;

/// <summary>
/// The static card service errors class.
/// </summary>
/// <remarks>
/// Contains errors that are relevant for the card service.
/// </remarks>
public static class CardServiceErrors
{
	private const string ErrorPrefix = $"{nameof(CardServiceErrors)}";

	/// <summary>
	/// Error that indicates an exception during the card service.
	/// </summary>
	public static readonly ApiError GetByUserIdFailed =
		ApiError.CreateFailed($"{ErrorPrefix}.{GetByUserIdFailed}",
			RESX.CardService_Create_Failed);

	/// <summary>
	/// Error that indicates an exception during the card service.
	/// </summary>
	public static readonly ApiError GetAllNotFound =
		ApiError.CreateNotFound($"{ErrorPrefix}.{GetAllNotFound}",
			RESX.CardService_GetAll_NotFound);

	/// <summary>
	/// Error that indicates an exception during the card service.
	/// </summary>
	/// <param name="cardId">The identifier of the bank card.</param>
	public static ApiError GetByCardIdFailed(Guid cardId) =>
		ApiError.CreateFailed($"{ErrorPrefix}.{GetByCardIdFailed}",
			RESX.CardService_GetById_Failed.FormatInvariant(cardId));

	/// <summary>
	/// Error that indicates an exception during the card service.
	/// </summary>
	/// <param name="cardId">The identifier of the bank card.</param>
	public static ApiError GetByIdNotFound(Guid cardId) =>
		ApiError.CreateNotFound($"{ErrorPrefix}.{GetByIdNotFound}",
			RESX.CardService_GetById_NotFound.FormatInvariant(cardId));

	/// <summary>
	/// Error that indicates an exception during the card service.
	/// </summary>
	/// <param name="pan">The payment card number of the bank card.</param>
	public static ApiError GetByNumberFailed(string pan) =>
		ApiError.CreateFailed($"{ErrorPrefix}.{GetByNumberFailed}",
			RESX.CardService_GetByNumber_Failed.FormatInvariant(pan));

	/// <summary>
	/// Error that indicates an exception during the card service.
	/// </summary>
	/// <param name="pan">The payment card number of the bank card.</param>
	public static ApiError GetByNumberNotFound(string pan) =>
		ApiError.CreateNotFound($"{ErrorPrefix}.{GetByNumberNotFound}",
			RESX.CardService_GetByNumber_NotFound.FormatInvariant(pan));

	/// <summary>
	/// Error that indicates an exception during the card service.
	/// </summary>
	public static readonly ApiError CreateFailed =
		ApiError.CreateFailed($"{ErrorPrefix}.{CreateFailed}",
			RESX.CardService_Create_Failed);

	/// <summary>
	/// Error that indicates an exception during the card service.
	/// </summary>
	/// <param name="accountId">The identifier of the bank account.</param>
	public static ApiError CreateAccountIdNotFound(Guid accountId) =>
		ApiError.CreateNotFound($"{ErrorPrefix}.{CreateAccountIdNotFound}",
			RESX.CardService_GetAccountById_NotFound.FormatInvariant(accountId));

	/// <summary>
	/// Error that indicates an exception during the card service.
	/// </summary>
	/// <param name="pan">The payment card number of the bank card.</param>
	public static ApiError CreateNumberConflict(string pan) =>
		ApiError.CreateConflict($"{ErrorPrefix}.{CreateNumberConflict}",
			RESX.CardService_Number_Conflict.FormatInvariant(pan));

	/// <summary>
	/// Error that indicates an exception during the card service.
	/// </summary>
	/// <param name="pan">The payment card number of the bank card.</param>
	public static ApiError CreateNumberInvalid(string pan) =>
		ApiError.CreateConflict($"{ErrorPrefix}.{CreateNumberInvalid}",
			RESX.CardService_Number_Invalid.FormatInvariant(pan));

	/// <summary>
	/// Error that indicates an exception during the card service.
	/// </summary>
	public static readonly ApiError DeleteFailed =
		ApiError.CreateFailed($"{ErrorPrefix}.{DeleteFailed}",
			RESX.CardService_Delete_Failed);

	/// <summary>
	/// Error that indicates an exception during the card service.
	/// </summary>
	public static readonly ApiError UpdateFailed =
		ApiError.CreateFailed($"{ErrorPrefix}.{UpdateFailed}",
			RESX.CardService_Update_Failed);
}
