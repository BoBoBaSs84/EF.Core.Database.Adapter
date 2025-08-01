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
	public async Task DeleteByIdsShouldReturnFailedWhenExceptionIsThrown()
	{
		Guid userId = Guid.NewGuid();
		CancellationToken token = CancellationToken.None;
		IEnumerable<Guid> ids = [Guid.NewGuid()];
		string parameter = string.Join(',', ids.Select(x => $"{x}"));

		ErrorOr<Deleted> result = await _sut
			.DeleteAsync(ids, token)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(DocumentServiceErrors.DeleteByIdsFailed(ids));
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), parameter, It.IsAny<Exception>()), Times.Once);
		});
	}

	[TestMethod]
	public async Task DeleteByIdsShouldReturnNotFoundWhenNotFound()
	{
		Guid userId = Guid.NewGuid();
		CancellationToken token = CancellationToken.None;
		IEnumerable<Guid> ids = [Guid.NewGuid()];
		Mock<IDocumentRepository> docRepoMock = new();
		docRepoMock.Setup(x => x.GetManyByConditionAsync(x => ids.Contains(x.Id), default, default, default, default, default, default, token))
			.Returns(Task.FromResult<IEnumerable<DocumentEntity>>([]));
		_repositoryServiceMock.Setup(x => x.DocumentRepository)
			.Returns(docRepoMock.Object);

		ErrorOr<Deleted> result = await _sut
			.DeleteAsync(ids, token)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(DocumentServiceErrors.DeleteByIdsNotFound(ids));
			docRepoMock.Verify(x => x.GetManyByConditionAsync(x => ids.Contains(x.Id), default, default, default, default, default, default, token), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), It.IsAny<object>(), It.IsAny<Exception>()), Times.Never);
		});
	}

	[TestMethod]
	public async Task DeleteByIdsShouldReturnDeletedWhenSuccessful()
	{
		Guid userId = Guid.NewGuid();
		CancellationToken token = CancellationToken.None;
		IEnumerable<Guid> ids = [Guid.NewGuid()];
		IEnumerable<DocumentEntity> documents = [CreateDocument()];
		Mock<IDocumentRepository> docRepoMock = new();
		docRepoMock.Setup(x => x.GetManyByConditionAsync(x => ids.Contains(x.Id), default, default, default, default, default, default, token))
			.Returns(Task.FromResult(documents));
		_repositoryServiceMock.Setup(x => x.DocumentRepository)
			.Returns(docRepoMock.Object);

		ErrorOr<Deleted> result = await _sut
			.DeleteAsync(ids, token)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().Be(Result.Deleted);
			docRepoMock.Verify(x => x.GetManyByConditionAsync(x => ids.Contains(x.Id), default, default, default, default, default, default, token), Times.Once);
			docRepoMock.Verify(x => x.DeleteAsync(documents, token), Times.Once);
			_repositoryServiceMock.Verify(x => x.CommitChangesAsync(token), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), It.IsAny<object>(), It.IsAny<Exception>()), Times.Never);
		});
	}
}
