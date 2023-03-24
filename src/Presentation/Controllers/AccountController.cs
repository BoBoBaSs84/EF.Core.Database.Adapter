using Application.Contracts.Responses.Finance;
using Application.Interfaces.Application;
using Application.Interfaces.Infrastructure.Identity;
using Domain.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Attributes;
using Presentation.Common;
using Presentation.Controllers.Base;
using System.ComponentModel.DataAnnotations;
using RegexPatterns = Domain.Constants.DomainConstants.RegexPatterns;
using Roles = Domain.Enumerators.RoleTypes;

namespace Presentation.Controllers;

/// <summary>
/// The <see cref="AttendanceController"/> class.
/// </summary>
/// <remarks>
/// Inherits from <see cref="ApiControllerBase"/>.
/// </remarks>
[Route(Endpoints.Account.BaseUri)]
[ApiVersion(Versioning.CurrentVersion)]
[AuthorizeRoles(Roles.USER, Roles.SUPERUSER, Roles.ADMINISTRATOR)]
public sealed class AccountController : ApiControllerBase
{
	private readonly IAccountService _accountService;
	private readonly ICurrentUserService _currentUserService;

	/// <summary>
	/// Initializes an instance of <see cref="AccountController"/> class.
	/// </summary>
	/// <param name="accountService">The account service.</param>
	/// <param name="currentUserService">The current user service.</param>
	public AccountController(IAccountService accountService, ICurrentUserService currentUserService)
	{
		_accountService = accountService;
		_currentUserService = currentUserService;
	}

	/// <summary>
	/// Should get a collection of account entities.
	/// </summary>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <response code="200">The successful response.</response>
	/// <response code="401">No credentials or invalid credentials.</response>
	/// <response code="403">Not enough privileges to perform an action.</response>
	/// <response code="404">If no records were found.</response>
	/// <response code="500">If the something went wrong.</response>
	[HttpGet(Endpoints.Account.GetAll)]
	[ProducesResponseType(typeof(IEnumerable<AccountResponse>), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
	{
		ErrorOr<IEnumerable<AccountResponse>> result =
			await _accountService.GetAll(_currentUserService.UserId, false, cancellationToken);

		return Get(result);
	}

	/// <summary>
	/// Should get a account entity by the international bank account number.
	/// </summary>
	/// <param name="iban">The international bank account number.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <response code="200">The successful response.</response>
	/// <response code="401">No credentials or invalid credentials.</response>
	/// <response code="403">Not enough privileges to perform an action.</response>
	/// <response code="404">If no record was found.</response>
	/// <response code="500">If the something went wrong.</response>	
	[HttpGet(Endpoints.Account.GetByIban)]
	[ProducesResponseType(typeof(IEnumerable<AccountResponse>), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status403Forbidden)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> GetByNumber([RegularExpression(RegexPatterns.IBAN)] string iban, CancellationToken cancellationToken)
	{
		ErrorOr<AccountResponse> result =
			await _accountService.GetByNumber(_currentUserService.UserId, iban, false, cancellationToken);

		return Get(result);
	}
}
