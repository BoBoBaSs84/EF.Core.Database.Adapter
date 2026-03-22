using BB84.Home.Application.Errors.Services;
using BB84.Home.Application.Interfaces.Infrastructure.Persistence.Repositories.Documents;
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
	public async Task DeleteAsyncShouldReturnFailedWhenExceptionIsThrown()
	{
		Guid id = Guid.NewGuid();

		ErrorOr<Deleted> result = await _sut
			.DeleteAsync(id, _cancellationToken)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(DocumentServiceErrors.DeleteByIdFailed(id));
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), $"{id}", It.IsAny<Exception>()), Times.Once);
		});
	}

	[TestMethod]
	public async Task DeleteAsyncShouldReturnNotFoundWhenNotFound()
	{
		Guid id = Guid.NewGuid();
		Mock<IDocumentRepository> docRepoMock = new();
		docRepoMock.Setup(x => x.GetByIdAsync(id, default, default, _cancellationToken))
			.Returns(Task.FromResult<DocumentEntity?>(null));
		_repositoryServiceMock.Setup(x => x.DocumentRepository)
			.Returns(docRepoMock.Object);

		ErrorOr<Deleted> result = await _sut
			.DeleteAsync(id, _cancellationToken)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(DocumentServiceErrors.DeleteByIdNotFound(id));
			docRepoMock.Verify(x => x.GetByIdAsync(id, default, default, _cancellationToken), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), It.IsAny<object>(), It.IsAny<Exception>()), Times.Never);
		});
	}

	[TestMethod]
	public async Task DeleteAsyncShouldReturnDeletedWhenSuccessful()
	{
		Guid id = Guid.NewGuid();
		DocumentEntity document = CreateDocument(id);
		Mock<IDocumentRepository> docRepoMock = new();
		docRepoMock.Setup(x => x.GetByIdAsync(id, default, default, _cancellationToken))
			.Returns(Task.FromResult<DocumentEntity?>(document));
		_repositoryServiceMock.Setup(x => x.DocumentRepository)
			.Returns(docRepoMock.Object);

		ErrorOr<Deleted> result = await _sut
			.DeleteAsync(id, _cancellationToken)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().Be(Result.Deleted);
			docRepoMock.Verify(x => x.GetByIdAsync(id, default, default, _cancellationToken), Times.Once);
			docRepoMock.Verify(x => x.DeleteAsync(document, _cancellationToken), Times.Once);
			_repositoryServiceMock.Verify(x => x.CommitChangesAsync(default), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), It.IsAny<object>(), It.IsAny<Exception>()), Times.Never);
		});
	}
}
