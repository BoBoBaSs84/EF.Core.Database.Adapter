using Database.Adapter.Base.Tests.Helpers;
using Database.Adapter.Entities.Enumerators;
using Database.Adapter.Entities.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Database.Adapter.Entities.Tests.Enumerators;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "UnitTest")]
public class CardTypeTests
{
	private readonly ICollection<CardType> _cardType = CardType.CREDIT.GetListFromEnum();

	[TestMethod()]
	public void AllCardTypeEnumeratorsHaveDescriptionTest()
	{
		foreach (CardType e in _cardType)
		{
			string description = e.GetDescription();
			description.Should().NotBeNullOrWhiteSpace();
		}
	}

	[TestMethod()]
	public void AllCardTypeEnumeratorsHaveShortNameTest()
	{
		foreach (CardType e in _cardType)
		{
			string shortName = e.GetShortName();
			shortName.Should().NotBeNullOrWhiteSpace();
		}
	}

	[TestMethod()]
	public void AllCardTypeEnumeratorsHaveNameTest()
	{
		foreach (CardType e in _cardType)
		{
			string name = e.GetName();
			name.Should().NotBeNullOrWhiteSpace();
		}
	}

	[TestMethod()]
	public void AllCardTypeEnumeratorsHaveDisplayAttributeTest()
	{
		foreach (CardType e in _cardType)
			AttributeHelper.FieldHasAttribute<DisplayAttribute>(e.GetFieldInfo()).Should().BeTrue();
	}

	[TestMethod()]
	public void AllCardTypeEnumeratorsHaveResourceTypeTest()
	{
		foreach (CardType e in _cardType)
		{
			DisplayAttribute? displayAttribute = e.GetDisplayAttribute();
			displayAttribute.Should().NotBeNull();
			displayAttribute!.ResourceType.Should().NotBeNull();
		}
	}
}
