using Application.Contracts.Requests.Identity;

using AutoMapper;

using Domain.Models.Identity;

namespace Application.Common.MappingProfiles;

[SuppressMessage("Style", "IDE0058", Justification = "AutoMapper")]
internal static partial class RequestProfiles
{
	/// <summary>
	/// The identity request profile class.
	/// </summary>
	internal sealed class IdentityRequestProfile : Profile
	{
		public IdentityRequestProfile()
		{
			CreateMap<UserCreateRequest, UserModel>();
			CreateMap<UserUpdateRequest, UserModel>()
				.ForMember(dest => dest.Picture, opt => opt.MapFrom(src => Convert.FromBase64String(src.Picture ?? string.Empty)));

			CreateMap<PreferencesRequest, PreferencesModel>();
			CreateMap<AttendancePreferencesRequest, AttendancePreferencesModel>();
		}
	}
}
