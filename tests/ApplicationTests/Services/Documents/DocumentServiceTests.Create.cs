using Application.Contracts.Requests.Documents;
using Application.Services.Documents;

using ApplicationTests.Helpers;

using BaseTests.Helpers;

using Domain.Errors;
using Domain.Results;

using FluentAssertions;

namespace ApplicationTests.Services.Documents;

public sealed partial class DocumentServiceTests
{
	[TestMethod]
	[TestCategory(nameof(DocumentService.Create))]
	public async Task CreateShouldReturnFailedWhenExceptionIsThrown()
	{
		Guid userId = Guid.NewGuid();
		DocumentCreateRequest request = RequestHelper.GetDocumentCreateRequest();
		DocumentService sut = CreateMockedInstance();

		ErrorOr<Created> result = await sut.Create(userId, request)
			.ConfigureAwait(false);

		AssertionHelper.AssertInScope(() =>
		{
			result.Should().NotBeNull();
			result.IsError.Should().BeTrue();
		});
	}
}
