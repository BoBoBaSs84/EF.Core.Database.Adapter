using Application.Contracts.Responses.Common;

using AutoMapper;

using Domain.Enumerators;
using Domain.Models.Common;

namespace Application.Common.MappingProfiles.Responses;

[SuppressMessage("Style", "IDE0058", Justification = "AutoMapper")]
internal sealed class CalendarResponseProfile : Profile
{
	public CalendarResponseProfile()
	{
		CreateMap<CalendarDay, CalendarDayResponse>()
			.ForMember(dst => dst.DayType, opt => opt.MapFrom(src => GetDayType(src.Date)));
	}

	private static DayType GetDayType(DateTime dateTime)
		=> dateTime.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday ? DayType.WEEKENDDAY : DayType.WEEKDAY;
}
