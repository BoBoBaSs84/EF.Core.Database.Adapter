using System.Text.Json.Serialization;

using Domain.Converters;
using Domain.Extensions;

using FluentAssertions;

namespace DomainTests.Converters;

[TestClass]
public class FlagsToArrayConverterTests
{
	[TestMethod]
	public void ReadNoValueTest()
	{
		string jsonString = "{}";

		TestClass testClass = new TestClass().FromJsonString(jsonString);

		testClass.Flag.Should().HaveFlag(TestFlag.None);
	}

	[TestMethod]
	public void ReadValueTest()
	{
		string jsonString = @"{""flag"":[""Complex""]}";

		TestClass testClass = new TestClass().FromJsonString(jsonString);

		testClass.Flag.Should().HaveFlag(TestFlag.Complex);
	}

	[TestMethod]
	public void ReadValuesTest()
	{
		string jsonString = @"{""flag"":[""Simple"",""Medium""]}";

		TestClass testClass = new TestClass().FromJsonString(jsonString);

		testClass.Flag.Should().HaveFlag(TestFlag.Simple | TestFlag.Medium);
	}

	[TestMethod]
	public void WriteNoValueTest()
	{
		TestClass testClass = new();

		string jsonString = testClass.ToJsonString();

		jsonString.Should().Be(@"{""flag"":[""None""]}");
	}

	[TestMethod]
	public void WriteValueTest()
	{
		TestClass testClass = new(TestFlag.Simple);

		string jsonString = testClass.ToJsonString();

		jsonString.Should().Be(@"{""flag"":[""Simple""]}");
	}

	[TestMethod]
	public void WriteValuesTest()
	{
		TestClass testClass = new(TestFlag.Simple | TestFlag.Medium);

		string jsonString = testClass.ToJsonString();

		jsonString.Should().Be(@"{""flag"":[""Simple"",""Medium""]}");
	}

	private class TestClass
	{
		public TestClass()
		{ }

		public TestClass(TestFlag flag)
			=> Flag = flag;

		[JsonConverter(typeof(FlagsToArrayJsonConverter))]
		public TestFlag Flag { get; set; }
	}

	[Flags]
	private enum TestFlag
	{
		None = 0,
		Simple = 1 << 0,
		Medium = 1 << 1,
		Complex = 1 << 2,
	}
}