using DA.Models.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using static DA.BaseTests.Constants;
using static DA.BaseTests.Helpers.AssertionHelper;

namespace DA.ModelsTests.Extensions;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "UnitTest")]
public class DateTimeExtensionTests : ModelsBaseTest
{
	[TestMethod, Owner(Bobo)]
	public void ToSqlDateSuccessTest()
	{
		DateTime dateTimeNow = DateTime.Now,
			sqlDate;

		sqlDate = dateTimeNow.ToSqlDate();

		AssertInScope(() =>
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
		DateTime sqlDate = DateTime.Now.ToSqlDate();

		AssertInScope(() =>
		{
			sqlDate.Hour.Should().NotBe(1);
			sqlDate.Minute.Should().NotBe(1);
			sqlDate.Second.Should().NotBe(1);
		});
	}
}