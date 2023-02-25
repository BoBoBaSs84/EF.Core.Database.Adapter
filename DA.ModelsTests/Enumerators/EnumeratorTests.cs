using DA.BaseTests.Helpers;
using DA.Domain.Common;
using DA.Domain.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using static DA.BaseTests.Constants;

namespace DA.DomainTests.Enumerators;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "UnitTest")]
public sealed class EnumeratorTests : ModelsBaseTest
{
	[TestMethod, Owner(Bobo)]
	public void AllEnumeratorsHaveDescriptionNameShortNameAndResourceTest()
	{
		IEnumerable<Type> types = GetEnumTypes();
		foreach (Type type in types)
		{
			TestContext.WriteLine($"Testing enum: {type.Name}");
			IEnumerable<Enum> enums = GetEnums(type);
			foreach (Enum e in enums)
			{
				TestContext.WriteLine($"Value: {e}");

				AssertionHelper.AssertInScope(() =>
				{
					AttributeHelper.FieldHasAttribute<DisplayAttribute>(e.GetFieldInfo()).Should().BeTrue();
					e.GetDisplayAttribute().Should().NotBeNull();
					e.GetDisplayAttribute()!.ResourceType.Should().NotBeNull();
					e.GetDescription().Should().NotBeNullOrWhiteSpace();
					e.GetShortName().Should().NotBeNullOrWhiteSpace();
					e.GetName().Should().NotBeNullOrWhiteSpace();
				});
			}
		}
	}

	private static IEnumerable<Type> GetEnumTypes()
	{
		Assembly assembly = typeof(IModelsAssemblyMarker).Assembly;
		return TypeHelper.GetAssemblyTypes(
			assembly: assembly,
			expression: x => x.IsEnum.Equals(true) && x.BaseType is not null && x.BaseType.Equals(typeof(Enum))
			);
	}

	private static IEnumerable<Enum> GetEnums(Type type) =>
		Enum.GetValues(type).Cast<Enum>().ToList();
}
