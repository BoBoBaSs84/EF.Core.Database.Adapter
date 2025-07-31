using BB84.Home.Application.Contracts.Requests.Documents;
using BB84.Home.Application.Contracts.Responses.Documents;
using BB84.Home.Application.Features.Requests;
using BB84.Home.Application.Features.Responses;
using BB84.Home.Domain.Errors;
using BB84.Home.Domain.Results;

namespace BB84.Home.Application.Interfaces.Application.Services.Documents;

/// <summary>
/// The interface for the document service.
/// </summary>
public interface IDocumentService
{
	/// <summary>
	/// Creates a new document with the provided create <paramref name="request"/>.
	/// </summary>
	/// <param name="request">The document create request to use.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<Created>> CreateAsync(DocumentCreateRequest request, CancellationToken token = default);

	/// <summary>
	/// Creates multiple new document collection with the provided create <paramref name="requests"/>.
	/// </summary>
	/// <param name="requests">The document create requests to use.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<Created>> CreateAsync(IEnumerable<DocumentCreateRequest> requests, CancellationToken token = default);

	/// <summary>
	/// Deletes an existing document with the provided <paramref name="id"/>.
	/// </summary>
	/// <param name="id">The document identifier to use.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<Deleted>> DeleteAsync(Guid id, CancellationToken token = default);

	/// <summary>
	/// Deletes an existing document collection with the provided <paramref name="ids"/>.
	/// </summary>
	/// <param name="ids">The document identifiers to use.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<Deleted>> DeleteAsync(IEnumerable<Guid> ids, CancellationToken token = default);

	/// <summary>
	/// Returns an existing document by the provided <paramref name="id"/>.
	/// </summary>
	/// <param name="id">The document identifier to use.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<DocumentResponse>> GetByIdAsync(Guid id, CancellationToken token = default);

	/// <summary>
	/// Returns a document collection as a paged list filtered by the provided document <paramref name="parameters"/>.
	/// </summary>
	/// <param name="parameters">The document query parameters to use.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<IPagedList<DocumentResponse>>> GetPagedByParametersAsync(DocumentParameters parameters, CancellationToken token = default);

	/// <summary>
	/// Updates an existing document with the provided update <paramref name="request"/>.
	/// </summary>
	/// <param name="request">The document update request to use.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<Updated>> UpdateAsync(DocumentUpdateRequest request, CancellationToken token = default);

	/// <summary>
	/// Updates an existing document collection with the provided update <paramref name="requests"/>.
	/// </summary>
	/// <param name="requests">The document update requests to use.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<Updated>> UpdateAsync(IEnumerable<DocumentUpdateRequest> requests, CancellationToken token = default);
}
