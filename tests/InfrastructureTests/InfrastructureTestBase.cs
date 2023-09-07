using BaseTests;

namespace InfrastructureTests;

[TestClass]
public class InfrastructureTestBase : TestBase
{
	[AssemblyInitialize]
	public static new void AssemblyInitialize(TestContext testContext)
		=> TestBase.AssemblyInitialize(testContext);

	[AssemblyCleanup]
	public static new void AssemblyCleanup()
		=> TestBase.AssemblyCleanup();
}
