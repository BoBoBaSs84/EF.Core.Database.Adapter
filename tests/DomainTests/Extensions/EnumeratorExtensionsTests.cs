﻿using System.ComponentModel.DataAnnotations;

using Domain.Enumerators;
using Domain.Extensions;

using FluentAssertions;

using static BaseTests.Constants.TestConstants;

namespace DomainTests.Extensions;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "UnitTest")]
public class EnumeratorExtensionsTests : DomainTestBase
{
	[TestMethod, Owner(Bobo)]
	public void GetEnumDescriptionSuccessTest()
	{
		TestEnum testEnum = TestEnum.ALLGOOD;
		string description = testEnum.GetDescription();
		description.Should().Be(nameof(description));
	}

	[TestMethod, Owner(Bobo)]
	public void GetEnumDescriptionFailTest()
	{
		TestEnum testEnum = TestEnum.NODESCRIPTION;
		string description = testEnum.GetDescription();
		description.Should().NotBeSameAs(nameof(description));
	}

	[TestMethod, Owner(Bobo)]
	public void GetEnumNameSuccessTest()
	{
		TestEnum testEnum = TestEnum.ALLGOOD;
		string name = testEnum.GetName();
		name.Should().Be(nameof(name));
	}

	[TestMethod, Owner(Bobo)]
	public void GetEnumNameFailTest()
	{
		TestEnum testEnum = TestEnum.NONAME;
		string name = testEnum.GetName();
		name.Should().NotBeSameAs(nameof(name));
	}

	[TestMethod, Owner(Bobo)]
	public void GetEnumShortNameSuccessTest()
	{
		TestEnum testEnum = TestEnum.ALLGOOD;
		string shortname = testEnum.GetShortName();
		shortname.Should().Be(nameof(shortname));
	}

	[TestMethod, Owner(Bobo)]
	public void GetEnumShortNameFailTest()
	{
		TestEnum testEnum = TestEnum.NOSHORTNAME;
		string shortname = testEnum.GetShortName();
		shortname.Should().NotBeSameAs(nameof(shortname));
	}

	[TestMethod, Owner(Bobo)]
	public void GetEnumDisplayAttributeSuccessTest()
	{
		TestEnum testEnum = TestEnum.ALLGOOD;
		DisplayAttribute? displayAttribute = testEnum.GetDisplayAttribute();
		displayAttribute.Should().NotBeNull();
	}

	[TestMethod, Owner(Bobo)]
	public void GetEnumDisplayAttributeFailTest()
	{
		TestEnum testEnum = TestEnum.NODISPLAYATTRIBUTE;
		DisplayAttribute? displayAttribute = testEnum.GetDisplayAttribute();
		displayAttribute.Should().BeNull();
	}

	[TestMethod, Owner(Bobo)]
	public void GetEnumListSuccessTest()
	{
		TestEnum testEnum = TestEnum.ALLGOOD;
		List<TestEnum> enumList = testEnum.ToList();
		enumList.Should().NotBeNullOrEmpty();
		enumList.Should().HaveCount(5);
	}

	[TestMethod]
	[DynamicData(nameof(GetWorkingHoursRelevantAttendances))]
	public void IsWorkingHoursRelevantTrueTest(AttendanceType type)
	{
		bool relevant = type.IsWorkingHoursRelevant();

		Assert.IsTrue(relevant);
	}

	[TestMethod]
	[DynamicData(nameof(GetWorkingHoursNotRelevantAttendances))]
	public void IsWorkingHoursRelevantFalseTest(AttendanceType type)
	{
		bool relevant = type.IsWorkingHoursRelevant();

		Assert.IsFalse(relevant);
	}

	private enum TestEnum
	{
		NODISPLAYATTRIBUTE = 0,
		[Display(Description = "description", ShortName = "shortname")]
		NONAME,
		[Display(Description = "description", Name = "name")]
		NOSHORTNAME,
		[Display(Name = "name", ShortName = "shortname")]
		NODESCRIPTION,
		[Display(Description = "description", Name = "name", ShortName = "shortname")]
		ALLGOOD
	}

	private static IEnumerable<object[]> GetWorkingHoursRelevantAttendances
		=> new[]
		{
			new object[]{ AttendanceType.WORKDAY },
			new object[]{ AttendanceType.ABSENCE },
			new object[]{ AttendanceType.BUISNESSTRIP },
			new object[]{ AttendanceType.MOBILEWORKING },
			new object[]{ AttendanceType.SHORTTIMEWORK },
			new object[]{ AttendanceType.VACATIONBLOCK },
			new object[]{ AttendanceType.PLANNEDVACATION },
		};

	private static IEnumerable<object[]> GetWorkingHoursNotRelevantAttendances
		=> new[]
		{
			new object[]{ AttendanceType.HOLIDAY },
			new object[]{ AttendanceType.SUSPENSION },
			new object[]{ AttendanceType.SICKNESS },
			new object[]{ AttendanceType.VACATION },
		};
}