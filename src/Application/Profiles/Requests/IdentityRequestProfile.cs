﻿using AutoMapper;

using BB84.Home.Application.Contracts.Requests.Identity;
using BB84.Home.Domain.Entities.Identity;

namespace BB84.Home.Application.Profiles.Requests;

/// <summary>
/// The identity request profile class.
/// </summary>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, auto mapper profile.")]
internal sealed class IdentityRequestProfile : Profile
{
	public IdentityRequestProfile()
	{
		CreateMap<UserCreateRequest, UserEntity>();
		CreateMap<UserUpdateRequest, UserEntity>();

		CreateMap<PreferencesRequest, PreferencesModel>();
		CreateMap<AttendancePreferencesRequest, AttendancePreferencesModel>();
	}
}
