using Application.Contracts.Responses.Base;

using Domain.Enumerators;

namespace Application.Contracts.Responses.Enumerators;

/// <summary>
/// The card type response class.
/// </summary>
public sealed class CardTypeResponse : EnumeratorResponse<CardType>
{
	/// <summary>
	/// Initilizes an instance of the card type response class.
	/// </summary>
	/// <inheritdoc/>
	public CardTypeResponse(CardType enumValue) : base(enumValue)
	{ }
}
