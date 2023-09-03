using System.ComponentModel.DataAnnotations;

using RESX = Domain.Properties.EnumeratorResources;

namespace Domain.Enumerators;

/// <summary>
/// The error types.
/// </summary>
public enum ErrorType
{
	/// <summary>
	/// The <see cref="Failure"/> error type.
	/// </summary>
	[Display(Description = nameof(RESX.ErrorTypes_Failure_Description),
		Name = nameof(RESX.ErrorTypes_Failure_Name),
		ShortName = nameof(RESX.ErrorTypes_Failure_ShortName),
		ResourceType = typeof(RESX))]
	Failure = 1,
	/// <summary>
	/// The <see cref="Unexpected"/> error type.
	/// </summary>
	[Display(Description = nameof(RESX.ErrorTypes_Unexpected_Description),
		Name = nameof(RESX.ErrorTypes_Unexpected_Name),
		ShortName = nameof(RESX.ErrorTypes_Unexpected_ShortName),
		ResourceType = typeof(RESX))]
	Unexpected,
	/// <summary>
	/// The <see cref="Validation"/> error type.
	/// </summary>
	[Display(Description = nameof(RESX.ErrorTypes_Validation_Description),
		Name = nameof(RESX.ErrorTypes_Validation_Name),
		ShortName = nameof(RESX.ErrorTypes_Validation_ShortName),
		ResourceType = typeof(RESX))]
	Validation,
	/// <summary>
	/// The <see cref="Conflict"/> error type.
	/// </summary>
	[Display(Description = nameof(RESX.ErrorTypes_Conflict_Description),
		Name = nameof(RESX.ErrorTypes_Conflict_Name),
		ShortName = nameof(RESX.ErrorTypes_Conflict_ShortName),
		ResourceType = typeof(RESX))]
	Conflict,
	/// <summary>
	/// The <see cref="NotFound"/> error type.
	/// </summary>
	[Display(Description = nameof(RESX.ErrorTypes_NotFound_Description),
		Name = nameof(RESX.ErrorTypes_NotFound_Name),
		ShortName = nameof(RESX.ErrorTypes_NotFound_ShortName),
		ResourceType = typeof(RESX))]
	NotFound,
	/// <summary>
	/// The <see cref="NoContent"/> error type.
	/// </summary>
	[Display(Description = nameof(RESX.ErrorTypes_NotFound_Description),
		Name = nameof(RESX.ErrorTypes_NoContent_Name),
		ShortName = nameof(RESX.ErrorTypes_NoContent_ShortName),
		ResourceType = typeof(RESX))]
	NoContent,
	/// <summary>
	/// The <see cref="Unauthorized"/> error type.
	/// </summary>
	[Display(Description = nameof(RESX.ErrorTypes_Unauthorized_Description),
		Name = nameof(RESX.ErrorTypes_Unauthorized_Name),
		ShortName = nameof(RESX.ErrorTypes_Unauthorized_ShortName),
		ResourceType = typeof(RESX))]
	Unauthorized,
	/// <summary>
	/// The <see cref="Composite"/> error type.
	/// </summary>
	[Display(Description = nameof(RESX.ErrorTypes_Composite_Description),
		Name = nameof(RESX.ErrorTypes_Composite_Name),
		ShortName = nameof(RESX.ErrorTypes_Composite_ShortName),
		ResourceType = typeof(RESX))]
	Composite,
	/// <summary>
	/// The <see cref="Forbidden"/> error type.
	/// </summary>
	[Display(Description = nameof(RESX.ErrorTypes_Forbidden_Description),
		Name = nameof(RESX.ErrorTypes_Forbidden_Name),
		ShortName = nameof(RESX.ErrorTypes_NoContent_Description),
		ResourceType = typeof(RESX))]
	Forbidden
}
