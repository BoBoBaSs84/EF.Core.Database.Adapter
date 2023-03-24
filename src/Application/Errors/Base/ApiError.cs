using Domain.Errors;
using System.Net;

namespace Application.Errors.Base;

/// <summary>
/// Error class for http responses
/// </summary>
public record class ApiError : Error
{
	/// <summary>
	/// Gets the http status code for the error
	/// </summary>
	public HttpStatusCode StatusCode { get; }

	/// <summary>
	/// Initializes an instance of <see cref="ApiError"/>
	/// </summary>
	/// <param name="original"></param>
	/// <param name="statusCode"></param>
	protected ApiError(Error original, HttpStatusCode statusCode) : base(original) =>
		StatusCode = statusCode;

	/// <summary>
	/// Creates an <see cref="ApiError"/> with a given <see cref="Error"/> and a <see cref="HttpStatusCode"/>,
	/// </summary>
	/// <param name="error">Error</param>
	/// <param name="statusCode">Status code</param>
	/// <returns>Api error</returns>
	public static ApiError Create(Error error, HttpStatusCode statusCode) =>
		new(error, statusCode);

	/// <summary>
	/// Creates an api error with the status <see cref="HttpStatusCode.Unauthorized"/>
	/// with the given code and description.
	/// </summary>
	/// <param name="code">The unique error code.</param>
	/// <param name="description">The error description.</param>
	/// <returns>Api error</returns>
	public static ApiError CreateUnauthorized(string code, string description) =>
		new(Unauthorized(code, description), HttpStatusCode.Unauthorized);

	/// <summary>
	/// Creates an api error with the status <see cref="HttpStatusCode.BadRequest"/>
	/// with the given code and description.
	/// </summary>
	/// <param name="code">The unique error code.</param>
	/// <param name="description">The error description.</param>
	/// <returns>Api error</returns>
	public static ApiError CreateBadRequest(string code, string description) =>
		new(Validation(code, description), HttpStatusCode.BadRequest);

	/// <summary>
	/// Creates an api error with the status <see cref="HttpStatusCode.NotFound"/>
	/// with the given code and description.
	/// </summary>
	/// <param name="code">The unique error code.</param>
	/// <param name="description">The error description.</param>
	/// <returns>Api error</returns>
	public static ApiError CreateNotFound(string code, string description) =>
		new(NotFound(code, description), HttpStatusCode.NotFound);

	/// <summary>
	/// Creates an api error with the status <see cref="HttpStatusCode.Conflict"/>
	/// with the given code and description.
	/// </summary>
	/// <param name="code">The unique error code.</param>
	/// <param name="description">The error description.</param>
	/// <returns>Api error</returns>
	public static ApiError CreateConflict(string code, string description) =>
		new(Conflict(code, description), HttpStatusCode.Conflict);

	/// <summary>
	/// Creates an api error with the status <see cref="HttpStatusCode.InternalServerError"/>
	/// with the given code and description.
	/// </summary>
	/// <param name="code">The unique error code.</param>
	/// <param name="description">The error description.</param>
	/// <returns>Api error</returns>
	public static ApiError CreateFailed(string code, string description) =>
		new(Failure(code, description), HttpStatusCode.InternalServerError);
}
