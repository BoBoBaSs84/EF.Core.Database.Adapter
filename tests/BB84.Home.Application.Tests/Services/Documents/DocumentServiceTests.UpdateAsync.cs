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
	public async Task UpdateAsyncShouldReturnFailedWhenExceptionIsThrown()
	{
		CancellationToken token = CancellationToken.None;
		DocumentUpdateRequest request = RequestHelper.GetDocumentUpdateRequest();
		string parameter = request.ToJson();

		ErrorOr<Updated> result = await _sut
			.UpdateAsync(request, token)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(DocumentServiceErrors.UpdateByIdFailed(request.Id));
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), parameter, It.IsAny<Exception>()), Times.Once);
		});
	}

	[TestMethod]
	public async Task UpdateAsyncShouldReturnNotFoundWhenNotFound()
	{
		CancellationToken token = CancellationToken.None;
		DocumentUpdateRequest request = RequestHelper.GetDocumentUpdateRequest();
		string[] includes = [nameof(DocumentEntity.Extension), nameof(DocumentEntity.Data)];
		Mock<IDocumentRepository> docRepoMock = new();
		docRepoMock.Setup(x => x.GetByIdAsync(request.Id, default, true, token, includes))
			.Returns(Task.FromResult<DocumentEntity?>(null));
		_repositoryServiceMock.Setup(x => x.DocumentRepository)
			.Returns(docRepoMock.Object);

		ErrorOr<Updated> result = await _sut
			.UpdateAsync(request, token)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(DocumentServiceErrors.UpdateByIdNotFound(request.Id));
			docRepoMock.Verify(x => x.GetByIdAsync(request.Id, default, true, token, includes), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), It.IsAny<object>(), It.IsAny<Exception>()), Times.Never);
		});
	}

	[TestMethod]
	public async Task UpdateAsyncShouldReturnUpdatedWhenSuccessful()
	{
		CancellationToken token = CancellationToken.None;
		DocumentUpdateRequest request = RequestHelper.GetDocumentUpdateRequest();
		DocumentEntity document = CreateDocument();
		string[] includes = [nameof(DocumentEntity.Extension), nameof(DocumentEntity.Data)];
		Mock<IDocumentRepository> docRepoMock = new();
		docRepoMock.Setup(x => x.GetByIdAsync(request.Id, default, true, token, includes))
			.Returns(Task.FromResult<DocumentEntity?>(document));
		Mock<IDocumentExtensionRepository> extRepoMock = new();
		extRepoMock.Setup(x => x.GetByConditionAsync(x => x.Name == request.ExtensionName, default, default, default, token))
			.Returns(Task.FromResult<ExtensionEntity?>(null));
		byte[] md5Hash = request.Content.GetMD5();
		Mock<IDocumentDataRepository> dataRepoMock = new();
		dataRepoMock.Setup(x => x.GetByConditionAsync(x => x.MD5Hash.SequenceEqual(md5Hash), default, default, default, token))
			.Returns(Task.FromResult<DataEntity?>(null));
		_repositoryServiceMock.Setup(x => x.DocumentRepository)
			.Returns(docRepoMock.Object);
		_repositoryServiceMock.Setup(x => x.DocumentDataRepository)
			.Returns(dataRepoMock.Object);
		_repositoryServiceMock.Setup(x => x.DocumentExtensionRepository)
			.Returns(extRepoMock.Object);

		ErrorOr<Updated> result = await _sut
			.UpdateAsync(request, token)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().Be(Result.Updated);
			docRepoMock.Verify(x => x.GetByIdAsync(request.Id, default, true, token, includes), Times.Once);
			extRepoMock.Verify(x => x.GetByConditionAsync(x => x.Name == request.ExtensionName, default, default, default, token), Times.Once());
			_repositoryServiceMock.Verify(x => x.CommitChangesAsync(token), Times.Once());
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), It.IsAny<object>(), It.IsAny<Exception>()), Times.Never);
		});
	}
}
