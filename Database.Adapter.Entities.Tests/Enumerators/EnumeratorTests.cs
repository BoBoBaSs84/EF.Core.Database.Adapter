﻿using Database.Adapter.Base.Tests.Helpers;
using Database.Adapter.Entities.Common;
using Database.Adapter.Entities.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Database.Adapter.Entities.Tests.Enumerators;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "UnitTest")]
public sealed class EnumeratorTests : EntitiesBaseTest
{
	[TestMethod]
	public void AllEnumeratorsHaveDisplayAttributeTest()
	{
		ICollection<Type> types = GetEnumTypes();
		foreach (Type type in types)
		{
			TestContext.WriteLine($"Testing: {type.Name}");
			ICollection<Enum> enums = GetEnums(type);
			foreach (Enum e in enums)
			{
				TestContext.WriteLine($"Testing: {e}");
				AttributeHelper.FieldHasAttribute<DisplayAttribute>(e.GetFieldInfo()).Should().BeTrue();
			}
		}
	}

	[TestMethod]
	public void AllEnumeratorsHaveResourceTypeTest()
	{
		ICollection<Type> types = GetEnumTypes();
		foreach (Type type in types)
		{
			TestContext.WriteLine($"Testing: {type.Name}");
			ICollection<Enum> enums = GetEnums(type);
			foreach (Enum e in enums)
			{
				TestContext.WriteLine($"Testing: {e}");
				DisplayAttribute? displayAttribute = e.GetDisplayAttribute();
				displayAttribute.Should().NotBeNull();
				displayAttribute!.ResourceType.Should().NotBeNull();
			}
		}
	}

	[TestMethod]
	public void AllEnumeratorsHaveDescriptionTest()
	{
		ICollection<Type> types = GetEnumTypes();
		foreach (Type type in types)
		{
			TestContext.WriteLine($"Testing: {type.Name}");
			ICollection<Enum> enums = GetEnums(type);
			foreach (Enum e in enums)
			{
				TestContext.WriteLine($"Testing: {e}");
				string description = e.GetDescription();
				description.Should().NotBeNullOrWhiteSpace();
			}
		}
	}

	[TestMethod]
	public void AllEnumeratorsHaveShortNameTest()
	{
		ICollection<Type> types = GetEnumTypes();
		foreach (Type type in types)
		{
			TestContext.WriteLine($"Testing: {type.Name}");
			ICollection<Enum> enums = GetEnums(type);
			foreach (Enum e in enums)
			{
				TestContext.WriteLine($"Testing: {e}");
				string description = e.GetShortName();
				description.Should().NotBeNullOrWhiteSpace();
			}
		}
	}

	[TestMethod]
	public void AllEnumeratorsHaveNameTest()
	{
		ICollection<Type> types = GetEnumTypes();
		foreach (Type type in types)
		{
			TestContext.WriteLine($"Testing: {type.Name}");
			ICollection<Enum> enums = GetEnums(type);
			foreach (Enum e in enums)
			{
				TestContext.WriteLine($"Testing: {e}");
				string description = e.GetName();
				description.Should().NotBeNullOrWhiteSpace();
			}
		}
	}

	private static ICollection<Type> GetEnumTypes()
	{
		Assembly assembly = typeof(IAssemblyMarker).Assembly;
		return TypeHelper.GetAssemblyTypes(
			assembly: assembly,
			expression: x => x.IsEnum.Equals(true) && x.BaseType is not null && x.BaseType.Equals(typeof(Enum))
			);
	}

	private static ICollection<Enum> GetEnums(Type type) =>
		Enum.GetValues(type).Cast<Enum>().ToList();
}
