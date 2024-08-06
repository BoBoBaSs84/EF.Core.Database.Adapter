using Application.Contracts.Responses.Common.Base;

using Domain.Enumerators;

namespace Application.Contracts.Responses.Common;

/// <summary>
/// The priority level type response.
/// </summary>
/// <inheritdoc/>
public sealed class PriorityLevelTypeResponse(PriorityLevelType enumValue) : EnumeratorBaseResponse<PriorityLevelType>(enumValue)
{ }
