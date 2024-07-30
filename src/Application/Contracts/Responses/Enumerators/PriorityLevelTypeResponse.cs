using Application.Contracts.Responses.Base;

using Domain.Enumerators;

namespace Application.Contracts.Responses.Enumerators;

/// <summary>
/// The response class for the <see cref="PriorityLevelType"/> enumerator.
/// </summary>
/// <param name="priorityLevel">The priority level type.</param>
public sealed class PriorityLevelTypeResponse(PriorityLevelType priorityLevel) : EnumeratorResponse<PriorityLevelType>(priorityLevel)
{ }
