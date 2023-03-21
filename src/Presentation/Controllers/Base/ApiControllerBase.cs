using Application.Errors.Base;
using Domain.Enumerators;
using Domain.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Presentation.Constants;
using Presentation.Extensions;

namespace Presentation.Controllers.Base;

/// <summary>
/// Base api controller
/// </summary>
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
		if (errors?.Any() != true)
			throw new InvalidOperationException("Should not call Problem(errors) without errors");

		if (errors.All(error => error.Type == ErrorTypes.Validation))
			return ValidationProblem(errors);

		if (errors.Count > 1)
			return MultiProblem(errors);

		return SingleProblem(errors[0]);
	}

	#region HttpMethods

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

	private CreatedAtActionResult CreatePostResult(string actionName, string controllerName,
		object? routeValues, object? createdObject)
	{
		int controllerIndex = controllerName.LastIndexOf("Controller", StringComparison.Ordinal);

		if (controllerIndex > 0)
			controllerName = controllerName.Remove(controllerIndex);

		return CreatedAtAction(actionName, controllerName, routeValues, createdObject);
	}

	#endregion HttpMethods

	private IActionResult ValidationProblem(IList<Error> errors)
	{
		ModelStateDictionary modelStateDictionary = new();

		foreach (Error error in errors)
			modelStateDictionary.AddModelError(error.Code, error.Description);

		return ValidationProblem(modelStateDictionary.GetErrors(), modelStateDictionary: modelStateDictionary);
	}

	private static IActionResult MultiProblem(IList<Error> errors)
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

	private IActionResult SingleProblem(Error error)
	{
		if (error is ApiError apiError)
			return Problem(
				statusCode: (int)apiError.StatusCode,
				title: error.Code,
				detail: error.Description);

		int statusCode = error.Type switch
		{
			ErrorTypes.NotFound => StatusCodes.Status404NotFound,
			ErrorTypes.Conflict => StatusCodes.Status409Conflict,
			ErrorTypes.Validation => StatusCodes.Status400BadRequest,
			ErrorTypes.NoContent => StatusCodes.Status204NoContent,
			_ => StatusCodes.Status500InternalServerError
		};

		return Problem(statusCode: statusCode, title: error.Code);
	}
}
