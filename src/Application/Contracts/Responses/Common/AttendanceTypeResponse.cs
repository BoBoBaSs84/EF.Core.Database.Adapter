using Application.Contracts.Responses.Common.Base;

using Domain.Enumerators.Attendance;

namespace Application.Contracts.Responses.Common;

/// <summary>
/// The attendance type response.
/// </summary>
/// <inheritdoc/>
public sealed class AttendanceTypeResponse(AttendanceType enumValue) : EnumeratorBaseResponse<AttendanceType>(enumValue)
{ }
