using Application.Contracts.Requests.Finance;
using Application.Contracts.Responses.Finance;

using Domain.Errors;
using Domain.Results;

namespace Application.Interfaces.Application.Finance;

/// <summary>
/// The card service Guiderface.
/// </summary>
public interface ICardService
{
	/// <summary>
	/// Creates a bank card for the application user and bank account.
	/// </summary>
	/// <param name="userId">The identifier of the application user.</param>
	/// <param name="accountId">The identifier of the bank account.</param>
	/// <param name="request">The bank card create request.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<Created>> Create(Guid userId, Guid accountId, CardCreateRequest request, CancellationToken token = default);

	/// <summary>
	/// Deletes an existing bank card by the bank card identifier.
	/// </summary>
	/// <param name="id">The identifier of the bank card.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<Deleted>> Delete(Guid id, CancellationToken token = default);

	/// <summary>
	/// Returns a bank card by the bank card identifier.
	/// </summary>
	/// <param name="id">The identifier of the bank card.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<CardResponse>> GetById(Guid id, CancellationToken token = default);

	/// <summary>
	/// Returns a collection of bank cards for for the application user.
	/// </summary>
	/// <param name="id">The identifier of the application user.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<IEnumerable<CardResponse>>> GetByUserId(Guid id, CancellationToken token = default);

	/// <summary>
	/// Updates an existing bank card by the bank card identifier.
	/// </summary>
	/// <param name="id">The identifier of the bank card.</param>
	/// <param name="request">The bank card update request.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<Updated>> Update(Guid id, CardUpdateRequest request, CancellationToken token = default);
}
