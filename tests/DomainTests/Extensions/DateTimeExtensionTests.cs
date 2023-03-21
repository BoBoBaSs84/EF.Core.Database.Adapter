using Domain.Extensions;
using FluentAssertions;
using static BaseTests.Constants.TestConstants;
using static BaseTests.Helpers.AssertionHelper;

namespace DomainTests.Extensions;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "UnitTest")]
public class DateTimeExtensionTests : DomainBaseTest
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