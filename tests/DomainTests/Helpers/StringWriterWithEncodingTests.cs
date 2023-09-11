using System.Text;

using Domain.Helpers;

using FluentAssertions;

namespace DomainTests.Helpers;

[TestClass]
public sealed class StringWriterWithEncodingTests : DomainTestBase
{
	[TestMethod]
	public void StringWriterWithEncodingBaseTest()
	{
		StringWriterWithEncoding stringWriter = new();

		stringWriter.Encoding.Should().NotBe(Encoding.UTF8);
	}

	[TestMethod]
	public void StringWriterWithEncodingUtf8Test()
	{
		StringWriterWithEncoding stringWriter = new(Encoding.UTF8);

		stringWriter.Encoding.Should().Be(Encoding.UTF8);
	}
}