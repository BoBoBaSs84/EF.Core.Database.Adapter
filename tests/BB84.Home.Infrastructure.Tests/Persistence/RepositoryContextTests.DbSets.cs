using BB84.Home.Application.Interfaces.Infrastructure.Persistence;

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
	}
}
