﻿using Application.Contracts.Responses;
using Application.Features.Requests;
using Application.Features.Responses;
using Application.Interfaces.Application;
using Domain.Errors;
using Domain.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Common;
using Presentation.Controllers.Base;
using HHC = Presentation.Constants.WebConstants.HttpHeaders;

namespace Presentation.Controllers;

/// <summary>
/// The <see cref="DayTypeController"/> class.
/// </summary>
/// <remarks>
/// Inherits from <see cref="ApiControllerBase"/>.
/// </remarks>
[Route(Endpoints.DayType.BaseUri)]
[ApiVersion(Versioning.CurrentVersion)]
public sealed class DayTypeController : ApiControllerBase
{
	private readonly IDayTypeService _dayTypeService;

	/// <summary>
	/// Initializes an instance of <see cref="DayTypeController"/> class.
	/// </summary>
	/// <param name="dayTypeService">The day type service.</param>
	public DayTypeController(IDayTypeService dayTypeService) =>
		_dayTypeService = dayTypeService;

	/// <summary>
	/// Should return the day type entities as a paged list, filtered by the parameters.
	/// </summary>
	/// <param name="parameters">The day type query parameters.</param>
	/// <response code="200">If the result is returned.</response>
	/// <response code="404">If the result is empty.</response>
	/// <response code="500">If something went wrong.</response>
	[HttpGet(Endpoints.DayType.GetPagedByParameters)]
	[ProducesResponseType(typeof(IPagedList<DayTypeResponse>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> GetPagedByParameters([FromQuery] DayTypeParameters parameters)
	{
		ErrorOr<IPagedList<DayTypeResponse>> result = await _dayTypeService.GetPagedByParameters(parameters);

		if (!result.IsError)
		{
			string jsonMetadata = result.Value.MetaData.ToJsonString();
			Response.Headers.Add(HHC.Pagination, jsonMetadata);
		}

		return Get(result);
	}

	/// <summary>
	/// Should return the day type by its identifier.
	/// </summary>
	/// <param name="id">The identifier of the day type.</param>
	/// <response code="200">If the result is returned.</response>
	/// <response code="404">If the result is empty.</response>
	/// <response code="500">If something went wrong.</response>
	[HttpGet(Endpoints.DayType.GetById)]
	[ProducesResponseType(typeof(IPagedList<DayTypeResponse>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> GetById(int id)
	{
		ErrorOr<DayTypeResponse> result = await _dayTypeService.GetById(id);
		return Get(result);
	}

	/// <summary>
	/// Should return the day type by its name.
	/// </summary>
	/// <param name="name">The name of the day type.</param>
	/// <response code="200">If the result is returned.</response>
	/// <response code="404">If the result is empty.</response>
	/// <response code="500">If something went wrong.</response>
	[HttpGet(Endpoints.DayType.GetByName)]
	[ProducesResponseType(typeof(IPagedList<DayTypeResponse>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> GetByName(string name)
	{
		ErrorOr<DayTypeResponse> result = await _dayTypeService.GetByName(name);
		return Get(result);	
	}
}
