using System.ComponentModel.DataAnnotations;

using Application.Contracts.Requests.Finance;
using Application.Contracts.Responses.Finance;
using Application.Interfaces.Application.Finance;
using Application.Interfaces.Presentation.Services;

using Asp.Versioning;

using Domain.Enumerators;
using Domain.Errors;
using Domain.Results;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Presentation.Attributes;
using Presentation.Common;
using Presentation.Controllers.Base;

using RegexPatterns = Domain.Constants.DomainConstants.RegexPatterns;

namespace Presentation.Controllers;

/// <summary>
/// The bank account controller class.
/// </summary>
/// <remarks>
/// Inherits from <see cref="ApiControllerBase"/>.
/// </remarks>
/// <param name="accountService">The bank account service to use.</param>
/// <param name="currentUserService">The current user service to use.</param>
/// <param name="transactionService">The transaction service to use.</param>
[Authorize]
[Route(Endpoints.Account.BaseUri)]
[ApiVersion(Versioning.CurrentVersion)]
public sealed partial class AccountController(IAccountService accountService, ICurrentUserService currentUserService, ITransactionService transactionService) : ApiControllerBase
{
	private readonly IAccountService _accountService = accountService;
	private readonly ICurrentUserService _currentUserService = currentUserService;
	private readonly ITransactionService _transactionService = transactionService;

	/// <summary>
	/// Deletes an existing bank account for the application user by the bank account identifier.
	/// </summary>
	/// <param name="accountId">The identifier of the bank account.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <response code="200">The deleted response.</response>
	/// <response code="401">No credentials or invalid credentials.</response>
	/// <response code="403">Not enough privileges to perform an action.</response>
	/// <response code="404">If the bank account to delete was not found.</response>
	/// <response code="500">If the something internal went wrong.</response>	
	[HttpDelete(Endpoints.Account.Delete), AuthorizeRoles(RoleType.ADMINISTRATOR)]
	[ProducesResponseType(typeof(Deleted), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> Delete(Guid accountId, CancellationToken token)
	{
		ErrorOr<Deleted> response = await _accountService
			.Delete(accountId, token)
			.ConfigureAwait(false);

		return Delete(response);
	}

	/// <summary>
	/// Returns a collection of bank accounts for for the application user.
	/// </summary>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <response code="200">The successful response.</response>
	/// <response code="401">No credentials or invalid credentials.</response>
	/// <response code="404">If no bank account records were found.</response>
	/// <response code="500">If the something internal went wrong.</response>
	[HttpGet(Endpoints.Account.GetByUserId)]
	[ProducesResponseType(typeof(IEnumerable<AccountResponse>), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> GetByUserId(CancellationToken token)
	{
		ErrorOr<IEnumerable<AccountResponse>> response = await _accountService
			.GetByUserId(_currentUserService.UserId, token)
			.ConfigureAwait(false);

		return Get(response);
	}

	/// <summary>
	/// Returns a bank account for the application user by the bank account identifier.
	/// </summary>
	/// <param name="accountId">The identifier of the bank account.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <response code="200">The successful response.</response>
	/// <response code="401">No credentials or invalid credentials.</response>
	/// <response code="404">If no record was found.</response>
	/// <response code="500">If the something internal went wrong.</response>
	[HttpGet(Endpoints.Account.GetByAccountId)]
	[ProducesResponseType(typeof(AccountResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> GetByAccountId(Guid accountId, CancellationToken token)
	{
		ErrorOr<AccountResponse> response = await _accountService
			.GetByAccountId(accountId, token)
			.ConfigureAwait(false);

		return Get(response);
	}

	/// <summary>
	/// Returns a bank account for the application user by the international bank account number.
	/// </summary>
	/// <param name="iban">The international bank account number.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <response code="200">The successful response.</response>
	/// <response code="401">No credentials or invalid credentials.</response>
	/// <response code="404">If no record was found.</response>
	/// <response code="500">If the something internal went wrong.</response>
	[HttpGet(Endpoints.Account.GetByNumber)]
	[ProducesResponseType(typeof(AccountResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> GetByNumber([RegularExpression(RegexPatterns.IBAN)] string iban, CancellationToken token)
	{
		ErrorOr<AccountResponse> response = await _accountService
			.GetByNumber(_currentUserService.UserId, iban, token)
			.ConfigureAwait(false);

		return Get(response);
	}

	/// <summary>
	/// Creates a new bank account for the application user.
	/// </summary>
	/// <param name="request">The bank account create request.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <response code="201">The created response.</response>
	/// <response code="401">No credentials or invalid credentials.</response>
	/// <response code="409">Conflicts occured during creation of the resource.</response>
	/// <response code="500">If the something internal went wrong.</response>
	[HttpPost(Endpoints.Account.Post)]
	[ProducesResponseType(typeof(Created), StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> Post(AccountCreateRequest request, CancellationToken token)
	{
		ErrorOr<Created> response =
			await _accountService.Create(_currentUserService.UserId, request, token);
		return PostWithoutLocation(response);
	}

	/// <summary>
	/// Updates an existing bank account for the application user.
	/// </summary>
	/// <param name="request">The bank account update request.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <response code="200">The updated response.</response>
	/// <response code="400">The update request is incorrect.</response>
	/// <response code="401">No credentials or invalid credentials.</response>
	/// <response code="500">If the something internal went wrong.</response>
	[HttpPut(Endpoints.Account.Put)]
	[ProducesResponseType(typeof(Updated), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> Put(AccountUpdateRequest request, CancellationToken token)
	{
		ErrorOr<Updated> response =
			await _accountService.Update(_currentUserService.UserId, request, token);
		return Put(response);
	}
}
