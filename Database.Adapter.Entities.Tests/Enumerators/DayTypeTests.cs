using Database.Adapter.Entities.Enumerators;
using Database.Adapter.Entities.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel.DataAnnotations;

namespace Database.Adapter.Entities.Tests.Enumerators;

[TestClass]
public class DayTypeTests
{
	[DataTestMethod()]
	[DataRow(DayType.Weekday, DisplayName = $"{nameof(AllDayTypeEnumeratorsHaveDescriptionTest)} - All day type enums must have a description.")]
	public void AllDayTypeEnumeratorsHaveDescriptionTest(DayType dayType)
	{
		List<DayType> enumList = dayType.GetListFromEnum();
		foreach (DayType e in enumList)
		{
			string description = e.GetEnumDescription();
			Assert.IsFalse(string.IsNullOrWhiteSpace(description));
		}
	}

	[DataTestMethod]
	[DataRow(DayType.Weekday, DisplayName = $"{nameof(AllDayTypeEnumeratorsHaveShortNameTest)} - All day type enums must have a short name")]
	public void AllDayTypeEnumeratorsHaveShortNameTest(DayType dayType)
	{
		List<DayType> enumList = dayType.GetListFromEnum();
		foreach (DayType e in enumList)
		{
			string shortName = e.GetEnumShortName();
			Assert.IsFalse(string.IsNullOrWhiteSpace(shortName));
		}
	}

	[DataTestMethod]
	[DataRow(DayType.Weekday, DisplayName = $"{nameof(AllDayTypeEnumeratorsHaveNameTest)} - All day type enums must have a name.")]
	public void AllDayTypeEnumeratorsHaveNameTest(DayType dayType)
	{
		List<DayType> enumList = dayType.GetListFromEnum();
		foreach (DayType e in enumList)
		{
			string name = e.GetEnumName();
			Assert.IsFalse(string.IsNullOrWhiteSpace(name));
		}
	}

	[DataTestMethod]
	[DataRow(DayType.Weekday, DisplayName = $"{nameof(AllDayTypeEnumeratorsHaveDisplayAttributeTest)} - All day type enums must have a display attribute.")]
	public void AllDayTypeEnumeratorsHaveDisplayAttributeTest(DayType dayType)
	{
		List<DayType> enumList = dayType.GetListFromEnum();
		foreach (DayType e in enumList)
		{
			DisplayAttribute? displayAttribute = e.GetDisplayAttribute();
			Assert.IsNotNull(displayAttribute);
		}
	}

	[DataTestMethod]
	[DataRow(DayType.Weekday, DisplayName = $"{nameof(AllDayTypeEnumeratorsHaveResourceTypeTest)} - All day type enums must have a resource type defined.")]
	public void AllDayTypeEnumeratorsHaveResourceTypeTest(DayType dayType)
	{
		List<DayType> enumList = dayType.GetListFromEnum();
		foreach (DayType e in enumList)
		{
			DisplayAttribute? displayAttribute = e.GetDisplayAttribute();
			Assert.IsNotNull(displayAttribute);
			Assert.IsNotNull(displayAttribute.ResourceType);
		}
	}
}
