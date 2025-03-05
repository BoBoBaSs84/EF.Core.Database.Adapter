using Application.Contracts.Responses.Identity;

using AutoMapper;

using BB84.Home.Domain.Entities.Identity;

namespace Application.Profiles.Responses;

/// <summary>
/// The identity response profile class.
/// </summary>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, auto mapper profile.")]
internal sealed class IdentityResponseProfile : Profile
{
	public IdentityResponseProfile()
	{
		CreateMap<UserEntity, UserResponse>();

		CreateMap<PreferencesModel, PreferencesResponse>();
		CreateMap<AttendancePreferencesModel, AttendancePreferencesResponse>();
	}
}
