using Application.Errors.Services;
using Application.Interfaces.Infrastructure.Persistence.Repositories.Documents;
using Application.Services.Documents;

using BaseTests.Helpers;

using Domain.Errors;
using Domain.Models.Documents;
using Domain.Results;

using FluentAssertions;

using Microsoft.Extensions.Logging;

using Moq;

namespace ApplicationTests.Services.Documents;

public sealed partial class DocumentServiceTests
{
	[TestMethod]
	[TestCategory(nameof(DocumentService.DeleteById))]
	public async Task DeleteByIdShouldReturnFailedWhenExceptionIsThrown()
	{
		Guid id = Guid.NewGuid();
		DocumentService sut = CreateMockedInstance();

		ErrorOr<Deleted> result = await sut.DeleteById(id)
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
	[TestCategory(nameof(DocumentService.DeleteById))]
	public async Task DeleteByIdShouldReturnNotFoundWhenNotFound()
	{
		Guid id = Guid.NewGuid();
		Mock<IDocumentRepository> docRepoMock = new();
		docRepoMock.Setup(x => x.GetByIdAsync(id, default, default, default))
			.Returns(Task.FromResult<Document?>(null));
		DocumentService sut = CreateMockedInstance(docRepoMock.Object);

		ErrorOr<Deleted> result = await sut.DeleteById(id)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(DocumentServiceErrors.DeleteByIdNotFound(id));
			docRepoMock.Verify(x => x.GetByIdAsync(id, default, default, default), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), It.IsAny<object>(), It.IsAny<Exception>()), Times.Never);
		});
	}

	[TestMethod]
	[TestCategory(nameof(DocumentService.DeleteById))]
	public async Task DeleteByIdShouldReturnDeletedWhenSuccessful()
	{
		Guid id = Guid.NewGuid();
		Document document = CreateDocument(id);
		Mock<IDocumentRepository> docRepoMock = new();
		docRepoMock.Setup(x => x.GetByIdAsync(id, default, default, default))
			.Returns(Task.FromResult<Document?>(document));
		DocumentService sut = CreateMockedInstance(docRepoMock.Object);

		ErrorOr<Deleted> result = await sut.DeleteById(id)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().Be(Result.Deleted);
			docRepoMock.Verify(x => x.GetByIdAsync(id, default, default, default), Times.Once);
			docRepoMock.Verify(x => x.DeleteAsync(document, default), Times.Once);
			_repositoryServiceMock.Verify(x => x.CommitChangesAsync(default), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), It.IsAny<object>(), It.IsAny<Exception>()), Times.Never);
		});
	}
}
