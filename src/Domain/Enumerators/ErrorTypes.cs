using System.ComponentModel.DataAnnotations;
using RESX = Domain.Properties.EnumeratorResources;

namespace Domain.Enumerators;

/// <summary>
/// The error types.
/// </summary>
public enum ErrorTypes
{
	/// <summary>
	/// The <see cref="Failure"/> error type.
	/// </summary>
	[Display(ResourceType = typeof(RESX))]
	Failure,
	/// <summary>
	/// The <see cref="Unexpected"/> error type.
	/// </summary>
	[Display(ResourceType = typeof(RESX))]
	Unexpected,
	/// <summary>
	/// The <see cref="Validation"/> error type.
	/// </summary>
	[Display(ResourceType = typeof(RESX))]
	Validation,
	/// <summary>
	/// The <see cref="Conflict"/> error type.
	/// </summary>
	[Display(ResourceType = typeof(RESX))]
	Conflict,
	/// <summary>
	/// The <see cref="NotFound"/> error type.
	/// </summary>
	[Display(ResourceType = typeof(RESX))]
	NotFound,
	/// <summary>
	/// The <see cref="NoContent"/> error type.
	/// </summary>
	[Display(ResourceType = typeof(RESX))]
	NoContent,
	/// <summary>
	/// The <see cref="Unauthorized"/> error type.
	/// </summary>
	[Display(ResourceType = typeof(RESX))]
	Unauthorized,
	/// <summary>
	/// The <see cref="Composite"/> error type.
	/// </summary>
	[Display(ResourceType = typeof(RESX))]
	Composite,
	/// <summary>
	/// The <see cref="Forbidden"/> error type.
	/// </summary>
	[Display(ResourceType = typeof(RESX))]
	Forbidden
}
