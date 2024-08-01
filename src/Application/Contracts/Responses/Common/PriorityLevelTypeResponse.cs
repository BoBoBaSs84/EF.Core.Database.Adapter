using Application.Contracts.Responses.Common.Base;

using Domain.Enumerators;

namespace Application.Contracts.Responses.Common;

/// <summary>
/// The response class for the <see cref="PriorityLevelType"/> enumerator.
/// </summary>
/// <param name="priorityLevel">The priority level type.</param>
public sealed class PriorityLevelTypeResponse(PriorityLevelType priorityLevel) : EnumeratorBaseResponse<PriorityLevelType>(priorityLevel)
{ }
