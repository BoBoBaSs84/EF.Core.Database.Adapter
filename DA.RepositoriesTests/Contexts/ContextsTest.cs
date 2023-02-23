using DA.BaseTests.Helpers;
using DA.Repositories.Common;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using static DA.BaseTests.Constants;
using static DA.BaseTests.Helpers.AssertionHelper;

namespace DA.RepositoriesTests.Contexts;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "UnitTest")]
[SuppressMessage("Globalization", "CA1310", Justification = "UnitTest")]
public class ContextsTest : RepositoriesBaseTest
{
	private readonly Assembly _assembly = typeof(IAssemblyMarker).Assembly;

	[TestMethod, Owner(Bobo)]
	public void RepositoriesShouldNotBePublicAndShouldBeSealedTest()
	{
		IEnumerable<Type> typeList = TypeHelper.GetAssemblyTypes(
			assembly: _assembly,
			expression: x => x.Name.EndsWith("Repository") && x.IsInterface.Equals(false)
			);

		typeList.Should().NotBeNullOrEmpty();

		foreach (Type type in typeList)
		{
			TestContext.WriteLine($"Testing: {type.Name}");

			AssertInScope(() =>
			{
				type.IsSealed.Should().BeTrue();
				type.IsPublic.Should().BeFalse();
				type.IsVisible.Should().BeFalse();
			});
		}
	}
}
