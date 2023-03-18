using Application.Contracts.Responses;
using AutoMapper;
using Domain.Entities.Private;

namespace Application.Common.MappingProfiles;

internal sealed class CalendarResponseProfile : Profile
{
	public CalendarResponseProfile()
	{
		CreateMap<CalendarDay, CalendarResponse>()
			.ReverseMap();
	}
}
