using Application.Contracts.Requests.Attendance;

using AutoMapper;

using BB84.Home.Domain.Entities.Attendance;

namespace Application.Profiles.Requests;

/// <summary>
/// The attendance request profile class.
/// </summary>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, auto mapper profile.")]
internal sealed class AttendanceRequestProfile : Profile
{
	public AttendanceRequestProfile()
	{
		CreateMap<AttendanceCreateRequest, AttendanceEntity>();
		CreateMap<AttendanceUpdateRequest, AttendanceEntity>();
	}
}
