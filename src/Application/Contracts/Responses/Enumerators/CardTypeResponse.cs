using Application.Contracts.Responses.Base;

using Domain.Enumerators;

namespace Application.Contracts.Responses.Enumerators;

/// <summary>
/// The card type response class.
/// </summary>
/// <remarks>
/// Initilizes an instance of the card type response class.
/// </remarks>
/// <inheritdoc/>
public sealed class CardTypeResponse(CardType enumValue) : EnumeratorResponse<CardType>(enumValue)
{ }
