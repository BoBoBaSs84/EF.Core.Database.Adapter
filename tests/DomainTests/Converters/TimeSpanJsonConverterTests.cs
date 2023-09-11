using System.Text.Json.Serialization;

using Domain.Converters;
using Domain.Extensions;

using FluentAssertions;

namespace DomainTests.Converters;

[TestClass]
public class TimeSpanJsonConverterTests : DomainTestBase
{
	private readonly TimeSpan _time = new(6, 0, 0);

	[TestMethod]
	public void ReadValueTest()
	{
		string jsonString = @"{""time"":""06:00:00""}";

		TestClass testClass = new TestClass().FromJsonString(jsonString);

		testClass.Time.Should().Be(_time);
	}

	[TestMethod]
	public void ReadNoValueTest()
	{
		string jsonString = "{}";

		TestClass testClass = new TestClass().FromJsonString(jsonString);

		testClass.Time.Should().BeNull();
	}

	[TestMethod]
	public void WriteValueTest()
	{
		TestClass testClass = new(_time);

		string jsonString = testClass.ToJsonString();

		jsonString.Should().Be(@"{""time"":""06:00:00""}");
	}

	[TestMethod]
	public void WriteNoValueTest()
	{
		TestClass testClass = new(null);

		string jsonString = testClass.ToJsonString();

		jsonString.Should().Be("{}");
	}

	private class TestClass
	{
		public TestClass()
		{ }

		public TestClass(TimeSpan? time)
			=> Time = time;

		[JsonConverter(typeof(NullableTimeJsonConverter))]
		public TimeSpan? Time { get; set; }
	}
}