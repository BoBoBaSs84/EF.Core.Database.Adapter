using Application.Contracts.Requests.Documents;
using Application.Contracts.Responses.Documents;
using Application.Features.Requests;
using Application.Features.Responses;
using Application.Interfaces.Application.Documents;
using Application.Interfaces.Infrastructure.Services;

using AutoMapper;

using BB84.Extensions;
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
			//byte[] md5Hash = request.Data.GetMD5();

			//Document? entity = await repositoryService.DocumentRepository
			//	.GetByConditionAsync(
			//		expression: x => x.MD5Hash.SequenceEqual(md5Hash),
			//		trackChanges: true,
			//		token: token,
			//		includeProperties: [nameof(Document.DocumentUsers)])
			//	.ConfigureAwait(false);

			//if (entity is not null)
			//{
			//	if (entity.DocumentUsers.Select(x => x.UserId).Contains(userId))
			//		// TODO: Conflict
			//		throw new InvalidOperationException();

			//	entity.DocumentUsers.Add(new() { UserId = userId, DocumentId = entity.Id });

			//	_ = await repositoryService.CommitChangesAsync(token)
			//		.ConfigureAwait(false);

			//	return Result.Created;
			//}

			//Document document = mapper.Map<Document>(request);
			//document.DocumentUsers = [new() { UserId = userId, Document = document }];

			//await repositoryService.DocumentRepository.CreateAsync(document, token)
			//	.ConfigureAwait(false);

			_ = await repositoryService.CommitChangesAsync(token)
				.ConfigureAwait(false);

			return Result.Created;
		}
		catch (Exception ex)
		{
			string[] parameters = [$"{userId}", $"{request.ToJson()}"];
			loggerService.Log(LogExceptionWithParams, parameters, ex);
			// TODO: Failed
			throw new InvalidOperationException();
		}
	}

	public async Task<ErrorOr<Created>> Create(Guid userId, IEnumerable<DocumentCreateRequest> requests, CancellationToken token = default)
	{
		try
		{
			//var md5Hashs = requests.Select(x => x.Data.GetMD5());

			//IEnumerable<Document> entities = await repositoryService.DocumentRepository
			//	.GetManyByConditionAsync(
			//		expression: x => md5Hashs.Contains(x.MD5Hash),
			//		trackChanges: true,
			//		token: token,
			//		includeProperties: [nameof(Document.DocumentUsers)])
			//	.ConfigureAwait(false);

			//if (entities.Any())
			//{
			//	if (entities.SelectMany(x => x.DocumentUsers).Select(x => x.UserId).Contains(userId))
			//		// TODO: Conflict
			//		throw new InvalidOperationException();

			//	foreach (Document entity in entities)
			//		entity.DocumentUsers.Add(new() { UserId = userId, DocumentId = entity.Id });
			//}

			//IEnumerable<DocumentCreateRequest> leftOvers = requests
			//	.Where(x => entities.Select(x => x.MD5Hash).Contains([]).IsFalse());

			//foreach (DocumentCreateRequest leftOver in leftOvers)
			//{
			//	Document document = mapper.Map<Document>(leftOver);
			//	document.DocumentUsers = [new() { UserId = userId, Document = document }];

			//	await repositoryService.DocumentRepository.CreateAsync(document, token)
			//		.ConfigureAwait(false);
			//}

			_ = await repositoryService.CommitChangesAsync(token)
				.ConfigureAwait(false);

			return Result.Created;
		}
		catch (Exception ex)
		{
			string[] parameters = [$"{userId}", $"{requests.ToJson()}"];
			loggerService.Log(LogExceptionWithParams, parameters, ex);
			// TODO: Failed
			throw new InvalidOperationException();
		}
	}

	public async Task<ErrorOr<Deleted>> DeleteById(Guid userId, Guid documentId, CancellationToken token = default)
	{
		try
		{
			Document? entity = await repositoryService.DocumentRepository
				.GetByIdAsync(id: documentId, token: token, trackChanges: true, includeProperties: [nameof(Document.DocumentUsers)])
				.ConfigureAwait(false);

			if (entity is null)
				// TODO: NotFound
				throw new InvalidOperationException();

			DocumentUser? documentUser = entity.DocumentUsers.SingleOrDefault(x => x.UserId.Equals(userId));

			if (documentUser is null)
				// TODO: NotFound
				throw new InvalidOperationException();

			if (entity.DocumentUsers.Count > 1)
			{
				_ = entity.DocumentUsers.Remove(documentUser);

				_ = await repositoryService.CommitChangesAsync(token)
					.ConfigureAwait(false);

				return Result.Deleted;
			}

			await repositoryService.DocumentRepository.DeleteAsync(entity, token)
				.ConfigureAwait(false);

			_ = await repositoryService.CommitChangesAsync(token)
				.ConfigureAwait(false);

			return Result.Deleted;
		}
		catch (Exception ex)
		{
			string[] parameters = [$"{userId}", $"{documentId}"];
			loggerService.Log(LogExceptionWithParams, parameters, ex);
			// TODO: Failed
			throw new InvalidOperationException();
		}
	}

	public async Task<ErrorOr<Deleted>> DeleteByIds(Guid userId, IEnumerable<Guid> documentIds, CancellationToken token = default)
	{
		try
		{
			IEnumerable<Document> entities = await repositoryService.DocumentRepository
				.GetByIdsAsync(
					ids: documentIds,
					trackChanges: true,
					token: token,
					includeProperties: [nameof(Document.DocumentUsers)])
				.ConfigureAwait(false);

			if (entities.Any().IsFalse())
				// TODO: NotFound
				throw new InvalidOperationException();

			foreach (Document entity in entities)
			{
				if (entity.DocumentUsers.Count > 1)
				{
					DocumentUser? documentUser = entity.DocumentUsers
						.SingleOrDefault(x => x.UserId.Equals(userId));

					if (documentUser is null)
						continue;

					_ = entity.DocumentUsers.Remove(documentUser);
				}
				else
				{
					await repositoryService.DocumentRepository.DeleteAsync(entity, token)
						.ConfigureAwait(false);
				}
			}

			_ = await repositoryService.CommitChangesAsync(token)
				.ConfigureAwait(false);

			return Result.Deleted;
		}
		catch (Exception ex)
		{
			string[] parameters = [$"{userId}", string.Join(',', documentIds.Select(x => $"{x}"))];
			loggerService.Log(LogExceptionWithParams, parameters, ex);
			// TODO: Failed
			throw new InvalidDataException();
		}
	}

	public async Task<ErrorOr<DocumentResponse>> GetById(Guid userId, Guid documentId, CancellationToken token = default)
	{
		try
		{
			Document? entity = await repositoryService.DocumentRepository
				.GetByConditionAsync(
					expression: x => x.Id.Equals(documentId) && x.DocumentUsers.Select(x => x.UserId).Contains(userId),
					token: token,
					includeProperties: [nameof(Document.DocumentDatas), nameof(DocumentData.Data), nameof(Document.Extension)])
				.ConfigureAwait(false);

			if (entity is null)
				// TODO: NotFound
				throw new InvalidDataException();

			DocumentResponse response = mapper.Map<DocumentResponse>(entity);

			return response;
		}
		catch (Exception ex)
		{
			string[] parameters = [$"{userId}", $"{documentId}"];
			loggerService.Log(LogExceptionWithParams, parameters, ex);
			// TODO: Failed
			throw new InvalidDataException();
		}
	}

	public Task<ErrorOr<IPagedList<DocumentResponse>>> GetPagedByParameters(Guid userId, DocumentParameters parameters, CancellationToken token = default)
	{
		throw new NotImplementedException();
	}

	public async Task<ErrorOr<Updated>> Update(Guid userId, DocumentUpdateRequest request, CancellationToken token = default)
	{
		try
		{
			Document? entity = await repositoryService.DocumentRepository
				.GetByConditionAsync(
					expression: x => x.Id.Equals(request.Id) && x.DocumentUsers.Select(x => x.UserId).Contains(userId),
					trackChanges: true,
					token: token)
				.ConfigureAwait(false);

			if (entity is null)
				// TODO: NotFound
				throw new InvalidDataException();

			_ = mapper.Map(request, entity);

			_ = await repositoryService.CommitChangesAsync(token)
				.ConfigureAwait(false);

			return Result.Updated;
		}
		catch (Exception ex)
		{
			string[] parameters = [$"{userId}", $"{request.ToJson()}"];
			loggerService.Log(LogExceptionWithParams, parameters, ex);
			// TODO: Failed
			throw new InvalidDataException();
		}
	}

	public async Task<ErrorOr<Updated>> Update(Guid userId, IEnumerable<DocumentUpdateRequest> requests, CancellationToken token = default)
	{
		try
		{
			IEnumerable<Document> entities = await repositoryService.DocumentRepository
				.GetManyByConditionAsync(
					expression: x => requests.Select(x => x.Id).Contains(x.Id) && x.DocumentUsers.Select(x => x.UserId).Contains(userId),
					trackChanges: true,
					token: token)
				.ConfigureAwait(false);

			if (entities.Any().IsFalse())
				// TODO: NotFound
				throw new InvalidDataException();

			foreach (Document entity in entities)
				_ = mapper.Map(requests.Single(x => x.Id.Equals(entity.Id)), entity);

			_ = await repositoryService.CommitChangesAsync(token)
				.ConfigureAwait(false);

			return Result.Updated;
		}
		catch (Exception ex)
		{
			string[] parameters = [$"{userId}", $"{requests.ToJson()}"];
			loggerService.Log(LogExceptionWithParams, parameters, ex);
			// TODO: Failed
			throw new InvalidDataException();
		}
	}
}
