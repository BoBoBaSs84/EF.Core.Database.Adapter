using Domain.Extensions;

using FluentAssertions;

using static BaseTests.Constants.TestConstants;

namespace DomainTests.Extensions;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "UnitTest")]
public class StringExtensionTests : DomainTestBase
{
	[TestMethod, Owner(Bobo)]
	public void RemoveWhitespaceSuccessTest()
	{
		string testString = "Hallo Test!";

		testString = testString.RemoveWhitespace();

		testString.Should().NotContain(" ");
	}

	[TestMethod, Owner(Bobo)]
	public void RemoveWhitespaceFailTest()
	{
		string testString = "Hallo Test!";

		testString.RemoveWhitespace();

		testString.Should().Contain(" ");
	}
}