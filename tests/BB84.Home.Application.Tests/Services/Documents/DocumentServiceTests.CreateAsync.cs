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
	public async Task CreateAsyncShouldReturnFailedWhenExceptionIsThrown()
	{
		Guid userId = Guid.NewGuid();
		CancellationToken token = CancellationToken.None;
		DocumentCreateRequest request = RequestHelper.GetDocumentCreateRequest();
		string[] parameters = [$"{userId}", $"{request.ToJson()}"];
		_currentUserService.Setup(x => x.UserId)
			.Returns(userId);

		ErrorOr<Created> result = await _sut
			.CreateAsync(request, token)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(DocumentServiceErrors.CreateFailed($"{request.Name}.{request.ExtensionName}"));
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), parameters, It.IsAny<Exception>()), Times.Once);
		});
	}

	[TestMethod]
	public async Task CreateAsyncShouldReturnCreatedWhenSuccessful()
	{
		Guid userId = Guid.NewGuid();
		CancellationToken token = CancellationToken.None;
		DocumentCreateRequest request = RequestHelper.GetDocumentCreateRequest();
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
		_repositoryServiceMock.Setup(x => x.CommitChangesAsync(token))
			.Returns(Task.FromResult(1));

		ErrorOr<Created> result = await _sut
			.CreateAsync(request, token)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().Be(Result.Created);
			extRepoMock.Verify(x => x.GetByConditionAsync(x => x.Name == request.ExtensionName, default, default, default, token), Times.Once());
			//dataRepoMock.Verify(x => x.GetByConditionAsync(x => x.MD5Hash.SequenceEqual(md5Hash), default, default, default, default), Times.Once());
			docRepoMock.Verify(x => x.CreateAsync(It.IsAny<DocumentEntity>(), token), Times.Once());
			_repositoryServiceMock.Verify(x => x.CommitChangesAsync(token), Times.Once());
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), It.IsAny<object>(), It.IsAny<Exception>()), Times.Never);
		});
	}
}
