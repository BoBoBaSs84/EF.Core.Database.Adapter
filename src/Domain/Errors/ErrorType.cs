namespace Domain.Errors;

/// <summary>
/// Error types.
/// </summary>
public enum ErrorType
{
	Failure,

	Unexpected,

	Validation,

	Conflict,

	NotFound,

	NoContent,

	Authentication,

	Composite
}
