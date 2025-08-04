using BB84.Home.Application.Interfaces.Infrastructure.Persistence;

namespace InfrastructureTests.Persistence;

[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, unit testing.")]
public sealed partial class RepositoryContextTests
{
	[TestMethod]
	public void DifferenceShouldThrowExceptionWhenCalledDirectly()
		=> Assert.ThrowsExactly<InvalidOperationException>(() => GetService<IRepositoryContext>().Difference(string.Empty, string.Empty));

	[TestMethod]
	public void EndOfMonthShouldThrowExceptionWhenCalledDirectly()
		=> Assert.ThrowsExactly<InvalidOperationException>(() => GetService<IRepositoryContext>().EndOfMonth(DateTime.Today));

	[TestMethod]
	public void SoundexShouldThrowExceptionWhenCalledDirectly()
		=> Assert.ThrowsExactly<InvalidOperationException>(() => GetService<IRepositoryContext>().Soundex(string.Empty));

	[TestMethod]
	public void TranslateShouldThrowExceptionWhenCalledDirectly() => Assert.ThrowsExactly<InvalidOperationException>(()
		=> GetService<IRepositoryContext>().Translate(string.Empty, string.Empty, string.Empty));

	[TestMethod]
	public void QuoteNameShouldThrowExceptionWhenCalledDirectly()
		=> Assert.ThrowsExactly<InvalidOperationException>(() => GetService<IRepositoryContext>().QuoteName(string.Empty));
}
