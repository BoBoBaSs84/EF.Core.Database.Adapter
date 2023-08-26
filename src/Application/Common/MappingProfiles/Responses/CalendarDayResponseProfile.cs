using Application.Contracts.Responses;

using AutoMapper;

using Domain.Entities.Common;

namespace Application.Common.MappingProfiles.Responses;

[SuppressMessage("Style", "IDE0058", Justification = "AutoMapper")]
internal sealed class CalendarResponseProfile : Profile
{
	public CalendarResponseProfile()
	{
		CreateMap<CalendarDay, CalendarDayResponse>()
			.ForMember(dst => dst.DayType, opt => opt.MapFrom(src => src.DayType.Name));
	}
}
