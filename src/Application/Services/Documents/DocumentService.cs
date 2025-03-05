using AutoMapper;

using BB84.Extensions;
using BB84.Extensions.Serialization;
using BB84.Home.Application.Common;
using BB84.Home.Application.Contracts.Requests.Documents;
using BB84.Home.Application.Contracts.Requests.Documents.Base;
using BB84.Home.Application.Contracts.Responses.Documents;
using BB84.Home.Application.Errors.Services;
using BB84.Home.Application.Extensions;
using BB84.Home.Application.Features.Requests;
using BB84.Home.Application.Features.Responses;
using BB84.Home.Application.Interfaces.Application.Services.Documents;
using BB84.Home.Application.Interfaces.Infrastructure.Services;
using BB84.Home.Domain.Entities.Documents;
using BB84.Home.Domain.Errors;
using BB84.Home.Domain.Results;

using Microsoft.Extensions.Logging;

namespace BB84.Home.Application.Services.Documents;

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
			DocumentEntity document = await PrepareDocumentForCreate(userId, request, token)
				.ConfigureAwait(false);

			await repositoryService.DocumentRepository.CreateAsync(document, token)
				.ConfigureAwait(false);

			_ = await repositoryService.CommitChangesAsync(token)
				.ConfigureAwait(false);

			return Result.Created;
		}
		catch (Exception ex)
		{
			string[] parameters = [$"{userId}", request.ToJson()];
			loggerService.Log(LogExceptionWithParams, parameters, ex);

			string document = $"{request.Name}.{request.ExtensionName}";
			return DocumentServiceErrors.CreateFailed(document);
		}
	}

	public async Task<ErrorOr<Created>> Create(Guid userId, IEnumerable<DocumentCreateRequest> requests, CancellationToken token = default)
	{
		try
		{
			if (requests.Any().IsFalse())
				return DocumentServiceErrors.CreateMultipleBadRequest;

			List<DocumentEntity> documents = [];

			foreach (DocumentCreateRequest request in requests)
			{
				DocumentEntity document = await PrepareDocumentForCreate(userId, request, token)
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
			string[] parameters = [$"{userId}", requests.ToJson()];
			loggerService.Log(LogExceptionWithParams, parameters, ex);

			IEnumerable<string> documents = requests.Select(x => $"{x.Name}.{x.ExtensionName}");
			return DocumentServiceErrors.CreateMultipleFailed(documents);
		}
	}

	public async Task<ErrorOr<Deleted>> DeleteById(Guid id, CancellationToken token = default)
	{
		try
		{
			DocumentEntity? document = await repositoryService.DocumentRepository
				.GetByIdAsync(id, token: token)
				.ConfigureAwait(false);

			if (document is null)
				return DocumentServiceErrors.DeleteByIdNotFound(id);

			await repositoryService.DocumentRepository.DeleteAsync(document, token)
				.ConfigureAwait(false);

			_ = await repositoryService.CommitChangesAsync(token)
				.ConfigureAwait(false);

			return Result.Deleted;
		}
		catch (Exception ex)
		{
			loggerService.Log(LogExceptionWithParams, $"{id}", ex);
			return DocumentServiceErrors.DeleteByIdFailed(id);
		}
	}

	public async Task<ErrorOr<Deleted>> DeleteByIds(Guid userId, IEnumerable<Guid> ids, CancellationToken token = default)
	{
		try
		{
			IEnumerable<DocumentEntity> documents = await repositoryService.DocumentRepository
				.GetManyByConditionAsync(
					expression: x => x.UserId.Equals(userId) && ids.Contains(x.Id),
					token: token)
				.ConfigureAwait(false);

			if (documents.Any().IsFalse())
				return DocumentServiceErrors.DeleteByIdsNotFound(ids);

			await repositoryService.DocumentRepository.DeleteAsync(documents, token)
				.ConfigureAwait(false);

			_ = await repositoryService.CommitChangesAsync(token)
				.ConfigureAwait(false);

			return Result.Deleted;
		}
		catch (Exception ex)
		{
			loggerService.Log(LogExceptionWithParams, string.Join(',', ids.Select(x => $"{x}")), ex);
			return DocumentServiceErrors.DeleteByIdsFailed(ids);
		}
	}

	public async Task<ErrorOr<DocumentResponse>> GetById(Guid id, CancellationToken token = default)
	{
		try
		{
			DocumentEntity? document = await repositoryService.DocumentRepository
				.GetByIdAsync(id, token: token, includeProperties: [nameof(DocumentEntity.Data), nameof(DocumentEntity.Extension)])
				.ConfigureAwait(false);

			if (document is null)
				return DocumentServiceErrors.GetByIdNotFound(id);

			DocumentResponse response = mapper.Map<DocumentResponse>(document);

			return response;
		}
		catch (Exception ex)
		{
			loggerService.Log(LogExceptionWithParams, $"{id}", ex);
			return DocumentServiceErrors.GetByIdFailed(id);
		}
	}

	public async Task<ErrorOr<IPagedList<DocumentResponse>>> GetPagedByParameters(Guid userId, DocumentParameters parameters, CancellationToken token = default)
	{
		try
		{
			IEnumerable<DocumentEntity> documents = await repositoryService.DocumentRepository
				.GetManyByConditionAsync(
					expression: x => x.UserId.Equals(userId),
					queryFilter: x => x.FilterByParameters(parameters),
					orderBy: x => x.OrderByDescending(x => x.CreationTime),
					skip: (parameters.PageNumber - 1) * parameters.PageSize,
					take: parameters.PageSize,
					token: token)
				.ConfigureAwait(false);

			int totalCount = await repositoryService.DocumentRepository
				.CountAsync(
					expression: x => x.UserId.Equals(userId),
					queryFilter: x => x.FilterByParameters(parameters),
					token: token)
				.ConfigureAwait(false);

			IEnumerable<DocumentResponse> result = mapper.Map<IEnumerable<DocumentResponse>>(documents);

			return new PagedList<DocumentResponse>(result, totalCount, parameters.PageNumber, parameters.PageSize);
		}
		catch (Exception ex)
		{
			string[] parameter = [$"{userId}", parameters.ToJson()];
			loggerService.Log(LogExceptionWithParams, parameter, ex);
			return DocumentServiceErrors.GetPagedByParametersFailed;
		}
	}

	public async Task<ErrorOr<Updated>> Update(DocumentUpdateRequest request, CancellationToken token = default)
	{
		try
		{
			DocumentEntity? document = await repositoryService.DocumentRepository
				.GetByIdAsync(request.Id, default, true, token, [nameof(DocumentEntity.Extension), nameof(DocumentEntity.Data)])
				.ConfigureAwait(false);

			if (document is null)
				return DocumentServiceErrors.UpdateByIdNotFound(request.Id);

			document = await PrepareDocumentForUpdate(document, request, token)
				.ConfigureAwait(false);

			_ = await repositoryService.CommitChangesAsync(token)
				.ConfigureAwait(false);

			return Result.Updated;
		}
		catch (Exception ex)
		{
			string parameter = request.ToJson();
			loggerService.Log(LogExceptionWithParams, parameter, ex);
			return DocumentServiceErrors.UpdateByIdFailed(request.Id);
		}
	}

	public async Task<ErrorOr<Updated>> Update(Guid userId, IEnumerable<DocumentUpdateRequest> requests, CancellationToken token = default)
	{
		try
		{
			if (requests.Any().IsFalse())
				return DocumentServiceErrors.UpdateByIdsBadRequest;

			IEnumerable<DocumentEntity> documents = await repositoryService.DocumentRepository
				.GetManyByConditionAsync(
					expression: x => x.UserId.Equals(userId) && requests.Select(x => x.Id).Contains(x.Id),
					trackChanges: true,
					token: token,
					includeProperties: [nameof(DocumentEntity.Extension), nameof(DocumentEntity.Data)])
				.ConfigureAwait(false);

			if (documents.Any().IsFalse())
				return DocumentServiceErrors.UpdateByIdsNotFound(requests.Select(x => x.Id));

			foreach (DocumentEntity document in documents)
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
			string parameter = requests.ToJson();
			loggerService.Log(LogExceptionWithParams, parameter, ex);
			return DocumentServiceErrors.UpdateByIdsFailed(requests.Select(x => x.Id));
		}
	}

	private async Task<DocumentEntity> PrepareDocumentForCreate(Guid userId, DocumentCreateRequest request, CancellationToken token)
	{
		DataEntity data = await PrepareDocumentData(request, token)
			.ConfigureAwait(false);

		ExtensionEntity extension = await PrepareDocumentExtension(request, token)
			.ConfigureAwait(false);

		DocumentEntity document = mapper.Map<DocumentEntity>(request);
		document.UserId = userId;
		document.Extension = extension;
		document.Data = data;

		return document;
	}

	private async Task<DocumentEntity> PrepareDocumentForUpdate(DocumentEntity document, DocumentUpdateRequest request, CancellationToken token)
	{
		_ = mapper.Map(request, document);

		DataEntity data = await PrepareDocumentData(request, token)
			.ConfigureAwait(false);

		if (document.Data.Id.Equals(data.Id).IsFalse())
			document.Data = data;

		ExtensionEntity extension = await PrepareDocumentExtension(request, token)
			.ConfigureAwait(false);

		if (document.Extension.Id.Equals(extension.Id).IsFalse())
			document.Extension = extension;

		return document;
	}

	private async Task<ExtensionEntity> PrepareDocumentExtension(DocumentBaseRequest request, CancellationToken token)
	{
		ExtensionEntity? extension = await repositoryService.DocumentExtensionRepository
			.GetByConditionAsync(x => x.Name == request.ExtensionName, token: token)
			.ConfigureAwait(false);

		extension ??= new()
		{
			Name = request.ExtensionName,
			MimeType = MimeTypesMap.GetMimeType($"{request.Name}.{request.ExtensionName}")
		};

		return extension;
	}

	private async Task<DataEntity> PrepareDocumentData(DocumentBaseRequest request, CancellationToken token)
	{
		byte[] md5Hash = request.Content.GetMD5();

		DataEntity? data = await repositoryService.DocumentDataRepository
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
