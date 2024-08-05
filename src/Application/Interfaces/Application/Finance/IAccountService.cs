using Application.Contracts.Requests.Finance;
using Application.Contracts.Responses.Finance;

using Domain.Errors;
using Domain.Results;

namespace Application.Interfaces.Application.Finance;

/// <summary>
/// The account service interface.
/// </summary>
public interface IAccountService
{
	/// <summary>
	/// Creates a new bank account for the application user.
	/// </summary>
	/// <param name="id">The identifier of the application user.</param>
	/// <param name="request">The account create request.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<Created>> Create(Guid id, AccountCreateRequest request, CancellationToken token = default);

	/// <summary>
	/// Deletes an existing bank account by the bank account identifier.
	/// </summary>
	/// <param name="id">The identifier of the bank account.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<Deleted>> Delete(Guid id, CancellationToken token = default);

	/// <summary>
	/// Returns a bank account by the bank account identifier.
	/// </summary>
	/// <param name="id">The identifier of the bank account.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<AccountResponse>> GetById(Guid id, CancellationToken token = default);

	/// <summary>
	/// Returns a collection of bank accounts for for the application user.
	/// </summary>
	/// <param name="id">The identifier of the application user.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<IEnumerable<AccountResponse>>> GetByUserId(Guid id, CancellationToken token = default);

	/// <summary>
	/// Updates an existing bank account by the bank account identifier.
	/// </summary>
	/// <param name="id">The identifier of the bank account.</param>
	/// <param name="request">The account update request.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<Updated>> Update(Guid id, AccountUpdateRequest request, CancellationToken token = default);
}
