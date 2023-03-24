using Application.Errors.Base;
using Application.Services;
using Domain.Extensions;
using RESX = Application.Properties.ServiceErrors;

namespace Application.Errors.Services;

/// <summary>
/// The static <see cref="AccountServiceErrors"/> class.
/// </summary>
/// <remarks>
/// Contains errors that are relevant for the account service.
/// </remarks>
public static class AccountServiceErrors
{
	private static readonly CultureInfo CurrentCulture = CultureInfo.CurrentCulture;
	private const string ErrorPrefix = $"{nameof(AttendanceServiceErrors)}";

	/// <summary>
	/// Error that indicates an exception during the
	/// <see cref="AccountService.GetAll(int, bool, CancellationToken)"/> method.
	/// </summary>
	public static readonly ApiError GetAllFailed =
		ApiError.CreateFailed($"{ErrorPrefix}.{GetAllFailed}",
			RESX.AccountService_GetAll_Failed);

	/// <summary>
	/// Error that indicates an exception during the
	/// <see cref="AccountService.GetAll(int, bool, CancellationToken)"/> method.
	/// </summary>
	public static readonly ApiError GetAllNotFound =
		ApiError.CreateNotFound($"{ErrorPrefix}.{GetAllNotFound}",
			RESX.AccountService_GetAll_NotFound);

	/// <summary>
	/// Error that indicates an exception during the
	/// <see cref="AccountService.GetByNumber(int, string, bool, CancellationToken)"/> method.
	/// </summary>
	public static ApiError GetByNumberFailed(string iban) =>
		ApiError.CreateFailed($"{ErrorPrefix}.{GetByNumberFailed}",
			RESX.AccountService_GetByNumber_Failed.Format(CultureInfo.CurrentCulture, iban));

	/// <summary>
	/// Error that indicates an exception during the
	/// <see cref="AccountService.GetByNumber(int, string, bool, CancellationToken)"/> method.
	/// </summary>	
	public static ApiError GetByNumberNotFound(string iban) =>
		ApiError.CreateNotFound($"{ErrorPrefix}.{GetByNumberNotFound}",
			RESX.AccountService_GetByNumber_NotFound.Format(CultureInfo.CurrentCulture, iban));

}
