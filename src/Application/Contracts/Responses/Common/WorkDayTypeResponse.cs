using BB84.Home.Application.Contracts.Responses.Common.Base;
using BB84.Home.Domain.Enumerators.Attendance;

namespace BB84.Home.Application.Contracts.Responses.Common;

/// <summary>
/// The work day types response.
/// </summary>
/// <inheritdoc/>
public sealed class WorkDayTypeResponse(WorkDayTypes enumValue) : EnumeratorBaseResponse<WorkDayTypes>(enumValue)
{ }
