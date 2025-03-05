using Application.Errors.Services;
using Application.Interfaces.Infrastructure.Persistence.Repositories.Documents;
using Application.Services.Documents;

using BaseTests.Helpers;

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
	[TestCategory(nameof(DocumentService.DeleteByIds))]
	public async Task DeleteByIdsShouldReturnFailedWhenExceptionIsThrown()
	{
		Guid userId = Guid.NewGuid();
		IEnumerable<Guid> ids = [Guid.NewGuid()];
		string parameter = string.Join(',', ids.Select(x => $"{x}"));
		DocumentService sut = CreateMockedInstance();

		ErrorOr<Deleted> result = await sut.DeleteByIds(userId, ids)
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
	[TestCategory(nameof(DocumentService.DeleteByIds))]
	public async Task DeleteByIdsShouldReturnNotFoundWhenNotFound()
	{
		Guid userId = Guid.NewGuid();
		IEnumerable<Guid> ids = [Guid.NewGuid()];
		Mock<IDocumentRepository> docRepoMock = new();
		docRepoMock.Setup(x => x.GetManyByConditionAsync(x => x.UserId.Equals(userId) && ids.Contains(x.Id), default, default, default, default, default, default, default))
			.Returns(Task.FromResult<IEnumerable<DocumentEntity>>([]));
		DocumentService sut = CreateMockedInstance(docRepoMock.Object);

		ErrorOr<Deleted> result = await sut.DeleteByIds(userId, ids)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(DocumentServiceErrors.DeleteByIdsNotFound(ids));
			docRepoMock.Verify(x => x.GetManyByConditionAsync(x => x.UserId.Equals(userId) && ids.Contains(x.Id), default, default, default, default, default, default, default), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), It.IsAny<object>(), It.IsAny<Exception>()), Times.Never);
		});
	}

	[TestMethod]
	[TestCategory(nameof(DocumentService.DeleteByIds))]
	public async Task DeleteByIdsShouldReturnDeletedWhenSuccessful()
	{
		Guid userId = Guid.NewGuid();
		IEnumerable<Guid> ids = [Guid.NewGuid()];
		IEnumerable<DocumentEntity> documents = [CreateDocument()];
		Mock<IDocumentRepository> docRepoMock = new();
		docRepoMock.Setup(x => x.GetManyByConditionAsync(x => x.UserId.Equals(userId) && ids.Contains(x.Id), default, default, default, default, default, default, default))
			.Returns(Task.FromResult(documents));
		DocumentService sut = CreateMockedInstance(docRepoMock.Object);

		ErrorOr<Deleted> result = await sut.DeleteByIds(userId, ids)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().Be(Result.Deleted);
			docRepoMock.Verify(x => x.GetManyByConditionAsync(x => x.UserId.Equals(userId) && ids.Contains(x.Id), default, default, default, default, default, default, default), Times.Once);
			docRepoMock.Verify(x => x.DeleteAsync(documents, default), Times.Once);
			_repositoryServiceMock.Verify(x => x.CommitChangesAsync(default), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), It.IsAny<object>(), It.IsAny<Exception>()), Times.Never);
		});
	}
}
