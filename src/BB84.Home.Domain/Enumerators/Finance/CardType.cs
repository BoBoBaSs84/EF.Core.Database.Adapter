using System.ComponentModel.DataAnnotations;

using RESX = BB84.Home.Domain.Properties.EnumeratorResources;

namespace BB84.Home.Domain.Enumerators.Finance;

/// <summary>
/// The card type enumerator.
/// </summary>
public enum CardType : byte
{
	/// <summary>
	/// The <see cref="Credit"/> card type.
	/// </summary>
	[Display(ResourceType = typeof(RESX),
		Name = nameof(RESX.CardType_Credit_Name),
		ShortName = nameof(RESX.CardType_Credit_ShortName),
		Description = nameof(RESX.CardType_Credit_Description))]
	Credit = 1,
	/// <summary>
	/// The <see cref="Debit"/> card type.
	/// </summary>
	[Display(ResourceType = typeof(RESX),
		Name = nameof(RESX.CardType_Debit_Name),
		ShortName = nameof(RESX.CardType_Debit_ShortName),
		Description = nameof(RESX.CardType_Debit_Description))]
	Debit
}
