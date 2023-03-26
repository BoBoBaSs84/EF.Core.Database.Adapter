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
	/// Should create an account for the given <paramref name="userId"/>.
	/// </summary>
	/// <param name="userId">the user identifier.</param>
	/// <param name="createRequest">The account create request.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<Created>> Create(int userId, AccountCreateRequest createRequest, CancellationToken cancellationToken = default);

	/// <summary>
	/// Should get a account entity by the international bank account number.
	/// </summary>
	/// <param name="userId">the user identifier.</param>
	/// <param name="iban">The international bank account number.</param>
	/// <param name="trackChanges">Should the fetched entries be tracked?</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns>A account entity.</returns>
	Task<ErrorOr<AccountResponse>> GetByNumber(int userId, string iban, bool trackChanges = false, CancellationToken cancellationToken = default);

	/// <summary>
	/// Should get a collection of account entities by the user identifier.
	/// </summary>
	/// <param name="userId">the user identifier.</param>
	/// <param name="trackChanges">Should the fetched entries be tracked?</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns>A collection of account entities.</returns>
	Task<ErrorOr<IEnumerable<AccountResponse>>> GetAll(int userId, bool trackChanges = false, CancellationToken cancellationToken = default);

	/// <summary>
	/// Should update an account for the given <paramref name="userId"/>.
	/// </summary>
	/// <param name="userId">the user identifier.</param>
	/// <param name="updateRequest">The account update request.</param>
	/// <param name="cancellationToken">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<Updated>> Update(int userId, AccountUpdateRequest updateRequest, CancellationToken cancellationToken = default);
}
