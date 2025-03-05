using BB84.Home.Application.Contracts.Requests.Finance;
using BB84.Home.Application.Contracts.Responses.Finance;
using BB84.Home.Application.Features.Requests;
using BB84.Home.Application.Features.Responses;
using BB84.Home.Domain.Errors;
using BB84.Home.Domain.Results;
using BB84.Home.Presentation.Common;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BB84.Home.Presentation.Controllers;

public sealed partial class CardController
{
	/// <summary>
	/// Deletes an existing transaction for the bank card.
	/// </summary>
	/// <param name="cardId">The identifier of the bank card.</param>
	/// <param name="id">The identifier of the transaction.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <response code="200">The resource was successfully deleted.</response>
	/// <response code="401">No credentials or invalid credentials were supplied.</response>
	/// <response code="404">The requested resource could not be found.</response>
	/// <response code="500">Something internal went terribly wrong.</response>
	[HttpDelete(Endpoints.Card.Transaction.Delete)]
	[ProducesResponseType(typeof(Deleted), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> Delete(Guid cardId, Guid id, CancellationToken token)
	{
		ErrorOr<Deleted> response = await _transactionService
			.DeleteByCardId(cardId, id, token)
			.ConfigureAwait(false);

		return Delete(response);
	}

	/// <summary>
	/// Returns an existing transaction for the bank card.
	/// </summary>
	/// <param name="cardId">The identifier of the bank card.</param>
	/// <param name="id">The identifier of the bank transaction.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <response code="200">The successful response.</response>
	/// <response code="401">No credentials or invalid credentials.</response>
	/// <response code="404">If no transaction record to get were found.</response>
	/// <response code="500">If the something internal went wrong.</response>
	[HttpGet(Endpoints.Card.Transaction.GetByCardId)]
	[ProducesResponseType(typeof(TransactionResponse), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> GetByCardId(Guid cardId, Guid id, CancellationToken token)
	{
		ErrorOr<TransactionResponse> response = await _transactionService
			.GetByCardId(cardId, id, token)
			.ConfigureAwait(false);

		return Get(response);
	}

	/// <summary>
	/// Returns multiple transaction entries as a paged list for a bank card filtered by the transaction query parameters.
	/// </summary>
	/// <param name="cardId">The identifier of the bank card.</param>
	/// <param name="parameters">The transaction query parameters.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <response code="200">The successful response.</response>
	/// <response code="401">No credentials or invalid credentials.</response>
	/// <response code="404">If no record was found.</response>
	/// <response code="500">If the something internal went wrong.</response>
	[HttpGet(Endpoints.Card.Transaction.GetPagedByCardId)]
	[ProducesResponseType(typeof(IPagedList<TransactionResponse>), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> GetPagedByCardId(Guid cardId, [FromQuery] TransactionParameters parameters, CancellationToken token)
	{
		ErrorOr<IPagedList<TransactionResponse>> response = await _transactionService
			.GetPagedByCardId(cardId, parameters, token)
			.ConfigureAwait(false);

		return Get(response, response.Value.MetaData);
	}

	/// <summary>
	/// Creates a new transaction for the bank card.
	/// </summary>
	/// <param name="cardId">The identifier of the bank card.</param>
	/// <param name="request">The transaction create request.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <response code="201">The created response.</response>
	/// <response code="401">No credentials or invalid credentials.</response>
	/// <response code="404">If no record was found.</response>
	/// <response code="500">If the something internal went wrong.</response>
	[HttpPost(Endpoints.Card.Transaction.Post)]
	[ProducesResponseType(typeof(Created), StatusCodes.Status201Created)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> Post(Guid cardId, [FromBody] TransactionCreateRequest request, CancellationToken token)
	{
		ErrorOr<Created> response = await _transactionService
			.CreateByCardId(cardId, request, token)
			.ConfigureAwait(false);

		return PostWithoutLocation(response);
	}

	/// <summary>
	/// Updates an existing transaction for the card account.
	/// </summary>
	/// <param name="cardId">The identifier of the bank account.</param>
	/// <param name="id">The identifier of the bank transaction to update.</param>
	/// <param name="request">The transaction update request.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <response code="200">The updated response.</response>
	/// <response code="401">No credentials or invalid credentials.</response>
	/// <response code="404">If no transaction record to update were found.</response>
	/// <response code="500">If the something internal went wrong.</response>
	[HttpPut(Endpoints.Card.Transaction.Put)]
	[ProducesResponseType(typeof(Updated), StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
	[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
	public async Task<IActionResult> Put(Guid cardId, Guid id, [FromBody] TransactionUpdateRequest request, CancellationToken token)
	{
		ErrorOr<Updated> response = await _transactionService
			.UpdateByCardId(cardId, id, request, token)
			.ConfigureAwait(false);

		return Put(response);
	}
}
