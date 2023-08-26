using Application.Contracts.Responses;

using AutoMapper;

using Domain.Entities.Private;

namespace Application.Common.MappingProfiles.Responses;

[SuppressMessage("Style", "IDE0058", Justification = "AutoMapper")]
internal sealed class AttendanceResponseProfile : Profile
{
	public AttendanceResponseProfile()
	{
		CreateMap<Attendance, AttendanceResponse>()
			.ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.CalendarDay.Date))
			.ForMember(dest => dest.DayType, opt => opt.MapFrom(src => src.DayType.Name));
	}
}
