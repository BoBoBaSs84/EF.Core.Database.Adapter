using BB84.Home.Application.Contracts.Responses.Common.Base;
using BB84.Home.Domain.Enumerators.Attendance;

namespace BB84.Home.Application.Contracts.Responses.Common;

/// <summary>
/// The attendance type response.
/// </summary>
/// <inheritdoc/>
public sealed class AttendanceTypeResponse(AttendanceType enumValue) : EnumeratorBaseResponse<AttendanceType>(enumValue)
{ }
