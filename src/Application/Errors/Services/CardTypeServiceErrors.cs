using Application.Errors.Base;
using Application.Features.Requests;
using Application.Services;
using Domain.Extensions;
using System.Globalization;
using RESX = Application.Properties.ServiceErrors;

namespace Application.Errors.Services;

/// <summary>
/// The static card type service errors class.
/// </summary>
/// <remarks>
/// Contains errors that are relevant for the card type service.
/// </remarks>
internal static class CardTypeServiceErrors
{
	private readonly static CultureInfo CurrentCulture = Domain.Statics.CurrentCulture;
	private const string ErrorPrefix = $"{nameof(CardTypeServiceErrors)}";

	/// <summary>
	/// Error that indicates an exception during the
	/// <see cref="CardTypeService.GetById(int, bool, CancellationToken)"/> method.
	/// </summary>
	public static readonly ApiError GetByIdFailed =
		ApiError.CreateFailed($"{ErrorPrefix}.{nameof(GetByIdFailed)}",
			RESX.CardTypeService_GetById_Failed);

	/// <summary>
	/// Error that indicates an exception during the
	/// <see cref="CardTypeService.GetById(int, bool, CancellationToken)"/> method.
	/// </summary>
	public static ApiError GetByIdNotFound(int id) =>
		ApiError.CreateNotFound($"{ErrorPrefix}.{nameof(GetByIdNotFound)}",
			RESX.CardTypeService_GetById_NotFound.Format(CurrentCulture, id));

	/// <summary>
	/// Error that indicates an exception during the
	/// <see cref="CardTypeService.GetByName(string, bool, CancellationToken)"/> method.
	/// </summary>
	public static readonly ApiError GetByNameFailed =
		ApiError.CreateFailed($"{ErrorPrefix}.{nameof(GetByNameFailed)}",
			RESX.CardTypeService_GetByName_Failed);

	/// <summary>
	/// Error that indicates an exception during the
	/// <see cref="CardTypeService.GetByName(string, bool, CancellationToken)"/> method.
	/// </summary>
	public static ApiError GetByNameNotFound(string name) =>
		ApiError.CreateNotFound($"{ErrorPrefix}.{nameof(GetByNameNotFound)}",
			RESX.CardTypeService_GetByName_NotFound.Format(CurrentCulture, name));

	/// <summary>
	/// Error that indicates an exception during the
	/// <see cref="CardTypeService.GetPagedByParameters(CardTypeParameters, bool, CancellationToken)"/> method.
	/// </summary>
	public static readonly ApiError GetPagedByParametersFailed =
		ApiError.CreateFailed($"{ErrorPrefix}.{nameof(GetPagedByParametersFailed)}",
			RESX.CardTypeService_GetPagedByParameters_Failed);

	/// <summary>
	/// Error that indicates an exception during the
	/// <see cref="CardTypeService.GetPagedByParameters(CardTypeParameters, bool, CancellationToken)"/> method.
	/// </summary>
	public static readonly ApiError GetPagedByParametersNotFound =
		ApiError.CreateNotFound($"{ErrorPrefix}.{nameof(GetPagedByParametersNotFound)}",
			RESX.CardTypeService_GetPagedByParameters_NotFound);
}
