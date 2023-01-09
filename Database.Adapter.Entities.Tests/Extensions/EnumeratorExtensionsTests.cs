using Database.Adapter.Entities.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel.DataAnnotations;

namespace Database.Adapter.Entities.Tests.Extensions;

[TestClass]
public class EnumeratorExtensionsTests
{
	[TestMethod]
	public void GetEnumDescriptionSuccessTest()
	{
		TestEnum testEnum = TestEnum.ALLGOOD;
		string description = testEnum.GetEnumDescription();
		Assert.AreEqual(nameof(description), description);
	}

	[TestMethod]
	public void GetEnumDescriptionFailTest()
	{
		TestEnum testEnum = TestEnum.NODESCRIPTION;
		string description = testEnum.GetEnumDescription();
		Assert.AreNotEqual(nameof(description), description);
	}

	[TestMethod]
	public void GetEnumNameSuccessTest()
	{
		TestEnum testEnum = TestEnum.ALLGOOD;
		string name = testEnum.GetEnumName();
		Assert.AreEqual(nameof(name), name);
	}

	[TestMethod]
	public void GetEnumNameFailTest()
	{
		TestEnum testEnum = TestEnum.NONAME;
		string name = testEnum.GetEnumName();
		Assert.AreNotEqual(nameof(name), name);
	}

	[TestMethod]
	public void GetEnumShortNameSuccessTest()
	{
		TestEnum testEnum = TestEnum.ALLGOOD;
		string shortname = testEnum.GetEnumShortName();
		Assert.AreEqual(nameof(shortname), shortname);
	}

	[TestMethod]
	public void GetEnumShortNameFailTest()
	{
		TestEnum testEnum = TestEnum.NOSHORTNAME;
		string shortname = testEnum.GetEnumShortName();
		Assert.AreNotEqual(nameof(shortname), shortname);
	}

	[TestMethod]
	public void GetEnumDisplayAttributeSuccessTest()
	{
		TestEnum testEnum = TestEnum.ALLGOOD;
		DisplayAttribute? displayAttribute = testEnum.GetDisplayAttribute();
		Assert.IsNotNull(displayAttribute);
	}

	[TestMethod]
	public void GetEnumDisplayAttributeFailTest()
	{
		TestEnum testEnum = TestEnum.NODISPLAYATTRIBUTE;
		DisplayAttribute? displayAttribute = testEnum.GetDisplayAttribute();		
		Assert.IsNull(displayAttribute);
	}

	[TestMethod]
	public void GetEnumListSuccessTest()
	{
		TestEnum testEnum = TestEnum.ALLGOOD;
		List<TestEnum> enumList = testEnum.GetListFromEnum();
		Assert.IsNotNull(enumList);
		Assert.AreEqual(5, enumList.Count);
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