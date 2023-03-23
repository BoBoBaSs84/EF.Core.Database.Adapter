using Application.Contracts.Responses.Finance;
using Application.Interfaces.Application;
using Application.Interfaces.Infrastructure.Identity;
using Domain.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Common;
using Presentation.Controllers.Base;
using System.ComponentModel.DataAnnotations;
using RegexPatterns = Domain.Constants.DomainConstants.RegexPatterns;

namespace Presentation.Controllers;

/// <summary>
/// The <see cref="AttendanceController"/> class.
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
	/// Initializes an instance of <see cref="AccountController"/> class.
	/// </summary>
	/// <param name="accountService">The account service.</param>
	/// <param name="currentUserService">The current user service.</param>
	public AccountController(IAccountService accountService, ICurrentUserService currentUserService)
	{
		_accountService = accountService;
		_currentUserService = currentUserService;
	}

	[HttpGet(Endpoints.Account.GetAll)]
	public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
	{
		ErrorOr<IEnumerable<AccountResponse>> result =
			await _accountService.GetAll(_currentUserService.UserId, false, cancellationToken);

		return Get(result);
	}

	[HttpGet(Endpoints.Account.GetByIban)]
	public async Task<IActionResult> GetByIban([RegularExpression(RegexPatterns.IBAN)] string iban, CancellationToken cancellationToken)
	{
		ErrorOr<AccountResponse> result =
			await _accountService.GetByIban(_currentUserService.UserId, iban, false, cancellationToken);

		return Get(result);
	}
}
