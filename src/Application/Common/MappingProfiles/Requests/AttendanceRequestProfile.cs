using Application.Contracts.Requests;
using AutoMapper;
using Domain.Entities.Private;

namespace Application.Common.MappingProfiles.Requests;

[SuppressMessage("Style", "IDE0058", Justification = "AutoMapper")]
internal sealed class AttendanceRequestProfile : Profile
{
	public AttendanceRequestProfile()
	{
		CreateMap<AttendanceCreateRequest, Attendance>();
		CreateMap<AttendanceUpdateRequest, Attendance>();
	}
}
