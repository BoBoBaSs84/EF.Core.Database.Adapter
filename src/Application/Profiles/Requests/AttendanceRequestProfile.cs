using Application.Contracts.Requests.Attendance;

using AutoMapper;

using Domain.Models.Attendance;

namespace Application.Profiles.Requests;

/// <summary>
/// The attendance request profile class.
/// </summary>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, auto mapper profile.")]
internal sealed class AttendanceRequestProfile : Profile
{
	public AttendanceRequestProfile()
	{
		CreateMap<AttendanceCreateRequest, AttendanceModel>();
		CreateMap<AttendanceUpdateRequest, AttendanceModel>();
	}
}
