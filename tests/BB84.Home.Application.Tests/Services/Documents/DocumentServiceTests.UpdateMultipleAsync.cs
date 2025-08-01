using BB84.Extensions;
using BB84.Extensions.Serialization;
using BB84.Home.Application.Contracts.Requests.Documents;
using BB84.Home.Application.Errors.Services;
using BB84.Home.Application.Interfaces.Infrastructure.Persistence.Repositories.Documents;
using BB84.Home.Application.Tests.Helpers;
using BB84.Home.Base.Tests.Helpers;
using BB84.Home.Domain.Entities.Documents;
using BB84.Home.Domain.Errors;
using BB84.Home.Domain.Results;

using FluentAssertions;

using Microsoft.Extensions.Logging;

using Moq;

namespace ApplicationTests.Services.Documents;

public sealed partial class DocumentServiceTests
{
	[TestMethod]
	public async Task UpdateMultipleAsyncShouldReturnFailedWhenExceptionIsThrown()
	{		
		CancellationToken token = CancellationToken.None;
		IEnumerable<DocumentUpdateRequest> requests = [RequestHelper.GetDocumentUpdateRequest()];
		string parameter = requests.ToJson();

		ErrorOr<Updated> result = await _sut
			.UpdateAsync(requests, token)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(DocumentServiceErrors.UpdateByIdsFailed(requests.Select(x => x.Id)));
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), parameter, It.IsAny<Exception>()), Times.Once);
		});
	}

	[TestMethod]
	public async Task UpdateMultipleAsyncShouldReturnBadRequestWhenBodyIsEmpty()
	{
		CancellationToken token = CancellationToken.None;
		IEnumerable<DocumentUpdateRequest> requests = [];

		ErrorOr<Updated> result = await _sut
			.UpdateAsync(requests, token)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(DocumentServiceErrors.UpdateByIdsBadRequest);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), It.IsAny<object>(), It.IsAny<Exception>()), Times.Never);
		});
	}

	[TestMethod]
	public async Task UpdateMultipleAsyncShouldReturnNotFoundWhenNotFound()
	{
		CancellationToken token = CancellationToken.None;
		IEnumerable<DocumentUpdateRequest> requests = [RequestHelper.GetDocumentUpdateRequest()];
		string[] includes = [nameof(DocumentEntity.Extension), nameof(DocumentEntity.Data)];
		Mock<IDocumentRepository> docRepoMock = new();
		docRepoMock.Setup(x => x.GetManyByConditionAsync(x => requests.Select(x => x.Id).Contains(x.Id), default, default, default, default, default, true, token, includes))
			.Returns(Task.FromResult<IEnumerable<DocumentEntity>>([]));
		_repositoryServiceMock.Setup(x => x.DocumentRepository)
			.Returns(docRepoMock.Object);

		ErrorOr<Updated> result = await _sut
			.UpdateAsync(requests, token)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(DocumentServiceErrors.UpdateByIdsNotFound(requests.Select(x => x.Id)));
			docRepoMock.Verify(x => x.GetManyByConditionAsync(x => requests.Select(x => x.Id).Contains(x.Id), default, default, default, default, default, true, token, includes), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), It.IsAny<object>(), It.IsAny<Exception>()), Times.Never);
		});
	}

	[TestMethod]
	public async Task UpdateMultipleAsyncShouldReturnUpdatedWhenSuccessful()
	{
		CancellationToken token = CancellationToken.None;
		DocumentUpdateRequest request = RequestHelper.GetDocumentUpdateRequest();
		DocumentEntity document = CreateDocument(request.Id);
		IEnumerable<DocumentUpdateRequest> requests = [request];
		string[] includes = [nameof(DocumentEntity.Extension), nameof(DocumentEntity.Data)];
		Mock<IDocumentRepository> docRepoMock = new();
		docRepoMock.Setup(x => x.GetManyByConditionAsync(x => requests.Select(x => x.Id).Contains(x.Id), default, default, default, default, default, true, token, includes))
			.Returns(Task.FromResult<IEnumerable<DocumentEntity>>([document]));
		Mock<IDocumentExtensionRepository> extRepoMock = new();
		extRepoMock.Setup(x => x.GetByConditionAsync(x => x.Name == request.ExtensionName, default, default, default, token))
			.Returns(Task.FromResult<ExtensionEntity?>(null));
		byte[] md5Hash = request.Content.GetMD5();
		Mock<IDocumentDataRepository> dataRepoMock = new();
		dataRepoMock.Setup(x => x.GetByConditionAsync(x => x.MD5Hash.SequenceEqual(md5Hash), default, default, default, token))
			.Returns(Task.FromResult<DataEntity?>(null));
		_repositoryServiceMock.Setup(x => x.DocumentRepository)
			.Returns(docRepoMock.Object);
		_repositoryServiceMock.Setup(x => x.DocumentExtensionRepository)
			.Returns(extRepoMock.Object);
		_repositoryServiceMock.Setup(x => x.DocumentDataRepository)
			.Returns(dataRepoMock.Object);
		_repositoryServiceMock.Setup(x => x.CommitChangesAsync(token))
			.Returns(Task.FromResult(1));

		ErrorOr<Updated> result = await _sut
			.UpdateAsync(requests, token)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().Be(Result.Updated);
			docRepoMock.Verify(x => x.GetManyByConditionAsync(x => requests.Select(x => x.Id).Contains(x.Id), default, default, default, default, default, true, token, includes), Times.Once);
			extRepoMock.Verify(x => x.GetByConditionAsync(x => x.Name == request.ExtensionName, default, default, default, token), Times.Once());
			_repositoryServiceMock.Verify(x => x.CommitChangesAsync(token), Times.Once());
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), It.IsAny<object>(), It.IsAny<Exception>()), Times.Never);
		});
	}
}
