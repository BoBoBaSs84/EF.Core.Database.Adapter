using System.ComponentModel.DataAnnotations;
using System.Reflection;

using BaseTests.Helpers;

using BB84.Extensions;
using BB84.Home.Domain.Tests;

using Domain.Common;

using FluentAssertions;

using static BaseTests.Constants.TestConstants;

namespace BB84.Home.Domain.Tests.Enumerators;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "UnitTest")]
public sealed class EnumeratorTests : DomainTestBase
{
	[TestMethod, Owner(Bobo)]
	public void AllEnumeratorsHaveDescriptionNameShortNameAndResourceTest()
	{
		IEnumerable<Type> types = GetEnumTypes();
		foreach (Type type in types)
		{
			FieldInfo[] fields = type.GetFields();
			foreach (FieldInfo field in fields.Where(x => x.IsLiteral.IsTrue() && x.IsPublic.IsTrue()))
			{
				AssertionHelper.AssertInScope(() =>
				{
					AttributeHelper.FieldHasAttribute<DisplayAttribute>(field).Should().BeTrue();
					DisplayAttribute attr = field.GetCustomAttribute<DisplayAttribute>()!;
					attr.ResourceType.Should().NotBeNull();
					attr.Description.Should().NotBeNullOrEmpty();
					attr.ShortName.Should().NotBeNullOrEmpty();
					attr.Name.Should().NotBeNullOrEmpty();
				});
			}
		}
	}

	private static IEnumerable<Type> GetEnumTypes()
		=> TypeHelper.GetAssemblyTypes(
			assembly: typeof(IDomainAssemblyMarker).Assembly,
			expression: x => x.IsEnum.Equals(true) && x.BaseType is not null && x.BaseType.Equals(typeof(Enum))
			);
}
