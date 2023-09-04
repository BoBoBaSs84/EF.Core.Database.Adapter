using System.Globalization;

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
	private static readonly CultureInfo CurrentCulture = CultureInfo.CurrentCulture;
	private const string ErrorPrefix = $"{nameof(TransactionServiceErrors)}";

	/// <summary>
	/// Error that indicates an exception during the <see cref="ITransactionService.GetById(Guid, bool, CancellationToken)"/> method.
	/// </summary>
	/// <param name="id">The identifier of the bank transaction.</param>
	// TODO:
	public static ApiError GetByIdFailed(Guid id) =>
		ApiError.CreateFailed($"{ErrorPrefix}.{GetByIdFailed}",
			RESX.TransactionServiceErrors_GetById_Failed.Format(CultureInfo.CurrentCulture, id));

	/// <summary>
	/// Error that indicates an exception during the <see cref="ITransactionService.GetById(Guid, bool, CancellationToken)"/> method.
	/// </summary>
	/// <param name="id">The identifier of the bank transaction.</param>
	// TODO:
	public static ApiError GetByIdNotFound(Guid id) =>
		ApiError.CreateNotFound($"{ErrorPrefix}.{GetByIdNotFound}",
			RESX.TransactionServiceErrors_GetById_NotFound.Format(CultureInfo.CurrentCulture, id));
}
