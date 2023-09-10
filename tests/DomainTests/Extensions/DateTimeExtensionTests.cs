using BaseTests.Helpers;

using Domain.Extensions;

using FluentAssertions;

namespace DomainTests.Extensions;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "UnitTest")]
public class DateTimeExtensionTests : DomainTestBase
{
	[TestMethod]
	public void ToSqlDateTest()
	{
		DateTime dateTime = DateTime.Now;

		DateTime date = dateTime.ToSqlDate();

		AssertionHelper.AssertInScope(() =>
		{
			date.Hour.Should().Be(0);
			date.Minute.Should().Be(0);
			date.Second.Should().Be(0);
			date.Millisecond.Should().Be(0);
		});
	}

	[TestMethod]
	public void ToSqlDateNullableTest()
	{
		DateTime? dateTime = DateTime.Now;

		DateTime? date = dateTime.ToSqlDate();

		AssertionHelper.AssertInScope(() =>
		{
			Assert.IsNotNull(date);
			date.Value.Hour.Should().Be(0);
			date.Value.Minute.Should().Be(0);
			date.Value.Second.Should().Be(0);
			date.Value.Millisecond.Should().Be(0);
		});
	}

	[TestMethod]
	public void StartOfWeekTest()
	{
		DateTime today = new(2023, 9, 5);

		DateTime startOfWeek = today.StartOfWeek();

		startOfWeek.Should().Be(new(2023, 9, 4));
	}

	[TestMethod]
	public void EndOfWeekTest()
	{
		DateTime today = new(2023, 9, 5);

		DateTime endOfWeek = today.EndOfWeek();

		endOfWeek.Should().Be(new(2023, 9, 10));
	}

	[TestMethod]
	public void StartOfMonthTest()
	{
		DateTime today = new(2023, 9, 5);

		DateTime startOfMonth = today.StartOfMonth();

		startOfMonth.Should().Be(new(2023, 9, 1));
	}

	[TestMethod]
	public void EndOfMonthTest()
	{
		DateTime today = new(2023, 9, 5);

		DateTime endOfMonth = today.EndOfMonth();

		endOfMonth.Should().Be(new(2023, 9, 30));
	}
}