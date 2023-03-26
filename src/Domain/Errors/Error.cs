using Domain.Enumerators;

namespace Domain.Errors;

/// <summary>
/// Represents an error.
/// </summary>
[SuppressMessage("Naming", "CA1716:Identifiers should not match keywords")]
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
	public ErrorTypes Type { get; }

	/// <summary>
	/// Gets the numeric value of the type.
	/// </summary>
	public int NumericType { get; }

	private Error(string code, string description, ErrorTypes type)
	{
		Code = code;
		Description = description;
		Type = type;
		NumericType = (int)type;
	}

	/// <summary>
	/// Creates an <see cref="Error"/> of type <see cref="ErrorTypes.Failure"/> from a code and description.
	/// </summary>
	/// <param name="code">The unique error code.</param>
	/// <param name="description">The error description.</param>
	public static Error Failure(
		string code = "Error.Failure",
		string description = "A failure has occurred.") =>
		new(code, description, ErrorTypes.Failure);

	/// <summary>
	/// Creates an <see cref="Error"/> of type <see cref="ErrorTypes.Forbidden"/> from a code and description.
	/// </summary>
	/// <param name="code">The unique error code.</param>
	/// <param name="description">The error description.</param>
	public static Error Forbidden(
		string code = "Error.Forbidden",
		string description = "A forbidden error has occurred.") =>
		new(code, description, ErrorTypes.Forbidden);

	/// <summary>
	/// Creates an <see cref="Error"/> of type <see cref="ErrorTypes.Unexpected"/> from a code and description.
	/// </summary>
	/// <param name="code">The unique error code.</param>
	/// <param name="description">The error description.</param>
	public static Error Unexpected(
		string code = "Error.Unexpected",
		string description = "An unexpected error has occurred.") =>
		new(code, description, ErrorTypes.Unexpected);

	/// <summary>
	/// Creates an <see cref="Error"/> of type <see cref="ErrorTypes.Validation"/> from a code and description.
	/// </summary>
	/// <param name="code">The unique error code.</param>
	/// <param name="description">The error description.</param>
	public static Error Validation(
		string code = "Error.Validation",
		string description = "An validation error has occurred.") =>
		new(code, description, ErrorTypes.Validation);

	/// <summary>
	/// Creates an <see cref="Error"/> of type <see cref="ErrorTypes.Conflict"/> from a code and description.
	/// </summary>
	/// <param name="code">The unique error code.</param>
	/// <param name="description">The error description.</param>
	public static Error Conflict(
		string code = "Error.Conflict",
		string description = "A conflict error has occurred.") =>
		new(code, description, ErrorTypes.Conflict);

	/// <summary>
	/// Creates an <see cref="Error"/> of type <see cref="ErrorTypes.NotFound"/> from a code and description.
	/// </summary>
	/// <param name="code">The unique error code.</param>
	/// <param name="description">The error description.</param>
	public static Error NotFound(
		string code = "Error.NotFound",
		string description = "A 'Not Found' error has occurred.") =>
		new(code, description, ErrorTypes.NotFound);

	/// <summary>
	/// Creates an <see cref="Error"/> of type <see cref="ErrorTypes.NoContent"/> from a code and description.
	/// </summary>
	/// <param name="code">The unique error code.</param>
	/// <param name="description">The error description.</param>
	public static Error NoContent(
		string code = "Error.NoContent",
		string description = "A 'No Content' error has occurred.") =>
		new(code, description, ErrorTypes.NoContent);

	/// <summary>
	/// Creates an <see cref="Error"/> of type <see cref="ErrorTypes.Unauthorized"/> from a code and description.
	/// </summary>
	/// <param name="code">The unique error code.</param>
	/// <param name="description">The error description.</param>
	public static Error Unauthorized(
		string code = "Error.Unauthorized",
		string description = "An 'Authentication' error has occurred.") =>
		new(code, description, ErrorTypes.Unauthorized);

	/// <summary>
	/// Creates an <see cref="Error"/> of type <see cref="ErrorTypes.Composite"/> from a code and description.
	/// </summary>
	/// <param name="code">The unique error code.</param>
	/// <param name="description">The error description.</param>
	public static Error Composite(
		string code = "Error.Composite",
		string description = "A collection of errors has occurred.") =>
		new(code, description, ErrorTypes.Composite);

	/// <summary>
	/// Creates an <see cref="Error"/> with the given numeric <paramref name="type"/>,
	/// <paramref name="code"/>, and <paramref name="description"/>.
	/// </summary>
	/// <param name="type">The error type</param>
	/// <param name="code">The unique error code.</param>
	/// <param name="description">The error description.</param>
	public static Error Custom(
		ErrorTypes type,
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
		Custom((ErrorTypes)type, code, description);
}
