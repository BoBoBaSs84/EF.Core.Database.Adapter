namespace DomainTests;

[TestClass]
public abstract class DomainTestBase
{
	[AssemblyInitialize]
	public static void AssemblyInitialize(TestContext context) { }

	[AssemblyCleanup]
	public static void AssemblyCleanup() { }
}
