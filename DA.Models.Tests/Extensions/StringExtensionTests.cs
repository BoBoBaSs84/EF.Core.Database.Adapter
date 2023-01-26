using DA.Models.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

namespace DA.Models.Tests.Extensions;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "UnitTest")]
public class StringExtensionTests : EntitiesBaseTest
{
	[TestMethod]
	public void RemoveWhitespaceSuccessTest()
	{
		string testString = "Hallo Test!";

		testString = testString.RemoveWhitespace();

		testString.Should().NotContain(" ");
	}

	[TestMethod]
	public void RemoveWhitespaceFailTest()
	{
		string testString = "Hallo Test!";

		testString.RemoveWhitespace();

		testString.Should().Contain(" ");
	}
}