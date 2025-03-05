using AutoMapper;

using BB84.Home.Application.Contracts.Requests.Attendance;
using BB84.Home.Domain.Entities.Attendance;

namespace BB84.Home.Application.Profiles.Requests;

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
