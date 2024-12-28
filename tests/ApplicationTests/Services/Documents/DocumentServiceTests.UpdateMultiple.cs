﻿using Application.Contracts.Requests.Documents;
using Application.Errors.Services;
using Application.Interfaces.Infrastructure.Persistence.Repositories.Documents;
using Application.Services.Documents;

using ApplicationTests.Helpers;

using BaseTests.Helpers;

using BB84.Extensions;
using BB84.Extensions.Serialization;

using Domain.Entities.Documents;
using Domain.Errors;
using Domain.Results;

using FluentAssertions;

using Microsoft.Extensions.Logging;

using Moq;

namespace ApplicationTests.Services.Documents;

public sealed partial class DocumentServiceTests
{
	[TestMethod]
	[TestCategory(nameof(DocumentService.Update))]
	public async Task UpdateByIdsShouldReturnFailedWhenExceptionIsThrown()
	{
		Guid userId = Guid.NewGuid();
		IEnumerable<DocumentUpdateRequest> requests = [RequestHelper.GetDocumentUpdateRequest()];
		string parameter = requests.ToJson();
		DocumentService sut = CreateMockedInstance();

		ErrorOr<Updated> result = await sut.Update(userId, requests)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(DocumentServiceErrors.UpdateByIdsFailed(requests.Select(x => x.Id)));
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), parameter, It.IsAny<Exception>()), Times.Once);
		});
	}

	[TestMethod]
	[TestCategory(nameof(DocumentService.Update))]
	public async Task UpdateByIdsShouldReturnBadRequestWhenBodyIsEmpty()
	{
		Guid userId = Guid.NewGuid();
		IEnumerable<DocumentUpdateRequest> requests = [];
		DocumentService sut = CreateMockedInstance();

		ErrorOr<Updated> result = await sut.Update(userId, requests)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(DocumentServiceErrors.UpdateByIdsBadRequest);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), It.IsAny<object>(), It.IsAny<Exception>()), Times.Never);
		});
	}

	[TestMethod]
	[TestCategory(nameof(DocumentService.Update))]
	public async Task UpdateByIdsShouldReturnNotFoundWhenNotFound()
	{
		Guid userId = Guid.NewGuid();
		IEnumerable<DocumentUpdateRequest> requests = [RequestHelper.GetDocumentUpdateRequest()];
		string[] includes = [nameof(DocumentEntity.Extension), nameof(DocumentEntity.Data)];
		Mock<IDocumentRepository> docRepoMock = new();
		docRepoMock.Setup(x => x.GetManyByConditionAsync(x =>
			x.UserId.Equals(userId) && requests.Select(x => x.Id).Contains(x.Id), default, default, default, default, default, true, default, includes))
			.Returns(Task.FromResult<IEnumerable<DocumentEntity>>([]));
		DocumentService sut = CreateMockedInstance(docRepoMock.Object);

		ErrorOr<Updated> result = await sut.Update(userId, requests)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(DocumentServiceErrors.UpdateByIdsNotFound(requests.Select(x => x.Id)));
			docRepoMock.Verify(x => x.GetManyByConditionAsync(x =>
				x.UserId.Equals(userId) && requests.Select(x => x.Id).Contains(x.Id), default, default, default, default, default, true, default, includes), Times.Once);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), It.IsAny<object>(), It.IsAny<Exception>()), Times.Never);
		});
	}

	[TestMethod]
	[TestCategory(nameof(DocumentService.Update))]
	public async Task UpdateByIdsShouldReturnUpdatedWhenSuccessful()
	{
		Guid userId = Guid.NewGuid();
		DocumentUpdateRequest request = RequestHelper.GetDocumentUpdateRequest();
		DocumentEntity document = CreateDocument(request.Id);
		IEnumerable<DocumentUpdateRequest> requests = [request];
		string[] includes = [nameof(DocumentEntity.Extension), nameof(DocumentEntity.Data)];
		Mock<IDocumentRepository> docRepoMock = new();
		docRepoMock.Setup(x => x.GetManyByConditionAsync(x =>
			x.UserId.Equals(userId) && requests.Select(x => x.Id).Contains(x.Id), default, default, default, default, default, true, default, includes))
			.Returns(Task.FromResult<IEnumerable<DocumentEntity>>([document]));
		Mock<IDocumentExtensionRepository> extRepoMock = new();
		extRepoMock.Setup(x => x.GetByConditionAsync(x => x.Name == request.ExtensionName, default, default, default, default))
			.Returns(Task.FromResult<ExtensionEntity?>(null));
		byte[] md5Hash = request.Content.GetMD5();
		Mock<IDocumentDataRepository> dataRepoMock = new();
		dataRepoMock.Setup(x => x.GetByConditionAsync(x => x.MD5Hash.SequenceEqual(md5Hash), default, default, default, default))
			.Returns(Task.FromResult<DataEntity?>(null));
		DocumentService sut = CreateMockedInstance(docRepoMock.Object, extRepoMock.Object, dataRepoMock.Object);

		ErrorOr<Updated> result = await sut.Update(userId, requests)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().Be(Result.Updated);
			docRepoMock.Verify(x => x.GetManyByConditionAsync(x =>
				x.UserId.Equals(userId) && requests.Select(x => x.Id).Contains(x.Id), default, default, default, default, default, true, default, includes), Times.Once);
			extRepoMock.Verify(x => x.GetByConditionAsync(x => x.Name == request.ExtensionName, default, default, default, default), Times.Once());
			_repositoryServiceMock.Verify(x => x.CommitChangesAsync(default), Times.Once());
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), It.IsAny<object>(), It.IsAny<Exception>()), Times.Never);
		});
	}
}
