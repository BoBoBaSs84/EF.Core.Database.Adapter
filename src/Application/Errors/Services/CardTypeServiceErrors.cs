using Application.Errors.Base;
using Application.Features.Requests;
using Application.Services;
using Domain.Extensions;
using RESX = Application.Properties.Resources;

namespace Application.Errors.Services;

/// <summary>
/// The static card type service errors class.
/// </summary>
/// <remarks>
/// Contains errors that are relevant for the card type service.
/// </remarks>
internal static class CardTypeServiceErrors
{
	private const string ErrorPrefix = $"{nameof(CardTypeServiceErrors)}";

	/// <summary>
	/// Error that indicates an exception during the
	/// <see cref="CardTypeService.GetById(int, bool, CancellationToken)"/> method.
	/// </summary>
	public static readonly ApiError GetByIdFailed =
		ApiError.CreateFailed($"{ErrorPrefix}.{nameof(GetByIdFailed)}",
			RESX.CardTypeServiceErrors_GetByIdFailed);

	/// <summary>
	/// Error that indicates an exception during the
	/// <see cref="CardTypeService.GetById(int, bool, CancellationToken)"/> method.
	/// </summary>
	public static ApiError GetByIdNotFound(int id) =>
		ApiError.CreateNotFound($"{ErrorPrefix}.{nameof(GetByIdNotFound)}",
			RESX.CardTypeServiceErrors_GetByIdNotFound.FormatInvariant(id));

	/// <summary>
	/// Error that indicates an exception during the
	/// <see cref="CardTypeService.GetByName(string, bool, CancellationToken)"/> method.
	/// </summary>
	public static readonly ApiError GetByNameFailed =
		ApiError.CreateFailed($"{ErrorPrefix}.{nameof(GetByNameFailed)}",
			RESX.CardTypeServiceErrors_GetByNameFailed);

	/// <summary>
	/// Error that indicates an exception during the
	/// <see cref="CardTypeService.GetByName(string, bool, CancellationToken)"/> method.
	/// </summary>
	public static ApiError GetByNameNotFound(string name) =>
		ApiError.CreateNotFound($"{ErrorPrefix}.{nameof(GetByNameNotFound)}",
			RESX.CardTypeServiceErrors_GetByNameNotFound.FormatInvariant(name));

	/// <summary>
	/// Error that indicates an exception during the
	/// <see cref="CardTypeService.GetPagedByParameters(CardTypeParameters, bool, CancellationToken)"/> method.
	/// </summary>
	public static readonly ApiError GetPagedByParametersFailed =
		ApiError.CreateFailed($"{ErrorPrefix}.{nameof(GetPagedByParametersFailed)}",
			RESX.CardTypeServiceErrors_GetPagedByParametersFailed);

	/// <summary>
	/// Error that indicates an exception during the
	/// <see cref="CardTypeService.GetPagedByParameters(CardTypeParameters, bool, CancellationToken)"/> method.
	/// </summary>
	public static readonly ApiError GetPagedByParametersNotFound =
		ApiError.CreateNotFound($"{ErrorPrefix}.{nameof(GetPagedByParametersNotFound)}",
			RESX.CardTypeServiceErrors_GetPagedByParametersNotFound);
}
