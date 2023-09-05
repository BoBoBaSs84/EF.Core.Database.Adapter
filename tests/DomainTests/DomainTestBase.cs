using BaseTests;

namespace DomainTests;

[TestClass]
public class DomainTestBase : TestBase
{
	[AssemblyInitialize]
	public static new void AssemblyInitialize(TestContext testContext)
		=> TestBase.AssemblyInitialize(testContext);

	[AssemblyCleanup]
	public static new void AssemblyCleanup()
		=> TestBase.AssemblyCleanup();
}
