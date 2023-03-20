using System.Net;
using Domain.Errors;

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
	protected ApiError(Error original, HttpStatusCode statusCode) : base(original)
	{
		StatusCode = statusCode;
	}

	/// <summary>
	/// Creates an <see cref="ApiError"/> with a given <see cref="Error"/> and a <see cref="HttpStatusCode"/>,
	/// </summary>
	/// <param name="error">Error</param>
	/// <param name="statusCode">Status code</param>
	/// <returns>Api error</returns>
	public static ApiError Create(Error error, HttpStatusCode statusCode) =>
		new(error, statusCode);

	/// <summary>
	/// Creates a Authentication <see cref="ApiError"/> with a given code and description
	/// </summary>
	/// <param name="code">The unique error code.</param>
	/// <param name="description">The error description.</param>
	/// <returns>Api error</returns>
	public static ApiError CreateUnauthorized(string code, string description) =>
		new(Authentication(code, description), HttpStatusCode.Unauthorized);

	/// <summary>
	/// Creates a Validation <see cref="ApiError"/> with a given code and description
	/// </summary>
	/// <param name="code">The unique error code.</param>
	/// <param name="description">The error description.</param>
	/// <returns>Api error</returns>
	public static ApiError CreateBadRequest(string code, string description) =>
		new(Validation(code, description), HttpStatusCode.BadRequest);

	/// <summary>
	/// Creates a NotFound <see cref="ApiError"/> with a given code and description
	/// </summary>
	/// <param name="code">The unique error code.</param>
	/// <param name="description">The error description.</param>
	/// <returns>Api error</returns>
	public static ApiError CreateNotFound(string code, string description) =>
		new(NotFound(code, description), HttpStatusCode.NotFound);

	/// <summary>
	/// Creates a Conflict <see cref="ApiError"/> with a given code and description
	/// </summary>
	/// <param name="code">The unique error code.</param>
	/// <param name="description">The error description.</param>
	/// <returns>Api error</returns>
	public static ApiError CreateConflict(string code, string description) =>
		new(Conflict(code, description), HttpStatusCode.Conflict);

	/// <summary>
	/// Creates a Failure <see cref="ApiError"/> with a given code and description
	/// </summary>
	/// <param name="code">The unique error code.</param>
	/// <param name="description">The error description.</param>
	/// <returns>Api error</returns>
	public static ApiError CreateFailed(string code, string description) =>
		new(Failure(code, description), HttpStatusCode.InternalServerError);
}
