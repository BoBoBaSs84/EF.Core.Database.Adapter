using Application.Contracts.Responses.Common.Base;

using BB84.Home.Domain.Enumerators.Attendance;

namespace Application.Contracts.Responses.Common;

/// <summary>
/// The work day types response.
/// </summary>
/// <inheritdoc/>
public sealed class WorkDayTypeResponse(WorkDayTypes enumValue) : EnumeratorBaseResponse<WorkDayTypes>(enumValue)
{ }
