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
	public async Task CreateMultipleAsyncShouldReturnFailedWhenExceptionIsThrown()
	{
		Guid userId = Guid.NewGuid();
		CancellationToken token = CancellationToken.None;
		IEnumerable<DocumentCreateRequest> requests = [RequestHelper.GetDocumentCreateRequest()];
		string[] parameters = [$"{userId}", $"{requests.ToJson()}"];
		IEnumerable<string> documents = requests.Select(x => $"{x.Name}.{x.ExtensionName}");

		ErrorOr<Created> result = await _sut
			.CreateAsync(requests, token)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(DocumentServiceErrors.CreateMultipleFailed(documents));
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), parameters, It.IsAny<Exception>()), Times.Once);
		});
	}

	[TestMethod]
	public async Task CreateMultipleAsyncShouldReturnBadRequestWhenBodyIsEmpty()
	{
		Guid userId = Guid.NewGuid();
		CancellationToken token = CancellationToken.None;
		IEnumerable<DocumentCreateRequest> requests = [];

		ErrorOr<Created> result = await _sut
			.CreateAsync(requests, token)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(DocumentServiceErrors.CreateMultipleBadRequest);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), It.IsAny<object>(), It.IsAny<Exception>()), Times.Never);
		});
	}

	[TestMethod]
	public async Task CreateMultipleAsyncShouldReturnCreatedWhenSuccessful()
	{
		Guid userId = Guid.NewGuid();
		CancellationToken token = CancellationToken.None;
		DocumentCreateRequest request = RequestHelper.GetDocumentCreateRequest();
		IEnumerable<DocumentCreateRequest> requests = [request];
		byte[] md5Hash = request.Content.GetMD5();
		Mock<IDocumentExtensionRepository> extRepoMock = new();
		extRepoMock.Setup(x => x.GetByConditionAsync(x => x.Name == request.ExtensionName, default, default, default, token))
			.Returns(Task.FromResult<ExtensionEntity?>(null));
		Mock<IDocumentDataRepository> dataRepoMock = new();
		dataRepoMock.Setup(x => x.GetByConditionAsync(x => x.MD5Hash.SequenceEqual(md5Hash), default, default, default, token))
			.Returns(Task.FromResult<DataEntity?>(null));
		Mock<IDocumentRepository> docRepoMock = new();
		_repositoryServiceMock.Setup(x => x.DocumentExtensionRepository)
			.Returns(extRepoMock.Object);
		_repositoryServiceMock.Setup(x => x.DocumentDataRepository)
			.Returns(dataRepoMock.Object);
		_repositoryServiceMock.Setup(x => x.DocumentRepository)
			.Returns(docRepoMock.Object);

		ErrorOr<Created> result = await _sut
			.CreateAsync(requests, token)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().Be(Result.Created);
			extRepoMock.Verify(x => x.GetByConditionAsync(x => x.Name == request.ExtensionName, default, default, default, token), Times.Once());
			//dataRepoMock.Verify(x => x.GetByConditionAsync(x => x.MD5Hash.SequenceEqual(md5Hash), default, default, default, default), Times.Once());
			docRepoMock.Verify(x => x.CreateAsync(It.IsAny<IEnumerable<DocumentEntity>>(), default), Times.Once());
			_repositoryServiceMock.Verify(x => x.CommitChangesAsync(default), Times.Once());
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), It.IsAny<object>(), It.IsAny<Exception>()), Times.Never);
		});
	}
}
