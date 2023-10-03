using Application.Contracts.Requests.Finance;
using Application.Contracts.Responses.Finance;
using Application.Features.Requests;
using Application.Features.Responses;

using Domain.Errors;
using Domain.Results;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Presentation.Common;

namespace Presentation.Controllers;

public sealed partial class CardController
{
	/// <summary>
	/// Deletes an existing transaction for a bank card.
	/// </summary>
	/// <param name="cardId">The identifier of the bank card.</param>
	/// <param name="transactionId">The identifier of the transaction.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <response code="200">The deleted response.</response>
	/// <response code="401">No credentials or invalid credentials.</response>
	/// <response code="404">If the bank account to delete was not found.</response>
	/// <response code="500">If the something internal went wrong.</response>		
	[HttpDelete(Endpoints.Card.Transaction.Delete)]
	[ProducesResponseType(typeof(Deleted), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> DeleteTransaction(Guid cardId, Guid transactionId, CancellationToken cancellationToken)
	{
		ErrorOr<Deleted> response =
			await _transactionService.DeleteForCard(cardId, transactionId, cancellationToken);
		return Delete(response);
	}

	/// <summary>
	/// Returns an existing transaction for a bank card.
	/// </summary>
	/// <param name="cardId">The identifier of the bank card.</param>
	/// <param name="transactionId">The identifier of the transaction.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <response code="200">The successful response.</response>
	/// <response code="401">No credentials or invalid credentials.</response>
	/// <response code="404">If no transaction record to get were found.</response>
	/// <response code="500">If the something internal went wrong.</response>
	[HttpGet(Endpoints.Card.Transaction.Get)]
	[ProducesResponseType(typeof(TransactionResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> GetTransaction(Guid cardId, Guid transactionId, CancellationToken cancellationToken)
	{
		ErrorOr<TransactionResponse> response =
			await _transactionService.GetByCardId(cardId, transactionId, false, cancellationToken);
		return Get(response);
	}

	/// <summary>
	/// Returns the transaction entries for a bank card as a paged list, filtered by the parameters.
	/// </summary>
	/// <param name="cardId">The identifier of the bank card.</param>
	/// <param name="parameters">The attendance query parameters.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <response code="200">The successful response.</response>
	/// <response code="401">No credentials or invalid credentials.</response>
	/// <response code="404">If no record was found.</response>
	/// <response code="500">If the something internal went wrong.</response>
	[HttpGet(Endpoints.Card.Transaction.GetAll)]
	[ProducesResponseType(typeof(IPagedList<TransactionResponse>), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> GetAllTransactions(Guid cardId, [FromQuery] TransactionParameters parameters, CancellationToken cancellationToken)
	{
		ErrorOr<IPagedList<TransactionResponse>> response =
			await _transactionService.GetByCardId(cardId, parameters, false, cancellationToken);
		return Get(response, response.Value?.MetaData);
	}

	/// <summary>
	/// Creates a new transaction for a bank card.
	/// </summary>
	/// <param name="cardId">The identifier of the bank card.</param>
	/// <param name="request">The transaction update request.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <response code="201">The created response.</response>
	/// <response code="401">No credentials or invalid credentials.</response>
	/// <response code="404">If no record was found.</response>
	/// <response code="500">If the something internal went wrong.</response>
	[HttpPost(Endpoints.Card.Transaction.Post)]
	[ProducesResponseType(typeof(Created), StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> PostTransaction(Guid cardId, [FromBody] TransactionCreateRequest request, CancellationToken cancellationToken)
	{
		ErrorOr<Created> response =
			await _transactionService.CreateByCardId(cardId, request, cancellationToken);
		return PostWithoutLocation(response);
	}

	/// <summary>
	/// Updates an existing transaction for a bank card.
	/// </summary>
	/// <param name="cardId">The identifier of the bank card.</param>
	/// <param name="transactionId">The identifier of the transaction.</param>
	/// <param name="request">The transaction update request.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <response code="200">The updated response.</response>
	/// <response code="401">No credentials or invalid credentials.</response>
	/// <response code="404">If no transaction record to update were found.</response>
	/// <response code="500">If the something internal went wrong.</response>
	[HttpPut(Endpoints.Card.Transaction.Put)]
	[ProducesResponseType(typeof(Updated), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> Put(Guid cardId, Guid transactionId, [FromBody] TransactionUpdateRequest request, CancellationToken cancellationToken)
	{
		ErrorOr<Updated> response =
			await _transactionService.UpdateByCardId(cardId, transactionId, request, cancellationToken);
		return Put(response);
	}
}
