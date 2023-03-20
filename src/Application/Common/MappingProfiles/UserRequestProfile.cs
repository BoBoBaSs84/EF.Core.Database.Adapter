using Application.Contracts.Requests.Identity;
using AutoMapper;
using Domain.Entities.Identity;

namespace Application.Common.MappingProfiles;

[SuppressMessage("Style", "IDE0058", Justification = "AutoMapper")]
internal sealed class UserRequestProfile : Profile
{
	public UserRequestProfile()
	{
		CreateMap<User, UserCreateRequest>()
			.ReverseMap();
	}
}
