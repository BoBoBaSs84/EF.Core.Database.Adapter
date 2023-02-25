using DA.BaseTests.Helpers;
using DA.Models.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using static DA.BaseTests.Constants;
using static DA.BaseTests.Helpers.AssertionHelper;

namespace DA.ModelsTests.Extensions;

[TestClass()]
[SuppressMessage("Style", "IDE0058", Justification = "UnitTest")]
public class JsonExtensionTests
{
	[TestMethod, Owner(Bobo)]
	public void ToJsonStringSuccessTest()
	{
		FancyJsonTestClass testClass = new();

		string jsonString = testClass.ToJsonString();

		AssertInScope(() =>
		{
			jsonString.Should().Contain($@"{nameof(testClass.Id)}"":""{testClass.Id}");
			jsonString.Should().Contain($@"{nameof(testClass.Name)}"":""{testClass.Name}");
			jsonString.Should().Contain($@"{nameof(testClass.Description)}"":""{testClass.Description}");
		});
	}

	[TestMethod, Owner(Bobo)]
	public void ToJsonStringFailedTest()
	{
		FancyJsonTestClass testClass = new();

		string jsonString = testClass.ToJsonString();

		AssertInScope(() =>
		{
			jsonString.Should().NotContain($@"id"":""{testClass.Id}");
			jsonString.Should().NotContain($@"name"":""{testClass.Name}");
			jsonString.Should().NotContain($@"description"":""{testClass.Description}");
		});
	}

	[TestMethod, Owner(Bobo)]
	public void FromJsonStringSuccessTest()
	{
		FancyJsonTestClass testClass = new();

		testClass = testClass.FromJsonString(JsonTestString);

		AssertInScope(() =>
		{
			testClass.Id.Should().Be("356e4b9a-09f9-4399-82c7-d78c02cefb48");
			testClass.Name.Should().Be("qkxTAlLXUs");
			testClass.Description.Should().Be("QGVaYoljjHDTHasFRlGhDfJSehDCUnLqLsqfFesN");
		});
	}

	[TestMethod, Owner(Bobo)]
	public void FromJsonStringFailedTest()
	{
		FancyJsonTestClass testClass = new();

		testClass = testClass.FromJsonString(JsonTestString);

		AssertInScope(() =>
		{
			testClass.Id.Should().NotBe(Guid.NewGuid());
			testClass.Name.Should().NotBe(RandomHelper.GetString(10));
			testClass.Description.Should().NotBe(RandomHelper.GetString(40));
		});
	}

	public class FancyJsonTestClass
	{
		[JsonPropertyName(nameof(Id))]
		public Guid Id { get; set; } = Guid.NewGuid();
		[JsonPropertyName(nameof(Name))]
		public string Name { get; set; } = RandomHelper.GetString(10);
		[JsonPropertyName(nameof(Description))]
		public string Description { get; set; } = RandomHelper.GetString(40);
	}

	private const string JsonTestString = @"{""Id"":""356e4b9a-09f9-4399-82c7-d78c02cefb48"",""Name"":""qkxTAlLXUs"",""Description"":""QGVaYoljjHDTHasFRlGhDfJSehDCUnLqLsqfFesN""}";
}