using Application.Interfaces.Infrastructure.Persistence;

using FluentAssertions;

namespace InfrastructureTests.Persistence;

public sealed partial class RepositoryContextTests
{
	[TestMethod]
	public void DbSetsShouldNotBeNull()
	{
		IRepositoryContext? context;

		context = GetService<IRepositoryContext>();

		context.Should().NotBeNull();
		context.Accounts.Should().NotBeNull();
		context.Attendances.Should().NotBeNull();
		context.Cards.Should().NotBeNull();
		context.Transactions.Should().NotBeNull();
		context.TodoLists.Should().NotBeNull();
		context.TodoItems.Should().NotBeNull();
	}
}
