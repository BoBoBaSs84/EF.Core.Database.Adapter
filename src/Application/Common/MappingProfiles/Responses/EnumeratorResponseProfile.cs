using Application.Contracts.Responses.Enumerators;

using AutoMapper;

using Domain.Enumerators;

namespace Application.Common.MappingProfiles.Responses;

[SuppressMessage("Style", "IDE0058", Justification = "AutoMapper")]
internal sealed class EnumeratorResponseProfile : Profile
{
	public EnumeratorResponseProfile()
	{
		CreateMap<DayType, DayTypeResponse>()
			.ConstructUsing(x => new(x));

		CreateMap<CardType, CardTypeResponse>()
			.ConstructUsing(x => new(x));
	}
}
