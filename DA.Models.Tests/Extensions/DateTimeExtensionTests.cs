﻿using DA.Base.Tests.Helpers;
using DA.Models.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using static DA.Base.Tests.Constants;

namespace DA.Models.Tests.Extensions;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "UnitTest")]
public class DateTimeExtensionTests : EntitiesBaseTest
{
	[TestMethod, Owner(Bobo)]
	public void ToSqlDateSuccessTest()
	{
		DateTime dateTimeNow = DateTime.Now,
			sqlDate;

		sqlDate = dateTimeNow.ToSqlDate();

		AssertionHelper.AssertInScope(() =>
		{
			sqlDate.Hour.Should().Be(0);
			sqlDate.Minute.Should().Be(0);
			sqlDate.Second.Should().Be(0);
			sqlDate.Year.Should().Be(dateTimeNow.Year);
			sqlDate.Month.Should().Be(dateTimeNow.Month);
			sqlDate.Day.Should().Be(dateTimeNow.Day);
		});
	}

	[TestMethod, Owner(Bobo)]
	public void ToSqlDateFailTest()
	{
		DateTime dateTimeNow = DateTime.Now;

		dateTimeNow.ToSqlDate();

		AssertionHelper.AssertInScope(() =>
		{
			dateTimeNow.Hour.Should().NotBe(0);
			dateTimeNow.Minute.Should().NotBe(0);
			dateTimeNow.Second.Should().NotBe(0);
		});
	}
}