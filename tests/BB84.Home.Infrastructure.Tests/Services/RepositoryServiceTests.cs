using BB84.Home.Application.Interfaces.Infrastructure.Persistence;
using BB84.Home.Infrastructure.Services;

using FluentAssertions;

using Moq;

namespace BB84.Home.Infrastructure.Tests.Services;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, unit testing.")]
public sealed class RepositoryServiceTests : InfrastructureTestBase
{
	[TestMethod]
	[TestCategory(nameof(RepositoryService))]
	public void RepositoryServiceTest()
	{
		Mock<IRepositoryContext> contextMock = new();

		RepositoryService service = new(contextMock.Object);

		service.Should().NotBeNull();
		service.AccountRepository.Should().NotBeNull();
		service.AttendanceRepository.Should().NotBeNull();
		service.CardRepository.Should().NotBeNull();
		service.DocumentRepository.Should().NotBeNull();
		service.DocumentDataRepository.Should().NotBeNull();
		service.DocumentExtensionRepository.Should().NotBeNull();
		service.TodoItemRepository.Should().NotBeNull();
		service.TodoListRepository.Should().NotBeNull();
		service.TransactionRepository.Should().NotBeNull();
	}

	[TestMethod]
	[TestCategory(nameof(RepositoryService.CommitChangesAsync))]
	public async Task CommitChangesAsyncTest()
	{
		int count = 100;
		Mock<IRepositoryContext> contextMock = new();
		contextMock.Setup(x => x.SaveChangesAsync(default)).Returns(Task.FromResult(count));

		RepositoryService service = new(contextMock.Object);

		int result = await service.CommitChangesAsync()
			.ConfigureAwait(false);

		result.Should().Be(count);
		contextMock.Verify(x => x.SaveChangesAsync(default), Times.Once);
	}
}