using DA.Base.Tests.Helpers;
using DA.Repositories.Common;
using DA.Repositories.Tests;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace DA.Repositories.Tests.Contexts;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "UnitTest")]
[SuppressMessage("Globalization", "CA1310", Justification = "UnitTest")]
public class ContextsTest : RepositoriesBaseTest
{
	private readonly Assembly _assembly = typeof(IAssemblyMarker).Assembly;

	[TestMethod]
	public void RepositoriesShouldNotBePublicAndShouldBeSealedTest()
	{
		ICollection<Type> repositoriesList =
			TypeHelper.GetAssemblyTypes(_assembly, x => x.Name.EndsWith("Repository") && x.IsInterface.Equals(false));

		repositoriesList.Should().NotBeNullOrEmpty();

		foreach (Type type in repositoriesList)
		{
			TestContext.WriteLine($"Testing: {type.Name}");
			type.IsSealed.Should().BeTrue();
			type.IsPublic.Should().BeFalse();
			type.IsVisible.Should().BeFalse();
		}
	}
}
