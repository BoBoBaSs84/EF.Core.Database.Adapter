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
	public static ApiError GetByUserIdFailed(Guid id)
		=> ApiError.CreateFailed($"{ErrorPrefix}.{GetByUserIdFailed}",
			RESX.CardService_GetByUserId_Failed.FormatInvariant(id));

	/// <summary>
	/// Error that indicates an exception during the card service.
	/// </summary>
	public static ApiError GetByIdFailed(Guid id)
		=> ApiError.CreateFailed($"{ErrorPrefix}.{GetByIdFailed}",
			RESX.CardService_GetById_Failed.FormatInvariant(id));

	/// <summary>
	/// Error that indicates an exception during the card service.
	/// </summary>
	public static ApiError GetByIdNotFound(Guid id)
		=> ApiError.CreateNotFound($"{ErrorPrefix}.{GetByIdNotFound}",
			RESX.CardService_GetById_NotFound.FormatInvariant(id));

	/// <summary>
	/// Error that indicates an exception during the card service.
	/// </summary>
	public static ApiError CreateFailed(Guid id)
		=> ApiError.CreateFailed($"{ErrorPrefix}.{CreateFailed}",
			RESX.CardService_Create_Failed.FormatInvariant(id));

	/// <summary>
	/// Error that indicates an exception during the card service.
	/// </summary>
	public static ApiError CreateAccountIdNotFound(Guid accountId)
		=> ApiError.CreateNotFound($"{ErrorPrefix}.{CreateAccountIdNotFound}",
			RESX.CardService_GetAccountById_NotFound.FormatInvariant(accountId));

	/// <summary>
	/// Error that indicates an exception during the card service.
	/// </summary>
	public static ApiError CreateNumberConflict(string pan)
		=> ApiError.CreateConflict($"{ErrorPrefix}.{CreateNumberConflict}",
			RESX.CardService_Number_Conflict.FormatInvariant(pan));

	/// <summary>
	/// Error that indicates an exception during the card service.
	/// </summary>
	public static ApiError DeleteFailed(Guid id)
		=> ApiError.CreateFailed($"{ErrorPrefix}.{DeleteFailed}",
			RESX.CardService_Delete_Failed.FormatInvariant(id));

	/// <summary>
	/// Error that indicates an exception during the card service.
	/// </summary>
	public static ApiError DeleteNotFound(Guid id)
		=> ApiError.CreateNotFound($"{ErrorPrefix}.{DeleteNotFound}",
			RESX.CardService_Delete_NotFound.FormatInvariant(id));

	/// <summary>
	/// Error that indicates an exception during the card service.
	/// </summary>
	public static ApiError UpdateFailed(Guid id)
		=> ApiError.CreateFailed($"{ErrorPrefix}.{UpdateFailed}",
			RESX.CardService_Update_Failed.FormatInvariant(id));

	/// <summary>
	/// Error that indicates an exception during the card service.
	/// </summary>
	public static ApiError UpdateNotFound(Guid id)
		=> ApiError.CreateNotFound($"{ErrorPrefix}.{UpdateNotFound}",
			RESX.CardService_Update_NotFound.FormatInvariant(id));
}
