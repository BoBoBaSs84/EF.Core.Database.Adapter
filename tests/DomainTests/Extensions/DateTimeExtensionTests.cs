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

	[TestMethod()]
	public void StartOfWeekTest()
	{
		DateTime today = new(2023, 09, 05);

		DateTime startOfWeek = today.StartOfWeek();

		startOfWeek.Should().Be(new(2023, 09, 04));
	}

	[TestMethod()]
	public void EndOfWeekTest()
	{
		DateTime today = new(2023, 09, 05);

		DateTime endOfWeek = today.EndOfWeek();

		endOfWeek.Should().Be(new(2023, 09, 10));
	}
}