using Application.Contracts.Responses.Common.Base;

using Domain.Enumerators;

namespace Application.Contracts.Responses.Common;

/// <summary>
/// The work day type response class.
/// </summary>
/// <remarks>
/// Initilizes an instance of the work day type response class.
/// </remarks>
/// <inheritdoc/>
public sealed class WorkDayTypeResponse(WorkDayTypes enumValue) : EnumeratorBaseResponse<WorkDayTypes>(enumValue)
{ }
