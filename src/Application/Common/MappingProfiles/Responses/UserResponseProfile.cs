using Application.Contracts.Responses.Identity;

using AutoMapper;

using Domain.Entities.Identity;

namespace Application.Common.MappingProfiles.Responses;

[SuppressMessage("Style", "IDE0058", Justification = "AutoMapper")]
internal sealed class UserResponseProfile : Profile
{
	public UserResponseProfile()
	{
		CreateMap<User, UserResponse>();
	}
}
