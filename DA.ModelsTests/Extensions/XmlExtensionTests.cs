using DA.BaseTests.Helpers;
using DA.Models.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using static DA.BaseTests.Constants;
using static DA.BaseTests.Helpers.AssertionHelper;

namespace DA.ModelsTests.Extensions;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "UnitTest")]
public class XmlExtensionTests
{
	private readonly XmlWriterSettings _writerSettings = new();
	private readonly XmlReaderSettings _readerSettings = new();


	[DataTestMethod, Owner(Bobo)]
	[DataRow("utf-8"), DataRow("utf-16"), DataRow("utf-32")]
	public void ToXmlStringSuccessTest(string encodingString)
	{
		_writerSettings.Encoding = Encoding.GetEncoding(encodingString);

		FancyXmlTestClass testClass = new();

		string xmlString = testClass.ToXmlString(settings: _writerSettings);

		AssertInScope(() =>
		{
			xmlString.Should().Contain($"encoding=\"{encodingString}\"");
			xmlString.Should().Contain($@"{nameof(testClass.Id)}=""{testClass.Id}""");
			xmlString.Should().Contain($@"<{nameof(testClass.Name)}>{testClass.Name}");
			xmlString.Should().Contain($@"<{nameof(testClass.Description)}>{testClass.Description}");
		});
	}

	[TestMethod, Owner(Bobo)]
	public void ToXmlStringFailedTest()
	{
		FancyXmlTestClass testClass = new();

		string xmlString = testClass.ToXmlString();

		AssertInScope(() =>
		{
			xmlString.Should().NotContain($@"<{nameof(testClass.Id)}>""{testClass.Id}""");
			xmlString.Should().NotContain($@"{nameof(testClass.Name)}=""{testClass.Name}""");
			xmlString.Should().NotContain($@"{nameof(testClass.Description)}=""{testClass.Description}""");
		});
	}

	[TestMethod, Owner(Bobo)]
	public void FromXmlStringSuccessTest()
	{
		FancyXmlTestClass fancy = new();

		fancy = fancy.FromXmlString(XmlTextString);

		AssertInScope(() =>
		{
			fancy.Id.Should().Be("348798ee-12f2-4a20-b030-756bb6a4134d");
			fancy.Name.Should().Be("UnitTest");
			fancy.Description.Should().Be("UnitTestDescription");
		});
	}

	[TestMethod, Owner(Bobo)]
	public void FromXmlStringFailedTest()
	{
		FancyXmlTestClass fancy = new();

		fancy = fancy.FromXmlString(XmlTextString);

		AssertInScope(() =>
		{
			fancy.Id.Should().NotBe(Guid.NewGuid());
			fancy.Name.Should().NotBe(RandomHelper.GetString(10));
			fancy.Description.Should().NotBe(RandomHelper.GetString(40));
		});
	}

	[XmlRoot("Fancy")]
	public class FancyXmlTestClass
	{
		[XmlAttribute]
		public Guid Id { get; set; } = Guid.NewGuid();
		[XmlElement]
		public string Name { get; set; } = RandomHelper.GetString(10);
		[XmlElement]
		public string Description { get; set; } = RandomHelper.GetString(40);
	}

	private const string XmlTextString = @"<Fancy Id=""348798ee-12f2-4a20-b030-756bb6a4134d""><Name>UnitTest</Name><Description>UnitTestDescription</Description></Fancy>";
}