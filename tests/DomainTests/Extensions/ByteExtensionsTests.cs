using Domain.Extensions;

namespace DomainTests.Extensions;

[TestClass]
public class ByteExtensionsTests
{
	[TestMethod]
	public void GetHexStringTest()
	{
		byte[] inputBuffer = { 255, 255 };

		string outputString = inputBuffer.GetHexString();

		Assert.AreEqual("FFFF", outputString);
	}
}