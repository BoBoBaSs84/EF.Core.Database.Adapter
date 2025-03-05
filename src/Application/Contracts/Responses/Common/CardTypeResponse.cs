using Application.Contracts.Responses.Common.Base;

using BB84.Home.Domain.Enumerators.Finance;

namespace Application.Contracts.Responses.Common;

/// <summary>
/// The bank card type response.
/// </summary>
/// <inheritdoc/>
public sealed class CardTypeResponse(CardType enumValue) : EnumeratorBaseResponse<CardType>(enumValue)
{ }
