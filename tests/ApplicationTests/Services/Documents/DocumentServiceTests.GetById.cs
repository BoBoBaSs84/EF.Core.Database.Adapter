using Application.Contracts.Responses.Documents;
using Application.Contracts.Requests.Documents;
using Application.Errors.Services;
using Application.Interfaces.Infrastructure.Persistence.Repositories.Documents;
using Application.Services.Documents;

using ApplicationTests.Helpers;

using BaseTests.Helpers;

using BB84.Extensions;
using BB84.Extensions.Serialization;

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
	[TestCategory(nameof(DocumentService.GetById))]
	public async Task GetByIdShouldReturnFailedWhenExcpetionIsThrown()
	{
		Guid userId = Guid.NewGuid(), documentId = Guid.NewGuid();
		string[] parameters = [$"{userId}", $"{documentId}"];
		DocumentService sut = CreateMockedInstance();

		ErrorOr<DocumentResponse> result = await sut.GetById(userId, documentId)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(DocumentServiceErrors.GetByIdFailed(documentId));
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), parameters, It.IsAny<Exception>()), Times.Once);
		});
	}

	[TestMethod]
	[TestCategory(nameof(DocumentService.GetById))]
	public async Task GetByIdShouldReturnNotFoundWhenNotFound()
	{
		Guid userId = Guid.NewGuid(), documentId = Guid.NewGuid();
		string[] parameters = [$"{userId}", $"{documentId}"];
		Mock<IDocumentRepository> docRepoMock = new();
		string[] includes = [nameof(Document.Data), nameof(Document.Extension)];
		docRepoMock.Setup(x => x.GetByConditionAsync(x => x.Id.Equals(documentId) && x.DocumentUsers.Select(x => x.UserId).Contains(userId), default, default, default, default, includes))
			.Returns(Task.FromResult<Document?>(null));
		DocumentService sut = CreateMockedInstance(docRepoMock.Object);

		ErrorOr<DocumentResponse> result = await sut.GetById(userId, documentId)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(DocumentServiceErrors.GetByIdNotFound(documentId));
			docRepoMock.Verify(x => x.GetByConditionAsync(x => x.Id.Equals(documentId) && x.DocumentUsers.Select(x => x.UserId).Contains(userId), default, default, default, default, includes), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), parameters, It.IsAny<Exception>()), Times.Never);
		});
	}
}
