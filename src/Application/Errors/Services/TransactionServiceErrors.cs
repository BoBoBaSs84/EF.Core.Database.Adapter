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
	public static ApiError CreateByAccountIdFailed(Guid id) =>
		ApiError.CreateFailed($"{ErrorPrefix}.{CreateByAccountIdFailed}",
			RESX.TransactionServiceErrors_CreateByAccountId_Failed.FormatInvariant(id));

	/// <summary>
	/// Error that indicates an exception during the transaction service.
	/// </summary>
	public static ApiError CreateByAccountIdNotFound(Guid id) =>
		ApiError.CreateFailed($"{ErrorPrefix}.{CreateByAccountIdNotFound}",
			RESX.TransactionServiceErrors_CreateByAccountId_NotFound.FormatInvariant(id));

	/// <summary>
	/// Error that indicates an exception during the transaction service.
	/// </summary>
	public static ApiError CreateByCardIdFailed(Guid id) =>
		ApiError.CreateFailed($"{ErrorPrefix}.{CreateByCardIdFailed}",
			RESX.TransactionServiceErrors_CreateByCardId_Failed.FormatInvariant(id));

	/// <summary>
	/// Error that indicates an exception during the transaction service.
	/// </summary>
	public static ApiError CreateByCardIdNotFound(Guid id) =>
		ApiError.CreateFailed($"{ErrorPrefix}.{CreateByCardIdNotFound}",
			RESX.TransactionServiceErrors_CreateByCardId_NotFound.FormatInvariant(id));

	/// <summary>
	/// Error that indicates an exception during the transaction service.
	/// </summary>
	public static ApiError DeleteByAccountIdFailed(Guid id)
		=> ApiError.CreateFailed($"{ErrorPrefix}.{DeleteByAccountIdFailed}",
			RESX.TransactionServiceErrors_Delete_Failed.FormatInvariant(id));

	/// <summary>
	/// Error that indicates an exception during the transaction service.
	/// </summary>
	public static ApiError DeleteByAccountIdNotFound(Guid id)
		=> ApiError.CreateFailed($"{ErrorPrefix}.{DeleteByAccountIdFailed}",
			RESX.TransactionServiceErrors_Delete_NotFound.FormatInvariant(id));

	/// <summary>
	/// Error that indicates an exception during the transaction service.
	/// </summary>
	public static ApiError DeleteByCardIdFailed(Guid id)
		=> ApiError.CreateFailed($"{ErrorPrefix}.{DeleteByCardIdFailed}",
			RESX.TransactionServiceErrors_Delete_Failed.FormatInvariant(id));

	/// <summary>
	/// Error that indicates an exception during the transaction service.
	/// </summary>
	public static ApiError DeleteByCardIdNotFound(Guid id)
		=> ApiError.CreateFailed($"{ErrorPrefix}.{DeleteByCardIdNotFound}",
			RESX.TransactionServiceErrors_Delete_NotFound.FormatInvariant(id));

	/// <summary>
	/// Error that indicates an exception during the transaction service.
	/// </summary>
	public static ApiError GetByIdFailed(Guid id) =>
		ApiError.CreateFailed($"{ErrorPrefix}.{GetByIdFailed}",
			RESX.TransactionServiceErrors_GetById_Failed.FormatInvariant(id));

	/// <summary>
	/// Error that indicates an exception during the transaction service.
	/// </summary>
	public static ApiError GetByIdNotFound(Guid id) =>
		ApiError.CreateNotFound($"{ErrorPrefix}.{GetByIdNotFound}",
			RESX.TransactionServiceErrors_GetById_NotFound.FormatInvariant(id));

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
	public static ApiError UpdateByAccountIdFailed(Guid id)
		=> ApiError.CreateFailed($"{ErrorPrefix}.{UpdateByAccountIdFailed}",
			RESX.TransactionServiceErrors_Update_Failed.FormatInvariant(id));

	/// <summary>
	/// Error that indicates an exception during the transaction service.
	/// </summary>
	public static ApiError UpdateByAccountIdNotFound(Guid id)
		=> ApiError.CreateFailed($"{ErrorPrefix}.{UpdateByAccountIdNotFound}",
			RESX.TransactionServiceErrors_Update_NotFound.FormatInvariant(id));

	/// <summary>
	/// Error that indicates an exception during the transaction service.
	/// </summary>
	public static ApiError UpdateByCardIdFailed(Guid id)
		=> ApiError.CreateFailed($"{ErrorPrefix}.{UpdateByCardIdFailed}",
			RESX.TransactionServiceErrors_Update_Failed.FormatInvariant(id));

	/// <summary>
	/// Error that indicates an exception during the transaction service.
	/// </summary>
	public static ApiError UpdateByCardIdNotFound(Guid id)
		=> ApiError.CreateFailed($"{ErrorPrefix}.{UpdateByCardIdNotFound}",
			RESX.TransactionServiceErrors_Delete_NotFound.FormatInvariant(id));
}
