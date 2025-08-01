﻿using Asp.Versioning;

using BB84.Home.Application.Contracts.Requests.Documents;
using BB84.Home.Application.Contracts.Responses.Documents;
using BB84.Home.Application.Features.Requests;
using BB84.Home.Application.Features.Responses;
using BB84.Home.Application.Interfaces.Application.Services.Documents;
using BB84.Home.Domain.Errors;
using BB84.Home.Domain.Results;
using BB84.Home.Presentation.Common;
using BB84.Home.Presentation.Controllers.Base;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BB84.Home.Presentation.Controllers;

/// <summary>
/// Provides functionality for managing document records, including creation, retrieval, updating, and deletion.
/// </summary>
/// <param name="documentService">The document service instance to use.</param>
[Authorize]
[Route(Endpoints.Document.BaseUri)]
[ApiVersion(Versioning.CurrentVersion)]
public sealed class DocumentController(IDocumentService documentService) : ApiControllerBase
{
	/// <summary>
	/// Deletes an existing document by the provided <paramref name="id"/>.
	/// </summary>
	/// <param name="id">The document identifier to use.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <response code="200">The resource was successfully deleted.</response>
	/// <response code="401">No credentials or invalid credentials were supplied.</response>
	/// <response code="403">Insufficient permissions to access the resource or action.</response>
	/// <response code="404">The requested resource could not be found.</response>
	/// <response code="500">Something internal went terribly wrong.</response>
	[HttpDelete(Endpoints.Document.DeleteById)]
	[ProducesResponseType(typeof(Deleted), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> DeleteById(Guid id, CancellationToken token)
	{
		ErrorOr<Deleted> result = await documentService
			.DeleteAsync(id, token)
			.ConfigureAwait(false);

		return Delete(result);
	}

	/// <summary>
	/// Deletes an existing document collection by the provided <paramref name="ids"/>.
	/// </summary>
	/// <param name="ids">The document identifiers to use.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <response code="200">The resource was successfully deleted.</response>
	/// <response code="401">No credentials or invalid credentials were supplied.</response>
	/// <response code="403">Insufficient permissions to access the resource or action.</response>
	/// <response code="404">The requested resource could not be found.</response>
	/// <response code="500">Something internal went terribly wrong.</response>
	[HttpDelete(Endpoints.Document.DeleteByIds)]
	[ProducesResponseType(typeof(Deleted), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> DeleteByIds([FromBody] IEnumerable<Guid> ids, CancellationToken token)
	{
		ErrorOr<Deleted> result = await documentService
			.DeleteAsync(ids, token)
			.ConfigureAwait(false);

		return Delete(result);
	}

	/// <summary>
	/// Returns an existing document by the provided <paramref name="id"/>.
	/// </summary>
	/// <param name="id">The document identifier to use.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <response code="200">If the response was successfully returned.</response>
	/// <response code="401">No credentials or invalid credentials were supplied.</response>
	/// <response code="403">Insufficient permissions to access the resource or action.</response>
	/// <response code="404">The requested resource could not be found.</response>
	/// <response code="500">Something internal went terribly wrong.</response>	
	[HttpGet(Endpoints.Document.GetById)]
	[ProducesResponseType(typeof(DocumentResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> GetById(Guid id, CancellationToken token)
	{
		ErrorOr<DocumentResponse> result = await documentService
			.GetByIdAsync(id, token)
			.ConfigureAwait(false);

		return Get(result);
	}

	/// <summary>
	/// Returns a document collection as a paged list filtered by the document <paramref name="parameters"/>.
	/// </summary>
	/// <param name="parameters">The document query parameters to use.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <response code="200">If the response was successfully returned.</response>
	/// <response code="401">No credentials or invalid credentials were supplied.</response>
	/// <response code="403">Insufficient permissions to access the resource or action.</response>
	/// <response code="500">Something internal went terribly wrong.</response>	
	[HttpGet(Endpoints.Document.GetPagedByParameters)]
	[ProducesResponseType(typeof(IPagedList<DocumentResponse>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> GetPagedByParameters([FromQuery] DocumentParameters parameters, CancellationToken token)
	{
		ErrorOr<IPagedList<DocumentResponse>> result = await documentService
			.GetPagedByParametersAsync(parameters, token)
			.ConfigureAwait(false);

		return Get(result, result.Value.MetaData);
	}

	/// <summary>
	/// Creates a new document with the provided create <paramref name="request"/>.
	/// </summary>
	/// <param name="request">The document create request to use.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <response code="201">The resource was successfully created.</response>
	/// <response code="400">The provided request contained errors.</response>
	/// <response code="401">No credentials or invalid credentials were supplied.</response>
	/// <response code="403">Insufficient permissions to access the resource or action.</response>
	/// <response code="500">Something internal went terribly wrong.</response>
	[HttpPost(Endpoints.Document.Post)]
	[ProducesResponseType(typeof(Created), StatusCodes.Status201Created)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> Post([FromBody] DocumentCreateRequest request, CancellationToken token)
	{
		ErrorOr<Created> result = await documentService
			.CreateAsync(request, token)
			.ConfigureAwait(false);

		return PostWithoutLocation(result);
	}

	/// <summary>
	/// Creates a new document collection with the provided create <paramref name="requests"/>.
	/// </summary>
	/// <param name="requests">The document create requests to use.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <response code="201">The resource was successfully created.</response>
	/// <response code="400">The provided request contained errors.</response>
	/// <response code="401">No credentials or invalid credentials were supplied.</response>
	/// <response code="403">Insufficient permissions to access the resource or action.</response>
	/// <response code="500">Something internal went terribly wrong.</response>
	[HttpPost(Endpoints.Document.PostMultiple)]
	[ProducesResponseType(typeof(Created), StatusCodes.Status201Created)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> PostMultiple([FromBody] IEnumerable<DocumentCreateRequest> requests, CancellationToken token)
	{
		ErrorOr<Created> result = await documentService
			.CreateAsync(requests, token)
			.ConfigureAwait(false);

		return PostWithoutLocation(result);
	}

	/// <summary>
	/// Updates an existing document with the provided create <paramref name="request"/>.
	/// </summary>
	/// <param name="request">The document update request to use.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <response code="200">The resource was successfully updated.</response>
	/// <response code="400">The provided request contained errors.</response>
	/// <response code="401">No credentials or invalid credentials were supplied.</response>
	/// <response code="403">Insufficient permissions to access the resource or action.</response>
	/// <response code="404">The requested resource could not be found.</response>
	/// <response code="500">Something internal went terribly wrong.</response>
	[HttpPut(Endpoints.Document.Put)]
	[ProducesResponseType(typeof(Updated), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> Put([FromBody] DocumentUpdateRequest request, CancellationToken token)
	{
		ErrorOr<Updated> result = await documentService
			.UpdateAsync(request, token)
			.ConfigureAwait(false);

		return Put(result);
	}

	/// <summary>
	/// Updates an existing document collection with the provided create <paramref name="requests"/>.
	/// </summary>
	/// <param name="requests">The document update requests to use.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <response code="200">The resource was successfully updated.</response>
	/// <response code="400">The provided request contained errors.</response>
	/// <response code="401">No credentials or invalid credentials were supplied.</response>
	/// <response code="403">Insufficient permissions to access the resource or action.</response>
	/// <response code="404">The requested resource could not be found.</response>
	/// <response code="500">Something internal went terribly wrong.</response>
	[HttpPut(Endpoints.Document.PutMultiple)]
	[ProducesResponseType(typeof(Updated), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> PutMultiple([FromBody] IEnumerable<DocumentUpdateRequest> requests, CancellationToken token)
	{
		ErrorOr<Updated> result = await documentService
			.UpdateAsync(requests, token)
			.ConfigureAwait(false);

		return Put(result);
	}
}
