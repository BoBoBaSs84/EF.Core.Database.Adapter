using Database.Adapter.Entities.Enumerators;
using Database.Adapter.Entities.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Database.Adapter.Entities.Tests.Enumerators;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "UnitTest")]
public class DayTypeTests
{
	[DataTestMethod]
	[DataRow(DayType.WEEKDAY, DisplayName = $"{nameof(AllDayTypeEnumeratorsHaveDescriptionTest)} - All day type enums must have a description.")]
	public void AllDayTypeEnumeratorsHaveDescriptionTest(DayType dayType)
	{
		List<DayType> enumList = dayType.GetListFromEnum();
		foreach (DayType e in enumList)
		{
			string description = e.GetEnumDescription();
			description.Should().NotBeNullOrWhiteSpace();
		}
	}

	[DataTestMethod]
	[DataRow(DayType.WEEKDAY, DisplayName = $"{nameof(AllDayTypeEnumeratorsHaveShortNameTest)} - All day type enums must have a short name")]
	public void AllDayTypeEnumeratorsHaveShortNameTest(DayType dayType)
	{
		List<DayType> enumList = dayType.GetListFromEnum();
		foreach (DayType e in enumList)
		{
			string shortName = e.GetEnumShortName();
			shortName.Should().NotBeNullOrWhiteSpace();
		}
	}

	[DataTestMethod]
	[DataRow(DayType.WEEKDAY, DisplayName = $"{nameof(AllDayTypeEnumeratorsHaveNameTest)} - All day type enums must have a name.")]
	public void AllDayTypeEnumeratorsHaveNameTest(DayType dayType)
	{
		List<DayType> enumList = dayType.GetListFromEnum();
		foreach (DayType e in enumList)
		{
			string name = e.GetEnumName();
			name.Should().NotBeNullOrWhiteSpace();
		}
	}

	[DataTestMethod]
	[DataRow(DayType.WEEKDAY, DisplayName = $"{nameof(AllDayTypeEnumeratorsHaveDisplayAttributeTest)} - All day type enums must have a display attribute.")]
	public void AllDayTypeEnumeratorsHaveDisplayAttributeTest(DayType dayType)
	{
		List<DayType> enumList = dayType.GetListFromEnum();
		foreach (DayType e in enumList)
		{
			DisplayAttribute? displayAttribute = e.GetDisplayAttribute();
			displayAttribute.Should().NotBeNull();
		}
	}

	[DataTestMethod]
	[DataRow(DayType.WEEKDAY, DisplayName = $"{nameof(AllDayTypeEnumeratorsHaveResourceTypeTest)} - All day type enums must have a resource type defined.")]
	public void AllDayTypeEnumeratorsHaveResourceTypeTest(DayType dayType)
	{
		List<DayType> enumList = dayType.GetListFromEnum();
		foreach (DayType e in enumList)
		{
			DisplayAttribute? displayAttribute = e.GetDisplayAttribute();
			displayAttribute.Should().NotBeNull();
			displayAttribute!.ResourceType.Should().NotBeNull();
		}
	}
}
