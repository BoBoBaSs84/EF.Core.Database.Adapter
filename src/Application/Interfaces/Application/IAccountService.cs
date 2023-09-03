using Application.Contracts.Requests.Finance;
using Application.Contracts.Responses.Finance;

using Domain.Errors;
using Domain.Results;

namespace Application.Interfaces.Application;

/// <summary>
/// The account service interface.
/// </summary>
public interface IAccountService
{
	/// <summary>
	/// Creates an account for the given <paramref name="userId"/>.
	/// </summary>
	/// <param name="userId">The user identifier.</param>
	/// <param name="createRequest">The account create request.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<Created>> Create(Guid userId, AccountCreateRequest createRequest, CancellationToken cancellationToken = default);

	/// <summary>
	/// Deletes an account by the <paramref name="accountId"/> for the given <paramref name="userId"/>.
	/// </summary>
	/// <param name="userId">The user identifier.</param>
	/// <param name="accountId">The account identifier.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<Deleted>> Delete(Guid userId, Guid accountId, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns an account by the <paramref name="accountId"/> for the given <paramref name="userId"/>.
	/// </summary>
	/// <param name="userId">The user identifier.</param>
	/// <param name="accountId">The account identifier.</param>
	/// <param name="trackChanges">Should the fetched entries be tracked?</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<AccountResponse>> Get(Guid userId, Guid accountId, bool trackChanges = false, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns an account by the <paramref name="iban"/> (Guidernational bank account number) for the given <paramref name="userId"/>.
	/// </summary>
	/// <param name="userId">The user identifier.</param>
	/// <param name="iban">The Guidernational bank account number.</param>
	/// <param name="trackChanges">Should the fetched entries be tracked?</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns>A account entity.</returns>
	Task<ErrorOr<AccountResponse>> Get(Guid userId, string iban, bool trackChanges = false, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns a collection of accounts for the given <paramref name="userId"/>.
	/// </summary>
	/// <param name="userId">The user identifier.</param>
	/// <param name="trackChanges">Should the fetched entries be tracked?</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns>A collection of account entities.</returns>
	Task<ErrorOr<IEnumerable<AccountResponse>>> Get(Guid userId, bool trackChanges = false, CancellationToken cancellationToken = default);

	/// <summary>
	/// Updates an existing account for the given <paramref name="userId"/>.
	/// </summary>
	/// <param name="userId">The user identifier.</param>
	/// <param name="updateRequest">The account update request.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<Updated>> Update(Guid userId, AccountUpdateRequest updateRequest, CancellationToken cancellationToken = default);
}
