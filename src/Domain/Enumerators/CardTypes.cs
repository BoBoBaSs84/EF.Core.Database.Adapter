using Domain.Properties;
using System.ComponentModel.DataAnnotations;
using static Domain.Properties.Resources;

namespace Domain.Enumerators;

/// <summary>
/// The card type enumerator.
/// </summary>
public enum CardTypes
{
	/// <summary>
	/// The <see cref="CREDIT"/> card type.
	/// </summary>
	[Display(ResourceType = typeof(Resources),
		Name = nameof(CardType_Credit_Name),
		ShortName = nameof(CardType_Credit_ShortName),
		Description = nameof(CardType_Credit_Description))]
	CREDIT = 1,
	/// <summary>
	/// The <see cref="DEBIT"/> card type.
	/// </summary>
	[Display(ResourceType = typeof(Resources),
		Name = nameof(CardType_Debit_Name),
		ShortName = nameof(CardType_Debit_ShortName),
		Description = nameof(CardType_Debit_Description))]
	DEBIT
}
