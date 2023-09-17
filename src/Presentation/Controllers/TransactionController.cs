using Application.Contracts.Requests.Finance;
using Application.Contracts.Responses.Finance;
using Application.Interfaces.Application;

using Domain.Errors;
using Domain.Results;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Presentation.Common;
using Presentation.Controllers.Base;

namespace Presentation.Controllers;

/// <summary>
/// The bank transaction controller class.
/// </summary>
/// <remarks>
/// Inherits from <see cref="ApiControllerBase"/>.
/// </remarks>
[Authorize]
[Route(Endpoints.Account.BaseUri)]
[ApiVersion(Versioning.CurrentVersion)]
public sealed class TransactionController : ApiControllerBase
{
	private readonly ITransactionService _transactionService;

	/// <summary>
	/// Initializes an instance of the bank transaction controller class.
	/// </summary>
	/// <param name="transactionService">The transaction service to use.</param>
	public TransactionController(ITransactionService transactionService)
		=> _transactionService = transactionService;

	/// <summary>
	/// Deletes an existing bank transaction.
	/// </summary>
	/// <param name="id">The identifier of the transaction.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <response code="200">The deleted response.</response>
	/// <response code="401">No credentials or invalid credentials.</response>
	/// <response code="404">If the bank account to delete was not found.</response>
	/// <response code="500">If the something internal went wrong.</response>		
	[HttpDelete(Endpoints.Transaction.Delete)]
	[ProducesResponseType(typeof(Deleted), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
	{
		ErrorOr<Deleted> response =
			await _transactionService.Delete(id, cancellationToken);

		return Delete(response);
	}

	/// <summary>
	/// Returns a bank transaction by its identifier.
	/// </summary>
	/// <param name="id">The identifier of the transaction.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <response code="200">The successful response.</response>
	/// <response code="401">No credentials or invalid credentials.</response>
	/// <response code="404">If no transaction record to get were found.</response>
	/// <response code="500">If the something internal went wrong.</response>
	[HttpGet(Endpoints.Transaction.Get)]
	[ProducesResponseType(typeof(TransactionResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
	{
		ErrorOr<TransactionResponse> response =
			await _transactionService.Get(id, false, cancellationToken);

		return Get(response);
	}

	/// <summary>
	/// Updates an existing bank transaction.
	/// </summary>
	/// <param name="id">The identifier of the transaction.</param>
	/// <param name="request">The transaction update request.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <response code="200">The updated response.</response>
	/// <response code="401">No credentials or invalid credentials.</response>
	/// <response code="404">If no transaction record to update were found.</response>
	/// <response code="500">If the something internal went wrong.</response>
	[HttpPut(Endpoints.Transaction.Put)]
	[ProducesResponseType(typeof(Updated), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> Put(Guid id, [FromBody] TransactionUpdateRequest request, CancellationToken cancellationToken)
	{
		ErrorOr<Updated> response =
			await _transactionService.Update(id, request, cancellationToken);

		return Put(response);
	}
}
