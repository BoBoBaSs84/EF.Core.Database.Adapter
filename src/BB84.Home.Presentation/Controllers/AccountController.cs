﻿using Asp.Versioning;

using BB84.Home.Application.Contracts.Requests.Finance;
using BB84.Home.Application.Contracts.Responses.Finance;
using BB84.Home.Application.Interfaces.Application.Services.Finance;
using BB84.Home.Domain.Enumerators;
using BB84.Home.Domain.Errors;
using BB84.Home.Domain.Results;
using BB84.Home.Presentation.Attributes;
using BB84.Home.Presentation.Common;
using BB84.Home.Presentation.Controllers.Base;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BB84.Home.Presentation.Controllers;

/// <summary>
/// The bank account controller class.
/// </summary>
/// <remarks>
/// Inherits from <see cref="ApiControllerBase"/>.
/// </remarks>
/// <param name="accountService">The bank account service to use.</param>
/// <param name="transactionService">The transaction service to use.</param>
[Authorize]
[Route(Endpoints.Account.BaseUri)]
[ApiVersion(Versioning.CurrentVersion)]
public sealed partial class AccountController(IAccountService accountService, ITransactionService transactionService) : ApiControllerBase
{
	private readonly IAccountService _accountService = accountService;
	private readonly ITransactionService _transactionService = transactionService;

	/// <summary>
	/// Deletes an existing bank account for the application user by the bank account identifier.
	/// </summary>
	/// <param name="id">The identifier of the bank account.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <response code="200">The resource was successfully deleted.</response>
	/// <response code="401">No credentials or invalid credentials were supplied.</response>
	/// <response code="403">Insufficient permissions to access the resource or action.</response>
	/// <response code="404">The requested resource could not be found.</response>
	/// <response code="500">Something internal went terribly wrong.</response>
	[HttpDelete(Endpoints.Account.DeleteById), AuthorizeRoles(RoleType.Administrator)]
	[ProducesResponseType(typeof(Deleted), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> DeleteById(Guid id, CancellationToken token)
	{
		ErrorOr<Deleted> response = await _accountService
			.DeleteAsync(id, token)
			.ConfigureAwait(false);

		return Delete(response);
	}

	/// <summary>
	/// Returns a collection of bank accounts for for the application user.
	/// </summary>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <response code="200">If the response was successfully returned.</response>
	/// <response code="401">No credentials or invalid credentials were supplied.</response>
	/// <response code="500">Something internal went terribly wrong.</response>
	[HttpGet(Endpoints.Account.GetByUserId)]
	[ProducesResponseType(typeof(IEnumerable<AccountResponse>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> GetByUserId(CancellationToken token)
	{
		ErrorOr<IEnumerable<AccountResponse>> response = await _accountService
			.GetAllAsync(token)
			.ConfigureAwait(false);

		return Get(response);
	}

	/// <summary>
	/// Returns a bank account by the bank account identifier.
	/// </summary>
	/// <param name="id">The identifier of the bank account.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <response code="200">If the response was successfully returned.</response>
	/// <response code="401">No credentials or invalid credentials were supplied.</response>
	/// <response code="404">The requested resource could not be found.</response>
	/// <response code="500">Something internal went terribly wrong.</response>
	[HttpGet(Endpoints.Account.GetById)]
	[ProducesResponseType(typeof(AccountResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> GetById(Guid id, CancellationToken token)
	{
		ErrorOr<AccountResponse> response = await _accountService
			.GetByIdAsync(id, token)
			.ConfigureAwait(false);

		return Get(response);
	}

	/// <summary>
	/// Creates a new bank account for the application user.
	/// </summary>
	/// <param name="request">The account create request.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <response code="201">The resource was successfully created.</response>
	/// <response code="400">The provided request contained errors.</response>
	/// <response code="401">No credentials or invalid credentials were supplied.</response>
	/// <response code="409">The request conflicts with the target resource.</response>
	/// <response code="500">Something internal went terribly wrong.</response>
	[HttpPost(Endpoints.Account.Post)]
	[ProducesResponseType(typeof(Created), StatusCodes.Status201Created)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> Post(AccountCreateRequest request, CancellationToken token)
	{
		ErrorOr<Created> response = await _accountService
			.CreateAsync(request, token)
			.ConfigureAwait(false);

		return PostWithoutLocation(response);
	}

	/// <summary>
	/// Updates an existing bank account by the bank account identifier.
	/// </summary>
	/// <param name="id">The identifier of the bank account.</param>
	/// <param name="request">The account update request.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <response code="200">The resource was successfully updated.</response>
	/// <response code="400">The provided request contained errors.</response>
	/// <response code="401">No credentials or invalid credentials were supplied.</response>
	/// <response code="404">The requested resource could not be found.</response>
	/// <response code="500">Something internal went terribly wrong.</response>
	[HttpPut(Endpoints.Account.Put)]
	[ProducesResponseType(typeof(Updated), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> Put(Guid id, AccountUpdateRequest request, CancellationToken token)
	{
		ErrorOr<Updated> response = await _accountService
			.UpdateAsync(id, request, token)
			.ConfigureAwait(false);

		return Put(response);
	}
}
