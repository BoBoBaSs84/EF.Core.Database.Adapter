using System.ComponentModel.DataAnnotations;

using Application.Contracts.Requests.Finance;
using Application.Contracts.Responses.Finance;
using Application.Interfaces.Application;
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
[Authorize]
[Route(Endpoints.Account.BaseUri)]
[ApiVersion(Versioning.CurrentVersion)]
public sealed partial class AccountController : ApiControllerBase
{
	private readonly IAccountService _accountService;
	private readonly ICurrentUserService _currentUserService;
	private readonly ITransactionService _transactionService;

	/// <summary>
	/// Initializes an instance of the bank account controller class.
	/// </summary>
	/// <param name="accountService">The bank account service to use.</param>
	/// <param name="currentUserService">The current user service to use.</param>
	/// <param name="transactionService">The transaction service to use.</param>
	public AccountController(IAccountService accountService, ICurrentUserService currentUserService, ITransactionService transactionService)
	{
		_accountService = accountService;
		_currentUserService = currentUserService;
		_transactionService = transactionService;
	}

	/// <summary>
	/// Deletes an existing bank account for the application user by the bank account identifier.
	/// </summary>
	/// <param name="accountId">The identifier of the bank account.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <response code="200">The deleted response.</response>
	/// <response code="401">No credentials or invalid credentials.</response>
	/// <response code="403">Not enough privileges to perform an action.</response>
	/// <response code="404">If the bank account to delete was not found.</response>
	/// <response code="500">If the something internal went wrong.</response>	
	[HttpDelete(Endpoints.Account.Delete), AuthorizeRoles(RoleType.ADMINISTRATOR)]
	[ProducesResponseType(typeof(Deleted), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> Delete(Guid accountId, CancellationToken cancellationToken)
	{
		ErrorOr<Deleted> response =
			await _accountService.Delete(_currentUserService.UserId, accountId, cancellationToken);
		return Delete(response);
	}

	/// <summary>
	/// Returns a collection of bank accounts for for the application user.
	/// </summary>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <response code="200">The successful response.</response>
	/// <response code="401">No credentials or invalid credentials.</response>
	/// <response code="404">If no bank account records were found.</response>
	/// <response code="500">If the something internal went wrong.</response>
	[HttpGet(Endpoints.Account.GetAll)]
	[ProducesResponseType(typeof(IEnumerable<AccountResponse>), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
	{
		ErrorOr<IEnumerable<AccountResponse>> response =
			await _accountService.Get(_currentUserService.UserId, false, cancellationToken);
		return Get(response);
	}

	/// <summary>
	/// Returns a bank account for the application user by the bank account identifier.
	/// </summary>
	/// <param name="accountId">The identifier of the bank account.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <response code="200">The successful response.</response>
	/// <response code="401">No credentials or invalid credentials.</response>
	/// <response code="404">If no record was found.</response>
	/// <response code="500">If the something internal went wrong.</response>
	[HttpGet(Endpoints.Account.GetById)]
	[ProducesResponseType(typeof(AccountResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> GetById(Guid accountId, CancellationToken cancellationToken)
	{
		ErrorOr<AccountResponse> response =
			await _accountService.Get(_currentUserService.UserId, accountId, false, cancellationToken);
		return Get(response);
	}

	/// <summary>
	/// Returns a bank account for the application user by the international bank account number.
	/// </summary>
	/// <param name="iban">The international bank account number.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <response code="200">The successful response.</response>
	/// <response code="401">No credentials or invalid credentials.</response>
	/// <response code="404">If no record was found.</response>
	/// <response code="500">If the something internal went wrong.</response>
	[HttpGet(Endpoints.Account.GetByNumber)]
	[ProducesResponseType(typeof(AccountResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> GetByNumber([RegularExpression(RegexPatterns.IBAN)] string iban, CancellationToken cancellationToken)
	{
		ErrorOr<AccountResponse> response =
			await _accountService.Get(_currentUserService.UserId, iban, false, cancellationToken);
		return Get(response);
	}

	/// <summary>
	/// Creates a new bank account for the application user.
	/// </summary>
	/// <param name="request">The bank account create request.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <response code="201">The created response.</response>
	/// <response code="401">No credentials or invalid credentials.</response>
	/// <response code="409">Conflicts occured during creation of the resource.</response>
	/// <response code="500">If the something internal went wrong.</response>
	[HttpPost(Endpoints.Account.Post)]
	[ProducesResponseType(typeof(Created), StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> Post(AccountCreateRequest request, CancellationToken cancellationToken)
	{
		ErrorOr<Created> response =
			await _accountService.Create(_currentUserService.UserId, request, cancellationToken);
		return PostWithoutLocation(response);
	}

	/// <summary>
	/// Updates an existing bank account for the application user.
	/// </summary>
	/// <param name="request">The bank account update request.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <response code="200">The updated response.</response>
	/// <response code="400">The update request is incorrect.</response>
	/// <response code="401">No credentials or invalid credentials.</response>
	/// <response code="500">If the something internal went wrong.</response>
	[HttpPut(Endpoints.Account.Put)]
	[ProducesResponseType(typeof(Updated), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> Put(AccountUpdateRequest request, CancellationToken cancellationToken)
	{
		ErrorOr<Updated> response =
			await _accountService.Update(_currentUserService.UserId, request, cancellationToken);
		return Put(response);
	}
}
