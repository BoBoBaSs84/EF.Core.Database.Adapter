﻿using Application.Contracts.Responses.Attendance;

using AutoMapper;

using Domain.Entities.Attendance;
using Domain.Extensions;

namespace Application.Profiles.Responses;

/// <summary>
/// The attendance response profile class.
/// </summary>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, auto mapper profile.")]
internal sealed class AttendanceResponseProfile : Profile
{
	public AttendanceResponseProfile()
	{
		CreateMap<AttendanceEntity, AttendanceResponse>()
			.ForMember(dest => dest.WorkingHours, opt => opt.MapFrom(src => src.GetResultingWorkingHours()));
	}
}