using System.ComponentModel.DataAnnotations;

using BaseTests.Helpers;

using Domain.Common;
using Domain.Extensions;

using FluentAssertions;

using static BaseTests.Constants.TestConstants;

namespace DomainTests.Enumerators;

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
			IEnumerable<Enum> enums = GetEnums(type);
			foreach (Enum e in enums)
			{
				AssertionHelper.AssertInScope(() =>
				{
					AttributeHelper.FieldHasAttribute<DisplayAttribute>(e.GetTypeCode().GetFieldInfo()).Should().BeTrue();
					e.GetTypeCode().GetDisplayAttribute().Should().NotBeNull();
					e.GetTypeCode().GetDisplayAttribute()!.ResourceType.Should().NotBeNull();
					e.GetTypeCode().GetDescription().Should().NotBeNullOrWhiteSpace();
					e.GetTypeCode().GetShortName().Should().NotBeNullOrWhiteSpace();
					e.GetTypeCode().GetName().Should().NotBeNullOrWhiteSpace();
				});
			}
		}
	}

	private static IEnumerable<Type> GetEnumTypes()
		=> TypeHelper.GetAssemblyTypes(
			assembly: typeof(IDomainAssemblyMarker).Assembly,
			expression: x => x.IsEnum.Equals(true) && x.BaseType is not null && x.BaseType.Equals(typeof(Enum))
			);

	private static IEnumerable<Enum> GetEnums(Type type)
		=> Enum.GetValues(type).Cast<Enum>().ToList();
}
