using Application.Contracts.Responses;
using AutoMapper;
using Domain.Entities.Private;

namespace Application.Common.MappingProfiles.Responses;

[SuppressMessage("Style", "IDE0058", Justification = "AutoMapper")]
internal sealed class CalendarResponseProfile : Profile
{
	public CalendarResponseProfile()
	{
		CreateMap<CalendarDay, CalendarDayResponse>();
	}
}
