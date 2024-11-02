﻿using Application.Contracts.Requests.Documents;
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
	[TestCategory(nameof(DocumentService.UpdateById))]
	public async Task UpdateByIdShouldReturnFailedWhenExceptionIsThrown()
	{
		DocumentUpdateRequest request = RequestHelper.GetDocumentUpdateRequest();
		string parameter = request.ToJson();
		DocumentService sut = CreateMockedInstance();

		ErrorOr<Updated> result = await sut.UpdateById(request)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(DocumentServiceErrors.UpdateByIdFailed(request.Id));
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), parameter, It.IsAny<Exception>()), Times.Once);
		});
	}

	[TestMethod]
	[TestCategory(nameof(DocumentService.UpdateById))]
	public async Task UpdateByIdShouldReturnNotFoundWhenNotFound()
	{
		DocumentUpdateRequest request = RequestHelper.GetDocumentUpdateRequest();
		string[] includes = [nameof(Document.Extension), nameof(Document.Data)];
		Mock<IDocumentRepository> docRepoMock = new();
		docRepoMock.Setup(x => x.GetByIdAsync(request.Id, default, true, default, includes))
			.Returns(Task.FromResult<Document?>(null));
		DocumentService sut = CreateMockedInstance(docRepoMock.Object);

		ErrorOr<Updated> result = await sut.UpdateById(request)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(DocumentServiceErrors.UpdateByIdNotFound(request.Id));
			docRepoMock.Verify(x => x.GetByIdAsync(request.Id, default, true, default, includes), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), It.IsAny<object>(), It.IsAny<Exception>()), Times.Never);
		});
	}

	[TestMethod]
	[TestCategory(nameof(DocumentService.UpdateById))]
	public async Task UpdateByIdShouldReturnUpdatedWhenSuccessful()
	{
		DocumentUpdateRequest request = RequestHelper.GetDocumentUpdateRequest();
		Document document = CreateDocument();
		string[] includes = [nameof(Document.Extension), nameof(Document.Data)];
		Mock<IDocumentRepository> docRepoMock = new();
		docRepoMock.Setup(x => x.GetByIdAsync(request.Id, default, true, default, includes))
			.Returns(Task.FromResult<Document?>(document));
		Mock<IDocumentExtensionRepository> extRepoMock = new();
		extRepoMock.Setup(x => x.GetByConditionAsync(x => x.Name == request.ExtensionName, default, default, default, default))
			.Returns(Task.FromResult<Extension?>(null));
		byte[] md5Hash = request.Content.GetMD5();
		Mock<IDocumentDataRepository> dataRepoMock = new();
		dataRepoMock.Setup(x => x.GetByConditionAsync(x => x.MD5Hash.SequenceEqual(md5Hash), default, default, default, default))
			.Returns(Task.FromResult<Data?>(null));
		DocumentService sut = CreateMockedInstance(docRepoMock.Object, extRepoMock.Object, dataRepoMock.Object);

		ErrorOr<Updated> result = await sut.UpdateById(request)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().Be(Result.Updated);
			docRepoMock.Verify(x => x.GetByIdAsync(request.Id, default, true, default, includes), Times.Once);
			extRepoMock.Verify(x => x.GetByConditionAsync(x => x.Name == request.ExtensionName, default, default, default, default), Times.Once());
			_repositoryServiceMock.Verify(x => x.CommitChangesAsync(default), Times.Once());
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), It.IsAny<object>(), It.IsAny<Exception>()), Times.Never);
		});
	}
}