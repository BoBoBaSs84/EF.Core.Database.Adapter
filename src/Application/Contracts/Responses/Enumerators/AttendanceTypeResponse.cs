using Application.Contracts.Responses.Base;

using Domain.Enumerators;

namespace Application.Contracts.Responses.Enumerators;

/// <summary>
/// The response class for the <see cref="AttendanceType"/> enumerator.
/// </summary>
/// <param name="attendance">The attendance type.</param>
public sealed class AttendanceTypeResponse(AttendanceType attendance) : EnumeratorResponse<AttendanceType>(attendance)
{ }
