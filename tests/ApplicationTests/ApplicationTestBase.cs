using BaseTests;

namespace ApplicationTests;

[TestClass]
public class ApplicationTestBase : TestBase
{
	[AssemblyInitialize]
	public static new void AssemblyInitialize(TestContext context)
		=> TestBase.AssemblyInitialize(context);

	[AssemblyCleanup]
	public static new void AssemblyCleanup()
		=> TestBase.AssemblyCleanup();
}
