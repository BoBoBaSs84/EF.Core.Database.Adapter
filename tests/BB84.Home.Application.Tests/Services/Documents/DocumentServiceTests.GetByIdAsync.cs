using BB84.Home.Application.Contracts.Responses.Documents;
using BB84.Home.Application.Errors.Services;
using BB84.Home.Application.Interfaces.Infrastructure.Persistence.Repositories.Documents;
using BB84.Home.Base.Tests.Helpers;
using BB84.Home.Domain.Entities.Documents;
using BB84.Home.Domain.Errors;

using FluentAssertions;

using Microsoft.Extensions.Logging;

using Moq;

namespace ApplicationTests.Services.Documents;

public sealed partial class DocumentServiceTests
{
	[TestMethod]
	public async Task GetByIdAsyncShouldReturnFailedWhenExcpetionIsThrown()
	{
		Guid id = Guid.NewGuid();
		CancellationToken token = CancellationToken.None;

		ErrorOr<DocumentResponse> result = await _sut
			.GetByIdAsync(id, token)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(DocumentServiceErrors.GetByIdFailed(id));
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), $"{id}", It.IsAny<Exception>()), Times.Once);
		});
	}

	[TestMethod]
	public async Task GetByIdAsyncShouldReturnNotFoundWhenNotFound()
	{
		Guid id = Guid.NewGuid();
		CancellationToken token = CancellationToken.None;
		Mock<IDocumentRepository> docRepoMock = new();
		string[] includes = [nameof(DocumentEntity.Data), nameof(DocumentEntity.Extension)];
		docRepoMock.Setup(x => x.GetByIdAsync(id, default, default, token, includes))
			.Returns(Task.FromResult<DocumentEntity?>(null));
		_repositoryServiceMock.Setup(x => x.DocumentRepository)
			.Returns(docRepoMock.Object);

		ErrorOr<DocumentResponse> result = await _sut
			.GetByIdAsync(id, token)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(DocumentServiceErrors.GetByIdNotFound(id));
			docRepoMock.Verify(x => x.GetByIdAsync(id, default, default, token, includes), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), It.IsAny<object>(), It.IsAny<Exception>()), Times.Never);
		});
	}

	[TestMethod]
	public async Task GetByIdAsyncShouldReturnResultWhenSuccessful()
	{
		Guid id = Guid.NewGuid();
		CancellationToken token = CancellationToken.None;
		Mock<IDocumentRepository> docRepoMock = new();
		string[] includes = [nameof(DocumentEntity.Data), nameof(DocumentEntity.Extension)];
		DocumentEntity document = CreateDocument(id);
		docRepoMock.Setup(x => x.GetByIdAsync(id, default, default, token, includes))
			.Returns(Task.FromResult<DocumentEntity?>(document));
		_repositoryServiceMock.Setup(x => x.DocumentRepository)
			.Returns(docRepoMock.Object);

		ErrorOr<DocumentResponse> result = await _sut
			.GetByIdAsync(id, token)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Content?.SequenceEqual(document.Data.Content).Should().BeTrue();
			result.Value.CreationTime.Should().Be(document.CreationTime);
			result.Value.Directory.Should().Be(document.Directory);
			result.Value.ExtensionName.Should().Be(document.Extension.Name);
			result.Value.Flags.Should().Be(document.Flags);
			result.Value.Id.Should().Be(document.Id);
			result.Value.LastAccessTime.Should().Be(document.LastAccessTime);
			result.Value.LastWriteTime.Should().Be(document.LastWriteTime);
			result.Value.Length.Should().Be(document.Data.Length);
			result.Value.MD5Hash?.SequenceEqual(document.Data.MD5Hash).Should().BeTrue();
			result.Value.MimeType.Should().Be(document.Extension.MimeType);
			result.Value.Name.Should().Be(document.Name);
			docRepoMock.Verify(x => x.GetByIdAsync(id, default, default, token, includes), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), It.IsAny<object>(), It.IsAny<Exception>()), Times.Never);
		});
	}
}
