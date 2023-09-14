using System.Globalization;

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

	[TestMethod, Owner(Bobo)]
	public void FormatInvariantSuccessTest()
	{
		DateTime dateTime = DateTime.Now;
		string testString = @"Today is: {0}";

		string resultString = testString.ToInvariant(dateTime);

		resultString.Should().Contain(dateTime.ToString(CultureInfo.InvariantCulture));
	}

	[TestMethod, Owner(Bobo)]
	public void FormatInvariantFailedTest()
	{
		DateTime dateTime = DateTime.Now;
		string testString = @"Today is: {0}";

		string resultString = testString.ToInvariant(dateTime);

		resultString.Should().NotContain(dateTime.ToString(CultureInfo.GetCultureInfo("de-DE")));
	}

	[TestMethod, Owner(Bobo)]
	public void FormatSuccessTest()
	{
		DateTime dateTime = DateTime.Now;
		string testString = @"Today is: {0}";

		string resultString = testString.Format(CultureInfo.GetCultureInfo("de-DE"), dateTime);

		resultString.Should().Contain(dateTime.ToString(CultureInfo.GetCultureInfo("de-DE")));
	}

	[TestMethod, Owner(Bobo)]
	public void FormatFailedTest()
	{
		DateTime dateTime = DateTime.Now;
		string testString = @"Today is: {0}";

		string resultString = testString.Format(CultureInfo.GetCultureInfo("de-DE"), dateTime);

		resultString.Should().NotContain(dateTime.ToString(CultureInfo.InvariantCulture));
	}
}