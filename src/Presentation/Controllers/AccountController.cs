﻿using System.ComponentModel.DataAnnotations;

using Application.Contracts.Requests.Finance;
using Application.Contracts.Responses.Finance;
using Application.Interfaces.Application;
using Application.Interfaces.Presentation.Services;

using Domain.Errors;
using Domain.Results;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Presentation.Attributes;
using Presentation.Common;
using Presentation.Controllers.Base;

using RegexPatterns = Domain.Constants.DomainConstants.RegexPatterns;
using Roles = Domain.Enumerators.RoleType;

namespace Presentation.Controllers;

/// <summary>
/// The bank account controller class.
/// </summary>
/// <remarks>
/// Inherits from <see cref="ApiControllerBase"/>.
/// </remarks>
[Authorize]
[Route(Endpoints.Account.BaseUri)]
[ApiVersion(Versioning.CurrentVersion)]
public sealed class AccountController : ApiControllerBase
{
	private readonly IAccountService _accountService;
	private readonly ICurrentUserService _currentUserService;

	/// <summary>
	/// Initializes an instance of the bank account controller class.
	/// </summary>
	/// <param name="accountService">The bank account service to use.</param>
	/// <param name="currentUserService">The current user service to use.</param>
	public AccountController(IAccountService accountService, ICurrentUserService currentUserService)
	{
		_accountService = accountService;
		_currentUserService = currentUserService;
	}

	/// <summary>
	/// Deletes an existing bank account by the given <paramref name="accountId"/>.
	/// </summary>
	/// <param name="accountId">The bank account identifier.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <response code="200">The deleted response.</response>
	/// <response code="401">No credentials or invalid credentials.</response>
	/// <response code="403">Not enough privileges to perform an action.</response>
	/// <response code="404">If the bank account to delete was not found.</response>
	/// <response code="500">If the something internal went wrong.</response>	
	[HttpDelete(Endpoints.Account.Delete), AuthorizeRoles(Roles.ADMINISTRATOR)]
	[ProducesResponseType(typeof(Deleted), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> Delete(Guid accountId, CancellationToken cancellationToken)
	{
		ErrorOr<Deleted> result =
			await _accountService.Delete(_currentUserService.UserId, accountId, cancellationToken);

		return Delete(result);
	}

	/// <summary>
	/// Gets a collection of bank accounts.
	/// </summary>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <response code="200">The successful response.</response>
	/// <response code="401">No credentials or invalid credentials.</response>
	/// <response code="403">Not enough privileges to perform an action.</response>
	/// <response code="404">If no bank account records were found.</response>
	/// <response code="500">If the something internal went wrong.</response>
	[HttpGet(Endpoints.Account.GetAll)]
	[ProducesResponseType(typeof(IEnumerable<AccountResponse>), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
	{
		ErrorOr<IEnumerable<AccountResponse>> result =
			await _accountService.Get(_currentUserService.UserId, false, cancellationToken);

		return Get(result);
	}

	/// <summary>
	/// Should get a account by the <paramref name="accountId"/> .
	/// </summary>
	/// <param name="accountId">The bank account identifier.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <response code="200">The successful response.</response>
	/// <response code="401">No credentials or invalid credentials.</response>
	/// <response code="403">Not enough privileges to perform an action.</response>
	/// <response code="404">If no record was found.</response>
	/// <response code="500">If the something internal went wrong.</response>
	[HttpGet(Endpoints.Account.GetById)]
	[ProducesResponseType(typeof(AccountResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> GetById(Guid accountId, CancellationToken cancellationToken)
	{
		ErrorOr<AccountResponse> result =
			await _accountService.Get(_currentUserService.UserId, accountId, false, cancellationToken);

		return Get(result);
	}

	/// <summary>
	/// Should get an account by the international bank account number.
	/// </summary>
	/// <param name="iban">The international bank account number.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <response code="200">The successful response.</response>
	/// <response code="401">No credentials or invalid credentials.</response>
	/// <response code="403">Not enough privileges to perform an action.</response>
	/// <response code="404">If no record was found.</response>
	/// <response code="500">If the something internal went wrong.</response>
	[HttpGet(Endpoints.Account.GetByNumber)]
	[ProducesResponseType(typeof(AccountResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> GetByNumber([RegularExpression(RegexPatterns.IBAN)] string iban, CancellationToken cancellationToken)
	{
		ErrorOr<AccountResponse> result =
			await _accountService.Get(_currentUserService.UserId, iban, false, cancellationToken);

		return Get(result);
	}

	/// <summary>
	/// Should create an bank account.
	/// </summary>
	/// <param name="createRequest">The bank account create request.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <response code="201">The created response.</response>
	/// <response code="401">No credentials or invalid credentials.</response>
	/// <response code="403">Not enough privileges to perform an action.</response>
	/// <response code="409">Conflicts occured during creation of the resource.</response>
	/// <response code="500">If the something internal went wrong.</response>
	[HttpPost(Endpoints.Account.Post)]
	[ProducesResponseType(typeof(Created), StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> Post(AccountCreateRequest createRequest, CancellationToken cancellationToken)
	{
		ErrorOr<Created> result =
			await _accountService.Create(_currentUserService.UserId, createRequest, cancellationToken);

		return PostWithoutLocation(result);
	}

	/// <summary>
	/// Updates an existing bank account.
	/// </summary>
	/// <param name="updateRequest">The bank account update request.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <response code="200">The updated response.</response>
	/// <response code="400">The update request is incorrect.</response>
	/// <response code="401">No credentials or invalid credentials.</response>
	/// <response code="403">Not enough privileges to perform an action.</response>
	/// <response code="500">If the something internal went wrong.</response>
	[HttpPut(Endpoints.Account.Put)]
	[ProducesResponseType(typeof(Updated), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> Put(AccountUpdateRequest updateRequest, CancellationToken cancellationToken)
	{
		ErrorOr<Updated> result =
			await _accountService.Update(_currentUserService.UserId, updateRequest, cancellationToken);

		return Put(result);
	}
}
