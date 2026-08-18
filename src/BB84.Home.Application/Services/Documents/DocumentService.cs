using BB84.EntityFrameworkCore.Repositories.Abstractions;
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
using BB84.Home.Application.Interfaces.Presentation.Services;
using BB84.Home.Domain.Entities.Documents;
using BB84.Home.Domain.Errors;
using BB84.Home.Domain.Results;

using Microsoft.Extensions.Logging;

namespace BB84.Home.Application.Services.Documents;

/// <summary>
/// Provides functionality for managing document records, including creation, retrieval, updating, and deletion.
/// </summary>
/// <param name="loggerService">The logger service for logging errors and information.</param>
/// <param name="userService"> The service providing information about the current user.</param>
/// <param name="repositoryService">The repository service for accessing data repositories.</param>
internal sealed class DocumentService(ILoggerService<DocumentService> loggerService, ICurrentUserService userService, IRepositoryService repositoryService) : IDocumentService
{
	private static readonly Action<ILogger, object, Exception?> LogExceptionWithParams =
		LoggerMessage.Define<object>(LogLevel.Error, 0, "Exception occured. Params = {Parameters}");

	public async Task<ErrorOr<Created>> CreateAsync(DocumentCreateRequest request, CancellationToken token = default)
	{
		try
		{
			DocumentEntity document = await PrepareDocumentForCreate(request, token)
				.ConfigureAwait(false);

			await repositoryService.DocumentRepository
				.CreateAsync(document, token)
				.ConfigureAwait(false);

			_ = await repositoryService
				.CommitChangesAsync(token)
				.ConfigureAwait(false);

			return Result.Created;
		}
		catch (Exception ex)
		{
			string[] parameters = [$"{userService.UserId}", request.ToJson()];
			loggerService.Log(LogExceptionWithParams, parameters, ex);

			string document = $"{request.Name}.{request.ExtensionName}";
			return DocumentServiceErrors.CreateFailed(document);
		}
	}

	public async Task<ErrorOr<Created>> CreateAsync(IEnumerable<DocumentCreateRequest> requests, CancellationToken token = default)
	{
		try
		{
			if (requests.Any().IsFalse())
				return DocumentServiceErrors.CreateMultipleBadRequest;

			List<DocumentEntity> documents = [];

			foreach (DocumentCreateRequest request in requests)
			{
				DocumentEntity document = await PrepareDocumentForCreate(request, token)
					.ConfigureAwait(false);

				documents.Add(document);
			}

			await repositoryService.DocumentRepository
				.CreateAsync(documents, token)
				.ConfigureAwait(false);

			_ = await repositoryService
				.CommitChangesAsync(token)
				.ConfigureAwait(false);

			return Result.Created;
		}
		catch (Exception ex)
		{
			string[] parameters = [$"{userService.UserId}", requests.ToJson()];
			loggerService.Log(LogExceptionWithParams, parameters, ex);

			IEnumerable<string> documents = requests.Select(x => $"{x.Name}.{x.ExtensionName}");
			return DocumentServiceErrors.CreateMultipleFailed(documents);
		}
	}

	public async Task<ErrorOr<Deleted>> DeleteAsync(Guid id, CancellationToken token = default)
	{
		try
		{
			DocumentEntity? document = await repositoryService.DocumentRepository
				.GetByIdAsync(id, cancellationToken: token)
				.ConfigureAwait(false);

			if (document is null)
				return DocumentServiceErrors.DeleteByIdNotFound(id);

			repositoryService.DocumentRepository
				.Delete(document);

			_ = await repositoryService
				.CommitChangesAsync(token)
				.ConfigureAwait(false);

			return Result.Deleted;
		}
		catch (Exception ex)
		{
			loggerService.Log(LogExceptionWithParams, $"{id}", ex);
			return DocumentServiceErrors.DeleteByIdFailed(id);
		}
	}

	public async Task<ErrorOr<Deleted>> DeleteAsync(IEnumerable<Guid> ids, CancellationToken token = default)
	{
		try
		{
			IEnumerable<DocumentEntity> documents = await repositoryService.DocumentRepository
				.GetByIdsAsync(ids, cancellationToken: token)
				.ConfigureAwait(false);

			if (documents.Any().IsFalse())
				return DocumentServiceErrors.DeleteByIdsNotFound(ids);

			repositoryService.DocumentRepository
				.Delete(documents);

			_ = await repositoryService
				.CommitChangesAsync(token)
				.ConfigureAwait(false);

			return Result.Deleted;
		}
		catch (Exception ex)
		{
			loggerService.Log(LogExceptionWithParams, string.Join(',', ids.Select(x => $"{x}")), ex);
			return DocumentServiceErrors.DeleteByIdsFailed(ids);
		}
	}

	public async Task<ErrorOr<DocumentResponse>> GetByIdAsync(Guid id, CancellationToken token = default)
	{
		try
		{
			DocumentEntity? document = await repositoryService.DocumentRepository
				.GetByIdAsync(id, new() { Include = [e => e.Data, e => e.Extension] }, cancellationToken: token)
				.ConfigureAwait(false);

			if (document is null)
				return DocumentServiceErrors.GetByIdNotFound(id);

			DocumentResponse response = document.ToResponse();

			return response;
		}
		catch (Exception ex)
		{
			loggerService.Log(LogExceptionWithParams, $"{id}", ex);
			return DocumentServiceErrors.GetByIdFailed(id);
		}
	}

	public async Task<ErrorOr<IPagedList<DocumentResponse>>> GetPagedByParametersAsync(DocumentParameters parameters, CancellationToken token = default)
	{
		try
		{
			Query<DocumentEntity> query = new()
			{
				QueryFilter = x => x.FilterByParameters(parameters),
				OrderBy = x => x.OrderByDescending(x => x.CreationTime),
				Skip = (parameters.PageNumber - 1) * parameters.PageSize,
				Take = parameters.PageSize
			};

			IEnumerable<DocumentEntity> documents = await repositoryService.DocumentRepository
				.GetListAsync(query, token)
				.ConfigureAwait(false);

			int totalCount = await repositoryService.DocumentRepository
				.CountAsync(new() { QueryFilter = x => x.FilterByParameters(parameters) }, token)
				.ConfigureAwait(false);

			IEnumerable<DocumentResponse> result = documents.Select(x => x.ToResponse());

			return new PagedList<DocumentResponse>(result, totalCount, parameters.PageNumber, parameters.PageSize);
		}
		catch (Exception ex)
		{
			string[] parameter = [$"{userService.UserId}", parameters.ToJson()];
			loggerService.Log(LogExceptionWithParams, parameter, ex);
			return DocumentServiceErrors.GetPagedByParametersFailed;
		}
	}

	public async Task<ErrorOr<Updated>> UpdateAsync(DocumentUpdateRequest request, CancellationToken token = default)
	{
		try
		{
			DocumentEntity? document = await repositoryService.DocumentRepository
				.GetByIdAsync(request.Id, new() { Include = [e => e.Extension, e => e.Data], TrackChanges = true }, token)
				.ConfigureAwait(false);

			if (document is null)
				return DocumentServiceErrors.UpdateByIdNotFound(request.Id);

			document = await PrepareDocumentForUpdate(document, request, token)
				.ConfigureAwait(false);

			_ = await repositoryService
				.CommitChangesAsync(token)
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

	public async Task<ErrorOr<Updated>> UpdateAsync(IEnumerable<DocumentUpdateRequest> requests, CancellationToken token = default)
	{
		try
		{
			if (requests.Any().IsFalse())
				return DocumentServiceErrors.UpdateByIdsBadRequest;

			Query<DocumentEntity> query = new()
			{
				Where = x => requests.Select(x => x.Id).Contains(x.Id),
				Include = [x => x.Data, x => x.Extension],
				TrackChanges = true
			};

			IEnumerable<DocumentEntity> documents = await repositoryService.DocumentRepository
				.GetListAsync(query, token)
				.ConfigureAwait(false);

			if (documents.Any().IsFalse())
				return DocumentServiceErrors.UpdateByIdsNotFound(requests.Select(x => x.Id));

			foreach (DocumentEntity document in documents)
			{
				DocumentUpdateRequest request = requests.Single(x => x.Id.Equals(document.Id));

				_ = await PrepareDocumentForUpdate(document, request, token)
					.ConfigureAwait(false);
			}

			_ = await repositoryService
				.CommitChangesAsync(token)
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

	private async Task<DocumentEntity> PrepareDocumentForCreate(DocumentCreateRequest request, CancellationToken token)
	{
		DataEntity data = await PrepareDocumentData(request, token)
			.ConfigureAwait(false);

		ExtensionEntity extension = await PrepareDocumentExtension(request, token)
			.ConfigureAwait(false);

		DocumentEntity document = request.ToEntity();
		document.UserId = userService.UserId;
		document.Extension = extension;
		document.Data = data;

		return document;
	}

	private async Task<DocumentEntity> PrepareDocumentForUpdate(DocumentEntity document, DocumentUpdateRequest request, CancellationToken token)
	{
		_ = request.ToEntity(document);

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
			.GetSingleAsync(new() { Where = x => x.Name == request.ExtensionName }, token)
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
			.GetSingleAsync(new() { Where = x => x.MD5Hash.SequenceEqual(md5Hash) }, token)
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
