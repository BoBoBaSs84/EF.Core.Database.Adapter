using Database.Adapter.Base.Tests;
using Database.Adapter.Base.Tests.Helpers;
using Database.Adapter.Repositories.Common;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Database.Adapter.Repositories.Tests;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "UnitTest")]
[SuppressMessage("Globalization", "CA1310", Justification = "UnitTest")]
public class RepositoriesBaseTest : BaseTest
{
	private readonly Assembly _assembly = typeof(IAssemblyMarker).Assembly;

	[TestMethod]
	public void RepositoriesShouldNotBePublicAndShouldBeSealedTest()
	{
		ICollection<Type> typeList = 
			TypeHelper.GetAssemblyTypes(_assembly, x => x.Name.EndsWith("Repository") && x.IsInterface.Equals(false));
		
		foreach (Type type in typeList)
		{
			TestContext.WriteLine($"Testing: {type.Name}");
			type.IsSealed.Should().BeTrue();
			type.IsPublic.Should().BeFalse();
			type.IsVisible.Should().BeFalse();
		}
	}
}
