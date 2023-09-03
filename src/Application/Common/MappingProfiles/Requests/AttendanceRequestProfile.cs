using Application.Contracts.Requests.Attendance;

using AutoMapper;

using Domain.Models.Attendance;

namespace Application.Common.MappingProfiles.Requests;

[SuppressMessage("Style", "IDE0058", Justification = "AutoMapper")]
internal sealed class AttendanceRequestProfile : Profile
{
	public AttendanceRequestProfile()
	{
		CreateMap<AttendanceCreateRequest, AttendanceModel>();
		CreateMap<AttendanceUpdateRequest, AttendanceModel>();
	}
}
