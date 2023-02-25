using DA.Domain.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using static DA.BaseTests.Constants;

namespace DA.DomainTests.Extensions;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "UnitTest")]
public class EnumeratorExtensionsTests : ModelsBaseTest
{
	[TestMethod, Owner(Bobo)]
	public void GetEnumDescriptionSuccessTest()
	{
		TestEnum testEnum = TestEnum.ALLGOOD;
		string description = testEnum.GetDescription();
		description.Should().Be(nameof(description));
	}

	[TestMethod, Owner(Bobo)]
	public void GetEnumDescriptionFailTest()
	{
		TestEnum testEnum = TestEnum.NODESCRIPTION;
		string description = testEnum.GetDescription();
		description.Should().NotBeSameAs(nameof(description));
	}

	[TestMethod, Owner(Bobo)]
	public void GetEnumNameSuccessTest()
	{
		TestEnum testEnum = TestEnum.ALLGOOD;
		string name = testEnum.GetName();
		name.Should().Be(nameof(name));
	}

	[TestMethod, Owner(Bobo)]
	public void GetEnumNameFailTest()
	{
		TestEnum testEnum = TestEnum.NONAME;
		string name = testEnum.GetName();
		name.Should().NotBeSameAs(nameof(name));
	}

	[TestMethod, Owner(Bobo)]
	public void GetEnumShortNameSuccessTest()
	{
		TestEnum testEnum = TestEnum.ALLGOOD;
		string shortname = testEnum.GetShortName();
		shortname.Should().Be(nameof(shortname));
	}

	[TestMethod, Owner(Bobo)]
	public void GetEnumShortNameFailTest()
	{
		TestEnum testEnum = TestEnum.NOSHORTNAME;
		string shortname = testEnum.GetShortName();
		shortname.Should().NotBeSameAs(nameof(shortname));
	}

	[TestMethod, Owner(Bobo)]
	public void GetEnumDisplayAttributeSuccessTest()
	{
		TestEnum testEnum = TestEnum.ALLGOOD;
		DisplayAttribute? displayAttribute = testEnum.GetDisplayAttribute();
		displayAttribute.Should().NotBeNull();
	}

	[TestMethod, Owner(Bobo)]
	public void GetEnumDisplayAttributeFailTest()
	{
		TestEnum testEnum = TestEnum.NODISPLAYATTRIBUTE;
		DisplayAttribute? displayAttribute = testEnum.GetDisplayAttribute();
		displayAttribute.Should().BeNull();
	}

	[TestMethod, Owner(Bobo)]
	public void GetEnumListSuccessTest()
	{
		TestEnum testEnum = TestEnum.ALLGOOD;
		List<TestEnum> enumList = testEnum.GetListFromEnum();
		enumList.Should().NotBeNullOrEmpty();
		enumList.Should().HaveCount(5);
	}

	private enum TestEnum
	{
		NODISPLAYATTRIBUTE = 0,
		[Display(Description = "description", ShortName = "shortname")]
		NONAME,
		[Display(Description = "description", Name = "name")]
		NOSHORTNAME,
		[Display(Name = "name", ShortName = "shortname")]
		NODESCRIPTION,
		[Display(Description = "description", Name = "name", ShortName = "shortname")]
		ALLGOOD
	}
}