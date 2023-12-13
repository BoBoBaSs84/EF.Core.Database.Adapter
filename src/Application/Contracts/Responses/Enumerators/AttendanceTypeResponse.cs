using Application.Contracts.Responses.Base;

using Domain.Enumerators;

namespace Application.Contracts.Responses.Enumerators;

/// <summary>
/// The attendance type response class.
/// </summary>
/// <remarks>
/// Initilizes an instance of the attendance type response class.
/// </remarks>
/// <inheritdoc/>
public sealed class AttendanceTypeResponse(AttendanceType enumValue) : EnumeratorResponse<AttendanceType>(enumValue)
{ }
