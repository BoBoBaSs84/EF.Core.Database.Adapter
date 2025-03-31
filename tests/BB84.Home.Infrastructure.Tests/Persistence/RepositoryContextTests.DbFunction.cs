using BB84.Home.Application.Interfaces.Infrastructure.Persistence;

namespace InfrastructureTests.Persistence;

[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, unit testing.")]
public sealed partial class RepositoryContextTests
{
	[TestMethod]
	[ExpectedException(typeof(InvalidOperationException))]
	public void DifferenceShouldThrowExceptionWhenCalledDirectly()
	{
		IRepositoryContext context = GetService<IRepositoryContext>();

		context.Difference(string.Empty, string.Empty);
	}

	[TestMethod]
	[ExpectedException(typeof(InvalidOperationException))]
	public void EndOfMonthShouldThrowExceptionWhenCalledDirectly()
	{
		IRepositoryContext context = GetService<IRepositoryContext>();

		context.EndOfMonth(DateTime.Today);
	}

	[TestMethod]
	[ExpectedException(typeof(InvalidOperationException))]
	public void SoundexShouldThrowExceptionWhenCalledDirectly()
	{
		IRepositoryContext context = GetService<IRepositoryContext>();

		context.Soundex(string.Empty);
	}

	[TestMethod]
	[ExpectedException(typeof(InvalidOperationException))]
	public void TranslateShouldThrowExceptionWhenCalledDirectly()
	{
		IRepositoryContext context = GetService<IRepositoryContext>();

		context.Translate(string.Empty, string.Empty, string.Empty);
	}

	[TestMethod]
	[ExpectedException(typeof(InvalidOperationException))]
	public void QuoteNameShouldThrowExceptionWhenCalledDirectly()
	{
		IRepositoryContext context = GetService<IRepositoryContext>();

		context.QuoteName(string.Empty);
	}
}
