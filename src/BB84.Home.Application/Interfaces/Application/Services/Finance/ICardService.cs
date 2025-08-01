using BB84.Home.Application.Contracts.Requests.Finance;
using BB84.Home.Application.Contracts.Responses.Finance;
using BB84.Home.Domain.Errors;
using BB84.Home.Domain.Results;

namespace BB84.Home.Application.Interfaces.Application.Services.Finance;

/// <summary>
/// The card service Guiderface.
/// </summary>
public interface ICardService
{
	/// <summary>
	/// Creates a new bank card for the bank account.
	/// </summary>
	/// <param name="accountId">The identifier of the bank account.</param>
	/// <param name="request">The bank card create request.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<Created>> CreateAsync(Guid accountId, CardCreateRequest request, CancellationToken token = default);

	/// <summary>
	/// Deletes an existing bank card by the bank card identifier.
	/// </summary>
	/// <param name="id">The identifier of the bank card.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<Deleted>> DeleteAsync(Guid id, CancellationToken token = default);

	/// <summary>
	/// Returns a bank card by the bank card identifier.
	/// </summary>
	/// <param name="id">The identifier of the bank card.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<CardResponse>> GetByIdAsync(Guid id, CancellationToken token = default);

	/// <summary>
	/// Returns a collection of bank cards.
	/// </summary>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<IEnumerable<CardResponse>>> GetAllAsync(CancellationToken token = default);

	/// <summary>
	/// Updates an existing bank card by the bank card identifier.
	/// </summary>
	/// <param name="id">The identifier of the bank card.</param>
	/// <param name="request">The bank card update request.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<Updated>> UpdateAsync(Guid id, CardUpdateRequest request, CancellationToken token = default);
}
