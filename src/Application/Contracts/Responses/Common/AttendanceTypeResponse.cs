using Application.Contracts.Responses.Common.Base;

using Domain.Enumerators;

namespace Application.Contracts.Responses.Common;

/// <summary>
/// The response class for the <see cref="AttendanceType"/> enumerator.
/// </summary>
/// <param name="attendance">The attendance type.</param>
public sealed class AttendanceTypeResponse(AttendanceType attendance) : EnumeratorBaseResponse<AttendanceType>(attendance)
{ }
