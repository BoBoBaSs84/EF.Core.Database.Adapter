using Database.Adapter.Entities.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Database.Adapter.Entities.Tests.Extensions;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "UnitTest")]
public class EnumeratorExtensionsTests
{
	[TestMethod]
	public void GetEnumDescriptionSuccessTest()
	{
		TestEnum testEnum = TestEnum.ALLGOOD;
		string description = testEnum.GetEnumDescription();
		description.Should().Be(nameof(description));
	}

	[TestMethod]
	public void GetEnumDescriptionFailTest()
	{
		TestEnum testEnum = TestEnum.NODESCRIPTION;
		string description = testEnum.GetEnumDescription();
		description.Should().NotBeSameAs(nameof(description));
	}

	[TestMethod]
	public void GetEnumNameSuccessTest()
	{
		TestEnum testEnum = TestEnum.ALLGOOD;
		string name = testEnum.GetEnumName();
		name.Should().Be(nameof(name));
	}

	[TestMethod]
	public void GetEnumNameFailTest()
	{
		TestEnum testEnum = TestEnum.NONAME;
		string name = testEnum.GetEnumName();
		name.Should().NotBeSameAs(nameof(name));
	}

	[TestMethod]
	public void GetEnumShortNameSuccessTest()
	{
		TestEnum testEnum = TestEnum.ALLGOOD;
		string shortname = testEnum.GetEnumShortName();
		shortname.Should().Be(nameof(shortname));
	}

	[TestMethod]
	public void GetEnumShortNameFailTest()
	{
		TestEnum testEnum = TestEnum.NOSHORTNAME;
		string shortname = testEnum.GetEnumShortName();
		shortname.Should().NotBeSameAs(nameof(shortname));
	}

	[TestMethod]
	public void GetEnumDisplayAttributeSuccessTest()
	{
		TestEnum testEnum = TestEnum.ALLGOOD;
		DisplayAttribute? displayAttribute = testEnum.GetDisplayAttribute();
		displayAttribute.Should().NotBeNull();
	}

	[TestMethod]
	public void GetEnumDisplayAttributeFailTest()
	{
		TestEnum testEnum = TestEnum.NODISPLAYATTRIBUTE;
		DisplayAttribute? displayAttribute = testEnum.GetDisplayAttribute();
		displayAttribute.Should().BeNull();
	}

	[TestMethod]
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