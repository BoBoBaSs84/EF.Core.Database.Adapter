﻿using System.ComponentModel.DataAnnotations;

using BB84.Home.Domain.Enumerators.Attendance;
using BB84.Home.Domain.Extensions;

using FluentAssertions;

using static BB84.Home.Base.Tests.Constants.TestConstants;

namespace BB84.Home.Domain.Tests.Extensions;

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
			new object[]{ AttendanceType.Workday },
			[AttendanceType.Absence],
			[AttendanceType.BuisnessTrip],
			[AttendanceType.MobileWorking],
			[AttendanceType.ShortTimeWork],
			[AttendanceType.VacationBlock],
			[AttendanceType.PlannedVacation],
		};

	private static IEnumerable<object[]> GetWorkingHoursNotRelevantAttendances
		=> new[]
		{
			new object[]{ AttendanceType.Holiday },
			[AttendanceType.Suspension],
			[AttendanceType.Sickness],
			[AttendanceType.Vacation],
		};
}