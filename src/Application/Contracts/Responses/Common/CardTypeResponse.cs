using Application.Contracts.Responses.Common.Base;

using Domain.Enumerators.Finance;

namespace Application.Contracts.Responses.Common;

/// <summary>
/// The card type response class.
/// </summary>
/// <remarks>
/// Initilizes an instance of the card type response class.
/// </remarks>
/// <inheritdoc/>
public sealed class CardTypeResponse(CardType enumValue) : EnumeratorBaseResponse<CardType>(enumValue)
{ }
