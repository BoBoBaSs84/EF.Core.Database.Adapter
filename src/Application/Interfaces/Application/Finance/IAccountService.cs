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
	/// <param name="userId">The identifier of the application user.</param>
	/// <param name="request">The account create request.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<Created>> Create(Guid userId, AccountCreateRequest request, CancellationToken cancellationToken = default);

	/// <summary>
	/// Deletes an existing bank account for the application user by the bank account identifier.
	/// </summary>
	/// <param name="userId">The identifier of the application user.</param>
	/// <param name="accountId">The identifier of the bank account.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<Deleted>> Delete(Guid userId, Guid accountId, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns a bank account for the application user by the bank account identifier.
	/// </summary>
	/// <param name="userId">The identifier of the application user.</param>
	/// <param name="accountId">The identifier of the bank account.</param>
	/// <param name="trackChanges">Should the fetched entries be tracked?</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<AccountResponse>> Get(Guid userId, Guid accountId, bool trackChanges = false, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns a bank account for the application user by the international bank account number.
	/// </summary>
	/// <param name="userId">The identifier of the application user.</param>
	/// <param name="iban">The international bank account number.</param>
	/// <param name="trackChanges">Should the fetched entries be tracked?</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns>A account entity.</returns>
	Task<ErrorOr<AccountResponse>> Get(Guid userId, string iban, bool trackChanges = false, CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns a collection of bank accounts for for the application user.
	/// </summary>
	/// <param name="userId">The identifier of the application user.</param>
	/// <param name="trackChanges">Should the fetched entries be tracked?</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns>A collection of account entities.</returns>
	Task<ErrorOr<IEnumerable<AccountResponse>>> Get(Guid userId, bool trackChanges = false, CancellationToken cancellationToken = default);

	/// <summary>
	/// Updates an existing bank account for the application user.
	/// </summary>
	/// <param name="userId">The identifier of the application user.</param>
	/// <param name="request">The account update request.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<Updated>> Update(Guid userId, AccountUpdateRequest request, CancellationToken cancellationToken = default);
}
