using BB84.Extensions.Serialization;
using BB84.Home.Application.Errors.Base;
using BB84.Home.Application.Features.Responses;
using BB84.Home.Domain.Enumerators;
using BB84.Home.Domain.Errors;
using BB84.Home.Presentation.Common;
using BB84.Home.Presentation.Extensions;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

using HttpHeaders = BB84.Home.Presentation.Common.PresentationConstants.HttpHeaders;

namespace BB84.Home.Presentation.Controllers.Base;


/// <summary>
/// Provides a base class for API controllers, offering common functionality for handling API responses, including
/// standardized methods for returning results for various HTTP actions (GET, POST, PUT, DELETE), handling errors, and
/// generating problem details.
/// </summary>
/// <remarks>
/// This class is designed to simplify the implementation of API controllers by providing reusable methods for
/// common response patterns. It includes methods for handling success and error cases, generating appropriate
/// HTTP responses, and managing metadata for paginated results.  Derived controllers can use these methods
/// to ensure consistent behavior and response formatting across the API.
/// </remarks>
[ApiController]
public abstract class ApiControllerBase : ControllerBase
{
	/// <summary>
	/// Returns a <see cref="ProblemDetails.ProblemDetails"/> result from a list of <see cref="Error"/>
	/// </summary>
	/// <param name="errors">List of errors</param>
	/// <returns>Problem object result</returns>
	protected IActionResult Problem(IList<Error> errors)
	{
		return errors?.Any() != true
			? throw new InvalidOperationException("Should not call Problem(errors) without errors")
			: errors.All(error => error.Type == ErrorType.Validation)
			? ValidationProblem(errors)
			: errors.Count > 1 ? MultiProblem(errors) : SingleProblem(errors[0]);
	}

	/// <summary>
	/// Returns a <see cref="FileContentResult"/> response
	/// </summary>
	/// <param name="byteStream">Byte stream</param>
	/// <param name="file">file name with extension</param>
	/// <returns>FileContentResult</returns>
	protected FileContentResult ToFileResponse(byte[] byteStream, string file) =>
		File(byteStream, "content/type", file);

	/// <summary>
	/// Returns the api result for a get action
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="result">ErrorOr{T}</param>
	/// <returns>IActionResult</returns>
	protected IActionResult Get<T>(ErrorOr<T> result) =>
		result.Match(success => Ok(result.Value), Problem);

	/// <summary>
	/// Returns the api result for a get action
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="result">ErrorOr{T}</param>
	/// <returns>IActionResult</returns>
	protected IActionResult Get<T>(T result) => Ok(result);

	/// <summary>
	/// Returns the api result for a get action for a paged list.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="result">ErrorOr{T}</param>
	/// <param name="metaData">The meta data of th e paged list.</param>
	/// <returns>IActionResult</returns>
	protected IActionResult Get<T>(ErrorOr<T> result, MetaData? metaData)
	{
		if (!result.IsError && metaData is not null)
			Response.Headers.Append(HttpHeaders.Pagination, metaData.ToJson());

		return Get(result);
	}

	/// <summary>
	/// Returns the api result for a put action
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="result">ErrorOr{T}</param>
	/// <returns>IActionResult</returns>
	protected IActionResult Put<T>(ErrorOr<T> result) =>
		result.Match(success => Ok(result.Value), Problem);

	/// <summary>
	/// Returns the api result for a post action
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="result">>ErrorOr{T}</param>
	/// <param name="actionName">Name of the action to retrieve the new resource</param>
	/// <param name="controllerName">Name of the controller to retrieve the new resource</param>
	/// <param name="routeValues">Route values for the get if needed</param>
	/// <returns>IActionResult</returns>
	protected IActionResult Post<T>(ErrorOr<T> result, string actionName, string controllerName, object? routeValues) =>
		result.Match(success => CreatePostResult(actionName, controllerName, routeValues, result.Value), Problem);

	/// <summary>
	/// Returns the api result for a post action (without location in the headers)
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="result">>ErrorOr{T}</param>
	/// <returns>IActionResult</returns>
	protected IActionResult PostWithoutLocation<T>(ErrorOr<T> result) =>
		result.Match(success => new ObjectResult(result.Value) { StatusCode = StatusCodes.Status201Created }, Problem);

	/// <summary>
	/// Returns the api result for a delete action
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="result">>ErrorOr{T}</param>
	/// <returns>IActionResult</returns>
	protected IActionResult Delete<T>(ErrorOr<T> result) =>
		result.Match(success => Ok(result.Value), Problem);

	/// <summary>
	/// Creates a <see cref="CreatedAtActionResult"/> that specifies the location of a newly created resource.
	/// </summary>
	/// <param name="actionName">The name of the action to use in the generated URL.</param>
	/// <param name="controllerName">
	/// The name of the controller to use in the generated URL. The "Controller" suffix is automatically removed if present.
	/// </param>
	/// <param name="routeValues">
	/// An object containing the route values to include in the generated URL. Can be <see langword="null"/>.
	/// </param>
	/// <param name="createdObject">
	/// The object representing the newly created resource. Can be <see langword="null"/>.
	/// </param>
	/// <returns>
	/// A <see cref="CreatedAtActionResult"/> that specifies the action, controller, route values, and the created resource.
	/// </returns>
	private CreatedAtActionResult CreatePostResult(string actionName, string controllerName, object? routeValues, object? createdObject)
	{
		int controllerIndex = controllerName.LastIndexOf("Controller", StringComparison.Ordinal);

		if (controllerIndex > 0)
			controllerName = controllerName.Remove(controllerIndex);

		return CreatedAtAction(actionName, controllerName, routeValues, createdObject);
	}

	/// <summary>
	/// Creates a <see cref="ActionResult"/> based on the provided list of validation errors.
	/// </summary>
	/// <remarks>
	/// This method populates a <see cref="ModelStateDictionary"/> with the provided errors and generates
	/// a validation problem response. The response conforms to the standard problem details format for HTTP
	/// validation errors.
	/// </remarks>
	/// <param name="errors">A collection of <see cref="Error"/> objects representing validation errors.
	/// Each error must have a non-null <see cref="Error.Code"/> and <see cref="Error.Description"/>.
	/// </param>
	/// <returns>
	/// An <see cref="ActionResult"/> representing a validation problem response, including the error
	/// details in the response body.
	/// </returns>
	private ActionResult ValidationProblem(IList<Error> errors)
	{
		ModelStateDictionary modelStateDictionary = new();

		foreach (Error error in errors)
			modelStateDictionary.AddModelError(error.Code, error.Description);

		return ValidationProblem(modelStateDictionary.GetErrors(), modelStateDictionary: modelStateDictionary);
	}

	/// <summary>
	/// Creates an <see cref="ObjectResult"/> representing a composite error response with detailed problem information.
	/// </summary>
	/// <param name="errors">
	/// A collection of <see cref="Error"/> objects that provide additional details about the errors encountered.
	/// </param>
	/// <returns>
	/// An <see cref="ObjectResult"/> containing a <see cref="ProblemDetails"/> object with information
	/// about the composite error, including its type, status, title, and description.
	/// </returns>
	private static ObjectResult MultiProblem(IList<Error> errors)
	{
		ApiError error = ApiErrors.CompositeError;

		ProblemDetails problemDetails = new()
		{
			Type = PresentationConstants.ProblemDetailsTypes.Error500Type,
			Status = (int)error.StatusCode,
			Title = error.Code,
			Detail = error.Description,
			Extensions = { { nameof(errors), errors } }
		};

		return new ObjectResult(problemDetails);
	}

	/// <summary>
	/// Creates an <see cref="ObjectResult"/> representing a standardized HTTP response for the specified error.
	/// </summary>
	/// <remarks>
	/// If the <paramref name="error"/> is of type <see cref="ApiError"/>, the response will include additional
	/// details such as the error title and description. For other error types, the status code is determined 
	/// based on the <see cref="ErrorType"/>.
	/// </remarks>
	/// <param name="error">
	/// The <see cref="Error"/> object containing details about the error to be represented in the response.
	/// </param>
	/// <returns>
	/// An <see cref="ObjectResult"/> that represents the HTTP response for the given error.
	/// The response includes an appropriate HTTP status code and optional error details.
	/// </returns>
	private ObjectResult SingleProblem(Error error)
	{
		if (error is ApiError apiError)
			return Problem(
				statusCode: (int)apiError.StatusCode,
				title: error.Code,
				detail: error.Description);

		int statusCode = error.Type switch
		{
			ErrorType.NotFound => StatusCodes.Status404NotFound,
			ErrorType.Conflict => StatusCodes.Status409Conflict,
			ErrorType.Validation => StatusCodes.Status400BadRequest,
			ErrorType.NoContent => StatusCodes.Status204NoContent,
			_ => StatusCodes.Status500InternalServerError
		};

		return Problem(statusCode: statusCode, title: error.Code);
	}
}
