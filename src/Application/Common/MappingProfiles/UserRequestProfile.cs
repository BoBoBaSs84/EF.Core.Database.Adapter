using Application.Contracts.Requests.Identity;
using AutoMapper;
using Domain.Entities.Identity;

namespace Application.Common.MappingProfiles;

internal sealed class UserRequestProfile : Profile
{
	public UserRequestProfile()
	{
		CreateMap<User, UserCreateRequest>()
			.ReverseMap();
	}
}
