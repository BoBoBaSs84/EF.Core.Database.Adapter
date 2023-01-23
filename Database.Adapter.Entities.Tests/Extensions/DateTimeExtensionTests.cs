using Database.Adapter.Entities.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

namespace Database.Adapter.Entities.Tests.Extensions;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "UnitTest")]
public class DateTimeExtensionTests : EntitiesBaseTest
{
	[TestMethod]
	public void ToSqlDateSuccessTest()
	{
		DateTime dateTimeNow = DateTime.Now,
			sqlDate;

		sqlDate = dateTimeNow.ToSqlDate();

		sqlDate.Hour.Should().Be(0);
		sqlDate.Minute.Should().Be(0);
		sqlDate.Second.Should().Be(0);
		sqlDate.Year.Should().Be(dateTimeNow.Year);
		sqlDate.Month.Should().Be(dateTimeNow.Month);
		sqlDate.Day.Should().Be(dateTimeNow.Day);
	}

	[TestMethod]
	public void ToSqlDateFailTest()
	{
		DateTime dateTimeNow = DateTime.Now;

		dateTimeNow.ToSqlDate();

		dateTimeNow.Hour.Should().NotBe(0);
		dateTimeNow.Minute.Should().NotBe(0);
		dateTimeNow.Second.Should().NotBe(0);
	}
}