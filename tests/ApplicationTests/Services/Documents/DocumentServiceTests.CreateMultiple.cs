using BaseTests.Helpers;

using BB84.Extensions;
using BB84.Extensions.Serialization;
using BB84.Home.Application.Contracts.Requests.Documents;
using BB84.Home.Application.Errors.Services;
using BB84.Home.Application.Interfaces.Infrastructure.Persistence.Repositories.Documents;
using BB84.Home.Application.Services.Documents;
using BB84.Home.Application.Tests.Helpers;
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
	[TestCategory(nameof(DocumentService.Create))]
	public async Task CreateMultipleShouldReturnFailedWhenExceptionIsThrown()
	{
		Guid userId = Guid.NewGuid();
		IEnumerable<DocumentCreateRequest> requests = [RequestHelper.GetDocumentCreateRequest()];
		string[] parameters = [$"{userId}", $"{requests.ToJson()}"];
		IEnumerable<string> documents = requests.Select(x => $"{x.Name}.{x.ExtensionName}");
		DocumentService sut = CreateMockedInstance();

		ErrorOr<Created> result = await sut.Create(userId, requests)
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
	[TestCategory(nameof(DocumentService.Create))]
	public async Task CreateMultipleShouldReturnBadRequestWhenBodyIsEmpty()
	{
		Guid userId = Guid.NewGuid();
		IEnumerable<DocumentCreateRequest> requests = [];
		DocumentService sut = CreateMockedInstance();

		ErrorOr<Created> result = await sut.Create(userId, requests)
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
	[TestCategory(nameof(DocumentService.Create))]
	public async Task CreateMultipleShouldReturnCreatedWhenSuccessful()
	{
		Guid userId = Guid.NewGuid();
		DocumentCreateRequest request = RequestHelper.GetDocumentCreateRequest();
		IEnumerable<DocumentCreateRequest> requests = [request];
		byte[] md5Hash = request.Content.GetMD5();
		Mock<IDocumentExtensionRepository> extRepoMock = new();
		extRepoMock.Setup(x => x.GetByConditionAsync(x => x.Name == request.ExtensionName, default, default, default, default))
			.Returns(Task.FromResult<ExtensionEntity?>(null));
		Mock<IDocumentDataRepository> dataRepoMock = new();
		dataRepoMock.Setup(x => x.GetByConditionAsync(x => x.MD5Hash.SequenceEqual(md5Hash), default, default, default, default))
			.Returns(Task.FromResult<DataEntity?>(null));
		Mock<IDocumentRepository> docRepoMock = new();
		DocumentService sut = CreateMockedInstance(docRepoMock.Object, extRepoMock.Object, dataRepoMock.Object);

		ErrorOr<Created> result = await sut.Create(userId, requests)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().Be(Result.Created);
			extRepoMock.Verify(x => x.GetByConditionAsync(x => x.Name == request.ExtensionName, default, default, default, default), Times.Once());
			//dataRepoMock.Verify(x => x.GetByConditionAsync(x => x.MD5Hash.SequenceEqual(md5Hash), default, default, default, default), Times.Once());
			docRepoMock.Verify(x => x.CreateAsync(It.IsAny<IEnumerable<DocumentEntity>>(), default), Times.Once());
			_repositoryServiceMock.Verify(x => x.CommitChangesAsync(default), Times.Once());
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), It.IsAny<object>(), It.IsAny<Exception>()), Times.Never);
		});
	}
}
