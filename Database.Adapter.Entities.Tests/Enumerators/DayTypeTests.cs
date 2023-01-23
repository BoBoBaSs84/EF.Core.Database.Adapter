using Database.Adapter.Base.Tests.Helpers;
using Database.Adapter.Entities.Enumerators;
using Database.Adapter.Entities.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Database.Adapter.Entities.Tests.Enumerators;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "UnitTest")]
public class DayTypeTests : EntitiesBaseTest
{
	private readonly ICollection<DayType> _dayTypes = DayType.WEEKDAY.GetListFromEnum();

	[TestMethod()]
	public void AllDayTypeEnumeratorsHaveDescriptionTest()
	{
		foreach (DayType e in _dayTypes)
		{
			TestContext.WriteLine($"Testing: {e}");
			string description = e.GetDescription();
			description.Should().NotBeNullOrWhiteSpace();
		}
	}

	[TestMethod()]
	public void AllDayTypeEnumeratorsHaveShortNameTest()
	{
		foreach (DayType e in _dayTypes)
		{
			TestContext.WriteLine($"Testing: {e}");
			string shortName = e.GetShortName();
			shortName.Should().NotBeNullOrWhiteSpace();
		}
	}

	[TestMethod()]
	public void AllDayTypeEnumeratorsHaveNameTest()
	{
		foreach (DayType e in _dayTypes)
		{
			TestContext.WriteLine($"Testing: {e}");
			string name = e.GetName();
			name.Should().NotBeNullOrWhiteSpace();
		}
	}

	[TestMethod()]
	public void AllDayTypeEnumeratorsHaveDisplayAttributeTest()
	{
		foreach (DayType e in _dayTypes)
		{
			TestContext.WriteLine($"Testing: {e}");
			AttributeHelper.FieldHasAttribute<DisplayAttribute>(e.GetFieldInfo()).Should().BeTrue();
		}
	}

	[TestMethod()]
	public void AllDayTypeEnumeratorsHaveResourceTypeTest()
	{
		foreach (DayType e in _dayTypes)
		{
			TestContext.WriteLine($"Testing: {e}");
			DisplayAttribute? displayAttribute = e.GetDisplayAttribute();
			displayAttribute.Should().NotBeNull();
			displayAttribute!.ResourceType.Should().NotBeNull();
		}
	}
}
