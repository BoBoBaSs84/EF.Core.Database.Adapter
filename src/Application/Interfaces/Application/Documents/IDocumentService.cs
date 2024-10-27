using Application.Contracts.Requests.Documents;
using Application.Contracts.Responses.Documents;
using Application.Features.Requests;
using Application.Features.Responses;

using Domain.Errors;
using Domain.Results;

namespace Application.Interfaces.Application.Documents;

/// <summary>
/// The interface for the document service.
/// </summary>
public interface IDocumentService
{
	/// <summary>
	/// Creates a new document for the <paramref name="userId"/> with the provided create <paramref name="request"/>.
	/// </summary>
	/// <param name="userId">The identifier of the application user.</param>
	/// <param name="request">The document create request to use.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<Created>> Create(Guid userId, DocumentCreateRequest request, CancellationToken token = default);

	/// <summary>
	/// Creates multiple new document collection for the <paramref name="userId"/> with the provided create <paramref name="requests"/>.
	/// </summary>
	/// <param name="userId">The identifier of the application user.</param>
	/// <param name="requests">The document create requests to use.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<Created>> CreateMultiple(Guid userId, IEnumerable<DocumentCreateRequest> requests, CancellationToken token = default);

	/// <summary>
	/// Deletes an existing document by the provided <paramref name="id"/>.
	/// </summary>
	/// <param name="id">The document identifier to use.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<Deleted>> DeleteById(Guid id, CancellationToken token = default);

	/// <summary>
	/// Deletes an existing document collection by the provided <paramref name="ids"/>.
	/// </summary>
	/// <param name="ids">The document identifiers to use.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<Deleted>> DeleteByIds(IEnumerable<Guid> ids, CancellationToken token = default);

	/// <summary>
	/// Returns an existing document by the provided <paramref name="id"/>.
	/// </summary>
	/// <param name="id">The document identifier to use.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<DocumentResponse>> GetById(Guid id, CancellationToken token = default);

	/// <summary>
	/// Returns a document collection as a paged list for the <paramref name="userId"/> filtered by the document <paramref name="parameters"/>.
	/// </summary>
	/// <param name="userId">The identifier of the application user.</param>
	/// <param name="parameters">The document query parameters to use.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<IPagedList<DocumentResponse>>> GetPagedByParameters(Guid userId, DocumentParameters parameters, CancellationToken token = default);

	/// <summary>
	/// Updates an existing document with the provided create <paramref name="request"/>.
	/// </summary>
	/// <param name="request">The document update request to use.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<Updated>> Update(DocumentUpdateRequest request, CancellationToken token = default);

	/// <summary>
	/// Updates an existing document collection with the provided create <paramref name="requests"/>.
	/// </summary>
	/// <param name="requests">The document update requests to use.</param>
	/// <param name="token">The cancellation token to cancel the request.</param>
	/// <returns><see cref="ErrorOr{TValue}"/></returns>
	Task<ErrorOr<Updated>> Update(IEnumerable<DocumentUpdateRequest> requests, CancellationToken token = default);
}
