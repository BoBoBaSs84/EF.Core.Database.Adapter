using Application.Errors.Base;
using Application.Interfaces.Application;

using Domain.Extensions;

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
	/// Error that indicates an exception during the <see cref="ITransactionService.GetById"/> method.
	/// </summary>
	/// <param name="id">The identifier of the bank transaction.</param>
	public static ApiError GetByIdFailed(Guid id) =>
		ApiError.CreateFailed($"{ErrorPrefix}.{GetByIdFailed}",
			RESX.TransactionServiceErrors_GetById_Failed.FormatInvariant(id));

	/// <summary>
	/// Error that indicates an exception during the <see cref="ITransactionService.GetById"/> method.
	/// </summary>
	/// <param name="id">The identifier of the bank transaction.</param>
	public static ApiError GetByIdNotFound(Guid id) =>
		ApiError.CreateNotFound($"{ErrorPrefix}.{GetByIdNotFound}",
			RESX.TransactionServiceErrors_GetById_NotFound.FormatInvariant(id));

	/// <summary>
	/// Error that indicates an exception during the <see cref="ITransactionService.GetByCardId"/> method.
	/// </summary>
	/// <param name="id">The identifier of the bank card.</param>
	public static ApiError GetByCardIdFailed(Guid id) =>
		ApiError.CreateFailed($"{ErrorPrefix}.{GetByCardIdFailed}",
			RESX.TransactionServiceErrors_GetByCardId_Failed.FormatInvariant(id));

	/// <summary>
	/// Error that indicates an exception during the <see cref="ITransactionService.GetByCardId"/> method.
	/// </summary>
	/// <param name="id">The identifier of the bank card.</param>
	public static ApiError GetByCardIdNotFound(Guid id) =>
		ApiError.CreateNotFound($"{ErrorPrefix}.{GetByCardIdNotFound}",
			RESX.TransactionServiceErrors_GetByCardId_NotFound.FormatInvariant(id));

	/// <summary>
	/// Error that indicates an exception during the <see cref="ITransactionService.GetByAccountId"/> method.
	/// </summary>
	/// <param name="id">The identifier of the bank account.</param>
	public static ApiError GetByAccountIdFailed(Guid id) =>
		ApiError.CreateFailed($"{ErrorPrefix}.{GetByAccountIdFailed}",
			RESX.TransactionServiceErrors_GetByAccountId_Failed.FormatInvariant(id));

	/// <summary>
	/// Error that indicates an exception during the <see cref="ITransactionService.GetByAccountId"/> method.
	/// </summary>
	/// <param name="id">The identifier of the bank account.</param>
	public static ApiError GetByAccountIdNotFound(Guid id) =>
		ApiError.CreateNotFound($"{ErrorPrefix}.{GetByAccountIdNotFound}",
			RESX.TransactionServiceErrors_GetByAccountId_NotFound.FormatInvariant(id));
}
