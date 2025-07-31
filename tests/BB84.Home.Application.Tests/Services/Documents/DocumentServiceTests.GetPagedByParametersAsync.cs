using System.Linq.Expressions;

using BB84.Extensions.Serialization;
using BB84.Home.Application.Contracts.Responses.Documents;
using BB84.Home.Application.Errors.Services;
using BB84.Home.Application.Features.Requests;
using BB84.Home.Application.Features.Responses;
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
	public async Task GetPagedByParametersAsyncShouldReturnFailedWhenExceptionIsThrown()
	{
		Guid userId = Guid.NewGuid();
		CancellationToken token = CancellationToken.None;
		DocumentParameters parameters = new();
		string[] parameter = [$"{userId}", parameters.ToJson()];
		_currentUserService.Setup(x => x.UserId)
			.Returns(userId);

		ErrorOr<IPagedList<DocumentResponse>> result = await _sut
			.GetPagedByParametersAsync(parameters, token)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
			result.Errors.First().Should().Be(DocumentServiceErrors.GetPagedByParametersFailed);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), parameter, It.IsAny<Exception>()), Times.Once);
		});
	}

	[TestMethod]

	public async Task GetPagedByParametersAsyncShouldReturnResultWhenSuccessful()
	{
		Guid userId = Guid.NewGuid();
		CancellationToken token = CancellationToken.None;
		DocumentParameters parameters = new();
		IEnumerable<DocumentEntity> documents = [CreateDocument(), CreateDocument()];
		Mock<IDocumentRepository> docRepoMock = new();
		docRepoMock.Setup(x => x.GetManyByConditionAsync(
			It.IsAny<Expression<Func<DocumentEntity, bool>>?>(), It.IsAny<Func<IQueryable<DocumentEntity>, IQueryable<DocumentEntity>>?>(), default,
			It.IsAny<Func<IQueryable<DocumentEntity>, IOrderedQueryable<DocumentEntity>>?>(), (parameters.PageNumber - 1) * parameters.PageSize,
			parameters.PageSize, default, token))
			.Returns(Task.FromResult(documents));
		docRepoMock.Setup(x => x.CountAsync(
			It.IsAny<Expression<Func<DocumentEntity, bool>>?>(), It.IsAny<Func<IQueryable<DocumentEntity>, IQueryable<DocumentEntity>>?>(), default, token))
			.Returns(Task.FromResult(2));
		_currentUserService.Setup(x => x.UserId)
			.Returns(userId);
		_repositoryServiceMock.Setup(x => x.DocumentRepository)
			.Returns(docRepoMock.Object);

		ErrorOr<IPagedList<DocumentResponse>> result = await _sut
			.GetPagedByParametersAsync(parameters, token)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeFalse();
			result.Errors.Should().BeEmpty();
			result.Value.Should().NotBeNull();
			result.Value.Count.Should().Be(2);
			result.Value.MetaData.CurrentPage.Should().Be(1);
			result.Value.MetaData.HasNext.Should().BeFalse();
			result.Value.MetaData.HasPrevious.Should().BeFalse();
			result.Value.MetaData.PageSize.Should().Be(10);
			result.Value.MetaData.TotalCount.Should().Be(2);
			result.Value.MetaData.TotalPages.Should().Be(1);
			_loggerServiceMock.Verify(x => x.Log(It.IsAny<Action<ILogger, object, Exception?>>(), It.IsAny<object>(), It.IsAny<Exception>()), Times.Never);
		});
	}
}
