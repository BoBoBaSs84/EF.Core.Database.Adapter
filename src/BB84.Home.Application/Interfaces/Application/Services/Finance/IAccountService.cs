using BB84.Home.Application.Contracts.Requests.Finance;
using BB84.Home.Application.Contracts.Responses.Finance;
using BB84.Home.Domain.Errors;
using BB84.Home.Domain.Results;

namespace BB84.Home.Application.Interfaces.Application.Services.Finance;

/// <summary>
/// The account service interface.
/// </summary>
public interface IAccountService
{
	/// <summary>
	/// Creates a new bank account.
	/// </summary>
	/// <param name="request">The account create request.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<Created>> CreateAsync(AccountCreateRequest request, CancellationToken token = default);

	/// <summary>
	/// Deletes an existing bank account by the bank account identifier.
	/// </summary>
	/// <param name="id">The identifier of the bank account.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<Deleted>> DeleteAsync(Guid id, CancellationToken token = default);

	/// <summary>
	/// Returns a bank account by the bank account identifier.
	/// </summary>
	/// <param name="id">The identifier of the bank account.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<AccountResponse>> GetByIdAsync(Guid id, CancellationToken token = default);

	/// <summary>
	/// Returns a collection of bank accounts.
	/// </summary>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<IEnumerable<AccountResponse>>> GetAllAsync(CancellationToken token = default);

	/// <summary>
	/// Updates an existing bank account by the bank account identifier.
	/// </summary>
	/// <param name="id">The identifier of the bank account.</param>
	/// <param name="request">The account update request.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<Updated>> UpdateAsync(Guid id, AccountUpdateRequest request, CancellationToken token = default);
}
