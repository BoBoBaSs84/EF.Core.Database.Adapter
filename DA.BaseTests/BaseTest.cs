using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DA.BaseTests;

[TestClass]
public abstract class BaseTest
{
	public TestContext TestContext { get; set; } = default!;

	[TestInitialize]
	public virtual void Initialize() =>
		TestContext.WriteLine($"{nameof(Initialize)}: {TestContext.TestName}");

	[TestCleanup]
	public virtual void Cleanup() =>
		TestContext.WriteLine($"{nameof(Cleanup)}: {TestContext.TestName}");
}
