using Application.Contracts.Requests.Documents;
using Application.Contracts.Responses.Documents;
using Application.Features.Requests;
using Application.Features.Responses;
using Application.Interfaces.Application.Documents;
using Application.Interfaces.Infrastructure.Services;

using AutoMapper;

using BB84.Extensions.Serialization;

using Domain.Errors;
using Domain.Models.Documents;
using Domain.Results;

using Microsoft.Extensions.Logging;

namespace Application.Services.Documents;

/// <summary>
/// The document service class.
/// </summary>
internal sealed class DocumentService(ILoggerService<DocumentService> loggerService, IRepositoryService repositoryService, IMapper mapper) : IDocumentService
{
	private static readonly Action<ILogger, object, Exception?> LogExceptionWithParams =
		LoggerMessage.Define<object>(LogLevel.Error, 0, "Exception occured. Params = {Parameters}");

	public async Task<ErrorOr<Created>> Create(Guid userId, DocumentCreateRequest request, CancellationToken token = default)
	{
		try
		{
			Document? entity = await repositoryService.DocumentRepository
				.GetByConditionAsync(x => x.MD5Hash.SequenceEqual(request.MD5Hash), trackChanges: true, token: token)
				.ConfigureAwait(false);

			if (entity is not null)
			{
				entity.DocumentUsers = [new() { UserId = userId, DocumentId = entity.Id }];

				await repositoryService.CommitChangesAsync(token)
					.ConfigureAwait(false);

				return Result.Created;
			}

			Document document = mapper.Map<Document>(request);
			document.DocumentUsers = [new() { UserId = userId, Document = document }];

			await repositoryService.DocumentRepository.CreateAsync(document, token)
				.ConfigureAwait(false);

			await repositoryService.CommitChangesAsync(token)
				.ConfigureAwait(false);

			return Result.Created;
		}
		catch (Exception ex)
		{
			string[] parameters = [$"{userId}", $"{request.ToJson()}"];
			loggerService.Log(LogExceptionWithParams, parameters, ex);
			// TODO
			return Result.Created;
		}
	}

	public Task<ErrorOr<Created>> Create(Guid userId, IEnumerable<DocumentCreateRequest> requests, CancellationToken token = default)
	{
		throw new NotImplementedException();
	}

	public Task<ErrorOr<Deleted>> DeleteById(Guid userId, Guid documentId, CancellationToken token = default)
	{
		throw new NotImplementedException();
	}

	public Task<ErrorOr<Deleted>> DeleteByIds(Guid userId, IEnumerable<Guid> documentIds, CancellationToken token = default)
	{
		throw new NotImplementedException();
	}

	public async Task<ErrorOr<DocumentResponse>> GetById(Guid userId, Guid documentId, CancellationToken token = default)
	{
		try
		{
			Document? entity = await repositoryService.DocumentRepository.GetByConditionAsync(
				expression: x => x.Id.Equals(documentId) && x.DocumentUsers.Select(x => x.UserId).Contains(userId),
				token: token,
				includeProperties: [nameof(Document.Data), nameof(Document.Extension)])
				.ConfigureAwait(false);

			if (entity is null)
				// TODO
				throw new InvalidDataException();

			var response = mapper.Map<DocumentResponse>(entity);

			return response;
		}
		catch (Exception ex)
		{
			string[] parameters = [$"{userId}", $"{documentId}"];
			loggerService.Log(LogExceptionWithParams, parameters, ex);
			// TODO
			throw new InvalidDataException();
		}
	}

	public Task<ErrorOr<IPagedList<DocumentResponse>>> GetPagedByParameters(Guid userId, DocumentParameters parameters, CancellationToken token = default)
	{
		throw new NotImplementedException();
	}

	public Task<ErrorOr<Updated>> Update(Guid userId, DocumentUpdateRequest request, CancellationToken token = default)
	{
		throw new NotImplementedException();
	}

	public Task<ErrorOr<Updated>> Update(Guid userId, IEnumerable<DocumentUpdateRequest> requests, CancellationToken token = default)
	{
		throw new NotImplementedException();
	}
}
