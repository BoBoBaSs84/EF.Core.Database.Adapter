using Application.Contracts.Responses.Common.Base;

using BB84.Home.Domain.Enumerators.Todo;

namespace Application.Contracts.Responses.Common;

/// <summary>
/// The priority level type response.
/// </summary>
/// <inheritdoc/>
public sealed class PriorityLevelTypeResponse(PriorityLevelType enumValue) : EnumeratorBaseResponse<PriorityLevelType>(enumValue)
{ }
