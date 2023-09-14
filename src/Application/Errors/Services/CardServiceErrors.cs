using Application.Contracts.Requests.Finance;
using Application.Errors.Base;
using Application.Services;

using Domain.Extensions;

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
	/// Error that indicates an exception during the
	/// <see cref="CardService.Get(Guid, bool, CancellationToken)"/> method.
	/// </summary>
	public static readonly ApiError GetAllFailed =
		ApiError.CreateFailed($"{ErrorPrefix}.{GetAllFailed}", RESX.CardService_Create_Failed);

	/// <summary>
	/// Error that indicates an exception during the
	/// <see cref="CardService.Get(Guid, bool, CancellationToken)"/> method.
	/// </summary>
	public static readonly ApiError GetAllNotFound =
		ApiError.CreateNotFound($"{ErrorPrefix}.{GetAllNotFound}", RESX.CardService_GetAll_NotFound);

	/// <summary>
	/// Error that indicates an exception during the
	/// <see cref="CardService.Get(Guid, Guid, bool, CancellationToken)"/> method.
	/// </summary>
	public static ApiError GetByIdFailed(Guid cardId) =>
		ApiError.CreateFailed($"{ErrorPrefix}.{GetByIdFailed}",
			RESX.CardService_GetById_Failed.ToInvariant(cardId));

	/// <summary>
	/// Error that indicates an exception during the
	/// <see cref="CardService.Get(Guid, Guid, bool, CancellationToken)"/> method.
	/// </summary>	
	public static ApiError GetByIdNotFound(Guid cardId) =>
		ApiError.CreateNotFound($"{ErrorPrefix}.{GetByIdNotFound}",
			RESX.CardService_GetById_NotFound.ToInvariant(cardId));

	/// <summary>
	/// Error that indicates an exception during the
	/// <see cref="CardService.Get(Guid, string, bool, CancellationToken)"/> method.
	/// </summary>
	public static ApiError GetByNumberFailed(string pam) =>
		ApiError.CreateFailed($"{ErrorPrefix}.{GetByNumberFailed}",
			RESX.CardService_GetByNumber_Failed.ToInvariant(pam));

	/// <summary>
	/// Error that indicates an exception during the
	/// <see cref="CardService.Get(Guid, string, bool, CancellationToken)"/> method.
	/// </summary>	
	public static ApiError GetByNumberNotFound(string pam) =>
		ApiError.CreateNotFound($"{ErrorPrefix}.{GetByNumberNotFound}",
			RESX.CardService_GetByNumber_NotFound.ToInvariant(pam));

	/// <summary>
	/// Error that indicates an exception during the
	/// <see cref="CardService.Create(Guid, Guid, CardCreateRequest, CancellationToken)"/> method.
	/// </summary>
	public static readonly ApiError CreateFailed =
		ApiError.CreateFailed($"{ErrorPrefix}.{CreateFailed}", RESX.CardService_Create_Failed);

	/// <summary>
	/// Error that indicates an exception during the
	/// <see cref="CardService.Create(Guid, Guid, CardCreateRequest, CancellationToken)"/> method.
	/// </summary>
	public static ApiError CreateAccountIdNotFound(Guid accountId) =>
		ApiError.CreateNotFound($"{ErrorPrefix}.{CreateAccountIdNotFound}",
			RESX.CardService_GetAccountById_NotFound.ToInvariant(accountId));

	/// <summary>
	/// Error that indicates an exception during the
	/// <see cref="CardService.Create(Guid, Guid, CardCreateRequest, CancellationToken)"/> method.
	/// </summary>
	public static ApiError CreateNumberConflict(string pam)
		=> ApiError.CreateConflict($"{ErrorPrefix}.{CreateNumberConflict}",
			RESX.CardService_Number_Conflict.ToInvariant(pam));

	/// <summary>
	/// Error that indicates an exception during the
	/// <see cref="CardService.Delete(Guid, Guid, CancellationToken)"/> method.
	/// </summary>
	public static readonly ApiError DeleteFailed =
		ApiError.CreateFailed($"{ErrorPrefix}.{DeleteFailed}", RESX.CardService_Delete_Failed);

	/// <summary>
	/// Error that indicates an exception during the
	/// <see cref="CardService.Delete(Guid, Guid, CancellationToken)"/> method.
	/// </summary>
	public static readonly ApiError UpdateFailed =
		ApiError.CreateFailed($"{ErrorPrefix}.{UpdateFailed}", RESX.CardService_Update_Failed);
}
