using Application.Common;
using Application.Contracts.Requests.Documents;
using Application.Contracts.Requests.Documents.Base;
using Application.Contracts.Responses.Documents;
using Application.Errors.Services;
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
			Document document = await PrepareDocumentForCreate(userId, request, token)
				.ConfigureAwait(false);

			await repositoryService.DocumentRepository.CreateAsync(document, token)
				.ConfigureAwait(false);

			_ = await repositoryService.CommitChangesAsync(token)
				.ConfigureAwait(false);

			return Result.Created;
		}
		catch (Exception ex)
		{
			string[] parameters = [$"{userId}", $"{request.ToJson()}"];
			loggerService.Log(LogExceptionWithParams, parameters, ex);

			string document = $"{request.Name}.{request.ExtensionName}";
			return DocumentServiceErrors.CreateDocumentFailed(document);
		}
	}

	public async Task<ErrorOr<Created>> CreateMultiple(Guid userId, IEnumerable<DocumentCreateRequest> requests, CancellationToken token = default)
	{
		try
		{
			if (requests.Any().IsFalse())
				return DocumentServiceErrors.CreateMultipleDocumentNotEmpty;

			List<Document> documents = [];

			foreach (DocumentCreateRequest request in requests)
			{
				Document document = await PrepareDocumentForCreate(userId, request, token)
					.ConfigureAwait(false);

				documents.Add(document);
			}

			await repositoryService.DocumentRepository.CreateAsync(documents, token)
				.ConfigureAwait(false);

			_ = await repositoryService.CommitChangesAsync(token)
				.ConfigureAwait(false);

			return Result.Created;
		}
		catch (Exception ex)
		{
			string[] parameters = [$"{userId}", $"{requests.ToJson()}"];
			loggerService.Log(LogExceptionWithParams, parameters, ex);

			IEnumerable<string> documents = requests.Select(x => $"{x.Name}.{x.ExtensionName}");
			return DocumentServiceErrors.CreateMultipleDocumentFailed(documents);
		}
	}

	public async Task<ErrorOr<Deleted>> DeleteById(Guid id, CancellationToken token = default)
	{
		try
		{
			Document? entity = await repositoryService.DocumentRepository
				.GetByIdAsync(id, token: token)
				.ConfigureAwait(false);

			if (entity is null)
				// TODO: NotFound
				throw new InvalidOperationException();

			await repositoryService.DocumentRepository.DeleteAsync(entity, token)
				.ConfigureAwait(false);

			_ = await repositoryService.CommitChangesAsync(token)
				.ConfigureAwait(false);

			return Result.Deleted;
		}
		catch (Exception ex)
		{
			loggerService.Log(LogExceptionWithParams, $"{id}", ex);
			// TODO: Failed
			throw new InvalidOperationException();
		}
	}

	public async Task<ErrorOr<Deleted>> DeleteByIds(IEnumerable<Guid> ids, CancellationToken token = default)
	{
		try
		{
			IEnumerable<Document> entities = await repositoryService.DocumentRepository
				.GetByIdsAsync(ids, token: token)
				.ConfigureAwait(false);

			if (entities.Any().IsFalse())
				// TODO: NotFound
				throw new InvalidOperationException();

			await repositoryService.DocumentRepository.DeleteAsync(entities, token)
				.ConfigureAwait(false);

			_ = await repositoryService.CommitChangesAsync(token)
				.ConfigureAwait(false);

			return Result.Deleted;
		}
		catch (Exception ex)
		{
			loggerService.Log(LogExceptionWithParams, string.Join(',', ids.Select(x => $"{x}")), ex);
			// TODO: Failed
			throw new InvalidDataException();
		}
	}

	public async Task<ErrorOr<DocumentResponse>> GetById(Guid id, CancellationToken token = default)
	{
		try
		{
			Document? entity = await repositoryService.DocumentRepository
				.GetByIdAsync(id, token: token, includeProperties: [nameof(Document.Data), nameof(Document.Extension)])
				.ConfigureAwait(false);

			if (entity is null)
				return DocumentServiceErrors.GetByIdNotFound(id);

			DocumentResponse response = mapper.Map<DocumentResponse>(entity);

			return response;
		}
		catch (Exception ex)
		{
			loggerService.Log(LogExceptionWithParams, $"{id}", ex);
			return DocumentServiceErrors.GetByIdFailed(id);
		}
	}

	public Task<ErrorOr<IPagedList<DocumentResponse>>> GetPagedByParameters(Guid userId, DocumentParameters parameters, CancellationToken token = default)
	{
		throw new NotImplementedException();
	}

	public async Task<ErrorOr<Updated>> Update(DocumentUpdateRequest request, CancellationToken token = default)
	{
		try
		{
			Document? document = await repositoryService.DocumentRepository
				.GetByIdAsync(request.Id, default, true, token, [nameof(Document.Extension), nameof(Document.Data)])
				.ConfigureAwait(false);

			if (document is null)
				// TODO: NotFound
				throw new InvalidDataException();

			document = await PrepareDocumentForUpdate(document, request, token)
				.ConfigureAwait(false);

			_ = await repositoryService.CommitChangesAsync(token)
				.ConfigureAwait(false);

			return Result.Updated;
		}
		catch (Exception ex)
		{
			loggerService.Log(LogExceptionWithParams, $"{request.ToJson()}", ex);
			// TODO: Failed
			throw new InvalidDataException();
		}
	}

	public async Task<ErrorOr<Updated>> Update(IEnumerable<DocumentUpdateRequest> requests, CancellationToken token = default)
	{
		try
		{
			if (requests.Any().IsFalse())
				// TODO: BadRequest
				throw new InvalidOperationException();

			IEnumerable<Document> documents = await repositoryService.DocumentRepository
				.GetByIdsAsync(requests.Select(x => x.Id), default, true, token, [nameof(Document.Extension), nameof(Document.Data)])
				.ConfigureAwait(false);

			if (documents.Any().IsFalse())
				// TODO: NotFound
				throw new InvalidDataException();

			foreach (Document document in documents)
			{
				DocumentUpdateRequest request = requests.Single(x => x.Id.Equals(document.Id));

				_ = await PrepareDocumentForUpdate(document, request, token)
					.ConfigureAwait(false);
			}

			_ = await repositoryService.CommitChangesAsync(token)
				.ConfigureAwait(false);

			return Result.Updated;
		}
		catch (Exception ex)
		{
			loggerService.Log(LogExceptionWithParams, requests.ToJson(), ex);
			// TODO: Failed
			throw new InvalidDataException();
		}
	}

	private async Task<Document> PrepareDocumentForCreate(Guid userId, DocumentCreateRequest request, CancellationToken token)
	{
		Data data = await PrepareDocumentData(request, token)
			.ConfigureAwait(false);

		Extension extension = await PrepareDocumentExtension(request, token)
			.ConfigureAwait(false);

		Document document = mapper.Map<Document>(request);
		document.UserId = userId;
		document.Extension = extension;
		document.Data = data;

		return document;
	}

	private async Task<Document> PrepareDocumentForUpdate(Document document, DocumentUpdateRequest request, CancellationToken token)
	{
		_ = mapper.Map(request, document);

		Data data = await PrepareDocumentData(request, token)
			.ConfigureAwait(false);

		if (document.Data.Id.Equals(data.Id).IsFalse())
			document.Data = data;

		Extension extension = await PrepareDocumentExtension(request, token)
			.ConfigureAwait(false);

		if (document.Extension.Id.Equals(extension.Id).IsFalse())
			document.Extension = extension;

		return document;
	}

	private async Task<Extension> PrepareDocumentExtension(DocumentBaseRequest request, CancellationToken token)
	{
		Extension? extension = await repositoryService.DocumentExtensionRepository
			.GetByConditionAsync(x => x.Name == request.ExtensionName, token: token)
			.ConfigureAwait(false);

		extension ??= new()
		{
			Name = request.ExtensionName,
			MimeType = MimeTypesMap.GetMimeType($"{request.Name}.{request.ExtensionName}")
		};

		return extension;
	}

	private async Task<Data> PrepareDocumentData(DocumentBaseRequest request, CancellationToken token)
	{
		byte[] md5Hash = request.Content.GetMD5();

		Data? data = await repositoryService.DocumentDataRepository
			.GetByConditionAsync(x => x.MD5Hash.SequenceEqual(md5Hash), token: token)
			.ConfigureAwait(false);

		data ??= new()
		{
			MD5Hash = md5Hash,
			Length = request.Content.LongLength,
			Content = request.Content
		};

		return data;
	}
}
