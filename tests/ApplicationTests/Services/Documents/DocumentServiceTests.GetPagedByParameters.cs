using System.Linq.Expressions;

using Application.Contracts.Responses.Documents;
using Application.Errors.Services;
using Application.Features.Requests;
using Application.Features.Responses;
using Application.Interfaces.Infrastructure.Persistence.Repositories.Documents;
using Application.Services.Documents;

using BaseTests.Helpers;

using BB84.Extensions.Serialization;

using Domain.Entities.Documents;
using Domain.Errors;

using FluentAssertions;

using Microsoft.Extensions.Logging;

using Moq;

namespace ApplicationTests.Services.Documents;

public sealed partial class DocumentServiceTests
{
	[TestMethod]
	[TestCategory(nameof(DocumentService.GetPagedByParameters))]
	public async Task GetPagedByParametersShouldReturnFailedWhenExceptionIsThrown()
	{
		Guid userId = Guid.NewGuid();
		DocumentParameters parameters = new();
		string[] parameter = [$"{userId}", parameters.ToJson()];
		DocumentService sut = CreateMockedInstance();

		ErrorOr<IPagedList<DocumentResponse>> result = await sut.GetPagedByParameters(userId, parameters)
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
	[TestCategory(nameof(DocumentService.GetPagedByParameters))]
	public async Task GetPagedByParametersShouldReturnResultWhenSuccessful()
	{
		Guid userId = Guid.NewGuid();
		DocumentParameters parameters = new();
		IEnumerable<DocumentEntity> documents = [CreateDocument(), CreateDocument()];
		Mock<IDocumentRepository> docRepoMock = new();
		docRepoMock.Setup(x => x.GetManyByConditionAsync(
			It.IsAny<Expression<Func<DocumentEntity, bool>>?>(), It.IsAny<Func<IQueryable<DocumentEntity>, IQueryable<DocumentEntity>>?>(), default,
			It.IsAny<Func<IQueryable<DocumentEntity>, IOrderedQueryable<DocumentEntity>>?>(), (parameters.PageNumber - 1) * parameters.PageSize,
			parameters.PageSize, default, default))
			.Returns(Task.FromResult(documents));
		docRepoMock.Setup(x => x.CountAsync(
			It.IsAny<Expression<Func<DocumentEntity, bool>>?>(), It.IsAny<Func<IQueryable<DocumentEntity>, IQueryable<DocumentEntity>>?>(), default, default))
			.Returns(Task.FromResult(2));
		DocumentService sut = CreateMockedInstance(docRepoMock.Object);

		ErrorOr<IPagedList<DocumentResponse>> result = await sut.GetPagedByParameters(userId, parameters)
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
