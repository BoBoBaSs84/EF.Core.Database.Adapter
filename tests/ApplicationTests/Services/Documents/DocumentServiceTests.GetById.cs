﻿using Application.Contracts.Responses.Documents;
using Application.Errors.Services;
using Application.Interfaces.Infrastructure.Persistence.Repositories.Documents;
using Application.Services.Documents;

using BaseTests.Helpers;

using Domain.Errors;
using Domain.Models.Documents;

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
		Mock<IDocumentRepository> docRepoMock = new();
		string[] includes = [nameof(Document.Data), nameof(Document.Extension)];
		docRepoMock.Setup(x => x.GetByConditionAsync(
			x => x.Id.Equals(documentId) && x.DocumentUsers.Select(x => x.UserId).Contains(userId), default, default, default, default, includes))
			.Returns(Task.FromResult<Document?>(null));
		DocumentService sut = CreateMockedInstance(docRepoMock.Object);

		ErrorOr<DocumentResponse> result = await sut.GetById(userId, documentId)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(DocumentServiceErrors.GetByIdNotFound(documentId));
			docRepoMock.Verify(x => x.GetByConditionAsync(
				x => x.Id.Equals(documentId) && x.DocumentUsers.Select(x => x.UserId).Contains(userId), default, default, default, default, includes), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), It.IsAny<object>(), It.IsAny<Exception>()), Times.Never);
		});
	}

	[TestMethod]
	[TestCategory(nameof(DocumentService.GetById))]
	public async Task GetByIdShouldReturnResultWhenSuccessful()
	{
		Guid userId = Guid.NewGuid(), documentId = Guid.NewGuid();
		Mock<IDocumentRepository> docRepoMock = new();
		string[] includes = [nameof(Document.Data), nameof(Document.Extension)];
		Document document = CreateDocument(documentId);
		docRepoMock.Setup(x => x.GetByConditionAsync(
			x => x.Id.Equals(documentId) && x.DocumentUsers.Select(x => x.UserId).Contains(userId), default, default, default, default, includes))
			.Returns(Task.FromResult<Document?>(document));
		DocumentService sut = CreateMockedInstance(docRepoMock.Object);

		ErrorOr<DocumentResponse> result = await sut.GetById(userId, documentId)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Content?.SequenceEqual(document.Data.Content).Should().BeTrue();
			result.Value.CreationTime.Should().Be(document.CreationTime);
			result.Value.Directory.Should().Be(document.Directory);
			result.Value.ExtenionName.Should().Be(document.Extension.Name);
			result.Value.Flags.Should().Be(document.Flags);
			result.Value.Id.Should().Be(document.Id);
			result.Value.LastAccessTime.Should().Be(document.LastAccessTime);
			result.Value.LastWriteTime.Should().Be(document.LastWriteTime);
			result.Value.Length.Should().Be(document.Data.Length);
			result.Value.MD5Hash?.SequenceEqual(document.Data.MD5Hash).Should().BeTrue();
			result.Value.MimeType.Should().Be(document.Extension.MimeType);
			result.Value.Name.Should().Be(document.Name);
			docRepoMock.Verify(x => x.GetByConditionAsync(
				x => x.Id.Equals(documentId) && x.DocumentUsers.Select(x => x.UserId).Contains(userId), default, default, default, default, includes), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), It.IsAny<object>(), It.IsAny<Exception>()), Times.Never);
		});
	}
}
