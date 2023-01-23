﻿using Database.Adapter.Base.Tests.Helpers;
using Database.Adapter.Entities.Enumerators;
using Database.Adapter.Entities.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Database.Adapter.Entities.Tests.Enumerators;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "UnitTest")]
public class RoleTests : EntitiesBaseTest
{
	private readonly ICollection<RoleType> _dayTypes = RoleType.ADMINISTRATOR.GetListFromEnum();

	[TestMethod()]
	public void AllRoleTypeEnumeratorsHaveDescriptionTest()
	{
		foreach (RoleType e in _dayTypes)
		{
			TestContext.WriteLine($"Testing: {e}");
			string description = e.GetDescription();
			description.Should().NotBeNullOrWhiteSpace();
		}
	}

	[TestMethod()]
	public void AllRoleTypeEnumeratorsHaveShortNameTest()
	{
		foreach (RoleType e in _dayTypes)
		{
			TestContext.WriteLine($"Testing: {e}");
			string shortName = e.GetShortName();
			shortName.Should().NotBeNullOrWhiteSpace();
		}
	}

	[TestMethod()]
	public void AllRoleTypeEnumeratorsHaveNameTest()
	{
		foreach (RoleType e in _dayTypes)
		{
			TestContext.WriteLine($"Testing: {e}");
			string name = e.GetName();
			name.Should().NotBeNullOrWhiteSpace();
		}
	}

	[TestMethod()]
	public void AllRoleTypeEnumeratorsHaveDisplayAttributeTest()
	{
		foreach (RoleType e in _dayTypes)
		{
			TestContext.WriteLine($"Testing: {e}");
			AttributeHelper.FieldHasAttribute<DisplayAttribute>(e.GetFieldInfo()).Should().BeTrue();
		}
	}

	[TestMethod()]
	public void AllRoleTypeEnumeratorsHaveResourceTypeTest()
	{
		foreach (RoleType e in _dayTypes)
		{
			TestContext.WriteLine($"Testing: {e}");
			DisplayAttribute? displayAttribute = e.GetDisplayAttribute();
			displayAttribute.Should().NotBeNull();
			displayAttribute!.ResourceType.Should().NotBeNull();
		}
	}
}
