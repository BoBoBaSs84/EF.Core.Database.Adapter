using System.ComponentModel.DataAnnotations;

using RESX = Domain.Properties.EnumeratorResources;

namespace Domain.Enumerators.Finance;

/// <summary>
/// The card type enumerator.
/// </summary>
public enum CardType : byte
{
	/// <summary>
	/// The <see cref="CREDIT"/> card type.
	/// </summary>
	[Display(ResourceType = typeof(RESX),
		Name = nameof(RESX.CardType_Credit_Name),
		ShortName = nameof(RESX.CardType_Credit_ShortName),
		Description = nameof(RESX.CardType_Credit_Description))]
	CREDIT = 1,
	/// <summary>
	/// The <see cref="DEBIT"/> card type.
	/// </summary>
	[Display(ResourceType = typeof(RESX),
		Name = nameof(RESX.CardType_Debit_Name),
		ShortName = nameof(RESX.CardType_Debit_ShortName),
		Description = nameof(RESX.CardType_Debit_Description))]
	DEBIT
}
