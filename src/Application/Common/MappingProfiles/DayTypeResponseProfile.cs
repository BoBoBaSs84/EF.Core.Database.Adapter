using Application.Contracts.Responses;
using AutoMapper;
using Domain.Entities.Enumerator;

namespace Application.Common.MappingProfiles;

[SuppressMessage("Style", "IDE0058", Justification = "AutoMapper")]
internal sealed class DayTypeResponseProfile : Profile
{
	public DayTypeResponseProfile()
	{
		CreateMap<DayType, DayTypeResponse>()
			.ReverseMap();
	}
}
