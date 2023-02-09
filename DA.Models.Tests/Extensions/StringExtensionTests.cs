using DA.Models.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using static DA.Base.Tests.Constants;

namespace DA.Models.Tests.Extensions;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "UnitTest")]
public class StringExtensionTests : EntitiesBaseTest
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