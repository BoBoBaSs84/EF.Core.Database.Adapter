using BB84.Home.Domain.Enumerators;

namespace BB84.Home.Domain.Errors;

/// <summary>
/// Represents an error.
/// </summary>
public record class Error
{
	/// <summary>
	/// Gets the unique error code.
	/// </summary>
	public string Code { get; }

	/// <summary>
	/// Gets the error description.
	/// </summary>
	public string Description { get; }

	/// <summary>
	/// Gets the error type.
	/// </summary>
	public ErrorType Type { get; }

	/// <summary>
	/// Gets the numeric value of the type.
	/// </summary>
	public int NumericType { get; }

	private Error(string code, string description, ErrorType type)
	{
		Code = code;
		Description = description;
		Type = type;
		NumericType = (int)type;
	}

	/// <summary>
	/// Creates an <see cref="Error"/> of type <see cref="ErrorType.Failure"/> from a code and description.
	/// </summary>
	/// <param name="code">The unique error code.</param>
	/// <param name="description">The error description.</param>
	public static Error Failure(
		string code = "Error.Failure",
		string description = "A failure has occurred.") =>
		new(code, description, ErrorType.Failure);

	/// <summary>
	/// Creates an <see cref="Error"/> of type <see cref="ErrorType.Forbidden"/> from a code and description.
	/// </summary>
	/// <param name="code">The unique error code.</param>
	/// <param name="description">The error description.</param>
	public static Error Forbidden(
		string code = "Error.Forbidden",
		string description = "A forbidden error has occurred.") =>
		new(code, description, ErrorType.Forbidden);

	/// <summary>
	/// Creates an <see cref="Error"/> of type <see cref="ErrorType.Unexpected"/> from a code and description.
	/// </summary>
	/// <param name="code">The unique error code.</param>
	/// <param name="description">The error description.</param>
	public static Error Unexpected(
		string code = "Error.Unexpected",
		string description = "An unexpected error has occurred.") =>
		new(code, description, ErrorType.Unexpected);

	/// <summary>
	/// Creates an <see cref="Error"/> of type <see cref="ErrorType.Validation"/> from a code and description.
	/// </summary>
	/// <param name="code">The unique error code.</param>
	/// <param name="description">The error description.</param>
	public static Error Validation(
		string code = "Error.Validation",
		string description = "An validation error has occurred.") =>
		new(code, description, ErrorType.Validation);

	/// <summary>
	/// Creates an <see cref="Error"/> of type <see cref="ErrorType.Conflict"/> from a code and description.
	/// </summary>
	/// <param name="code">The unique error code.</param>
	/// <param name="description">The error description.</param>
	public static Error Conflict(
		string code = "Error.Conflict",
		string description = "A conflict error has occurred.") =>
		new(code, description, ErrorType.Conflict);

	/// <summary>
	/// Creates an <see cref="Error"/> of type <see cref="ErrorType.NotFound"/> from a code and description.
	/// </summary>
	/// <param name="code">The unique error code.</param>
	/// <param name="description">The error description.</param>
	public static Error NotFound(
		string code = "Error.NotFound",
		string description = "A 'Not Found' error has occurred.") =>
		new(code, description, ErrorType.NotFound);

	/// <summary>
	/// Creates an <see cref="Error"/> of type <see cref="ErrorType.NoContent"/> from a code and description.
	/// </summary>
	/// <param name="code">The unique error code.</param>
	/// <param name="description">The error description.</param>
	public static Error NoContent(
		string code = "Error.NoContent",
		string description = "A 'No Content' error has occurred.") =>
		new(code, description, ErrorType.NoContent);

	/// <summary>
	/// Creates an <see cref="Error"/> of type <see cref="ErrorType.Unauthorized"/> from a code and description.
	/// </summary>
	/// <param name="code">The unique error code.</param>
	/// <param name="description">The error description.</param>
	public static Error Unauthorized(
		string code = "Error.Unauthorized",
		string description = "An 'Authentication' error has occurred.") =>
		new(code, description, ErrorType.Unauthorized);

	/// <summary>
	/// Creates an <see cref="Error"/> of type <see cref="ErrorType.Composite"/> from a code and description.
	/// </summary>
	/// <param name="code">The unique error code.</param>
	/// <param name="description">The error description.</param>
	public static Error Composite(
		string code = "Error.Composite",
		string description = "A collection of errors has occurred.") =>
		new(code, description, ErrorType.Composite);

	/// <summary>
	/// Creates an <see cref="Error"/> with the given numeric <paramref name="type"/>,
	/// <paramref name="code"/>, and <paramref name="description"/>.
	/// </summary>
	/// <param name="type">The error type</param>
	/// <param name="code">The unique error code.</param>
	/// <param name="description">The error description.</param>
	public static Error Custom(
		ErrorType type,
		string code,
		string description) =>
		new(code, description, type);

	/// <summary>
	/// Creates an <see cref="Error"/> with the given numeric <paramref name="type"/>,
	/// <paramref name="code"/>, and <paramref name="description"/>.
	/// </summary>
	/// <param name="type">An integer value which represents the type of error that occurred.</param>
	/// <param name="code">The unique error code.</param>
	/// <param name="description">The error description.</param>
	public static Error Custom(
		int type,
		string code,
		string description) =>
		Custom((ErrorType)type, code, description);
}
