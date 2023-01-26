using DA.Models.Properties;
using System.ComponentModel.DataAnnotations;
using static DA.Models.Properties.EnumeratorResources;

namespace DA.Models.Enumerators;

/// <summary>
/// The card type enumerator.
/// </summary>
public enum CardType
{
	/// <summary>
	/// The <see cref="CREDIT"/> card type.
	/// </summary>
	[Display(ResourceType = typeof(EnumeratorResources),
		Name = nameof(CardType_Credit_Name),
		ShortName = nameof(CardType_Credit_ShortName),
		Description = nameof(CardType_Credit_Description))]
	CREDIT = 1,
	/// <summary>
	/// The <see cref="DEBIT"/> card type.
	/// </summary>
	[Display(ResourceType = typeof(EnumeratorResources),
		Name = nameof(CardType_Debit_Name),
		ShortName = nameof(CardType_Debit_ShortName),
		Description = nameof(CardType_Debit_Description))]
	DEBIT
}
