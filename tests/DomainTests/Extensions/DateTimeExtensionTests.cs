using Domain.Extensions;

using FluentAssertions;

namespace DomainTests.Extensions;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "UnitTest")]
public class DateTimeExtensionTests : DomainTestBase
{
	[TestMethod]
	public void WeekOfYearTest()
	{
		DateTime today = new(2023, 9, 5);

		int weekOfYear = today.WeekOfYear();

		weekOfYear.Should().Be(36);
	}
}