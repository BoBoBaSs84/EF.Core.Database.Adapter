﻿using Application.Contracts.Responses.Enumerators;

using AutoMapper;

using Domain.Enumerators;

namespace Application.Common.MappingProfiles;

[SuppressMessage("Style", "IDE0058", Justification = "AutoMapper")]
internal static partial class ResponseProfiles
{
	/// <summary>
	/// The enumerator response profile class.
	/// </summary>
	internal sealed class EnumeratorResponseProfile : Profile
	{
		public EnumeratorResponseProfile()
		{
			CreateMap<AttendanceType, AttendanceTypeResponse>()
				.ConstructUsing(x => new(x));

			CreateMap<CardType, CardTypeResponse>()
				.ConstructUsing(x => new(x));

			CreateMap<RoleType, RoleTypeResponse>()
				.ConstructUsing(x => new(x));

			CreateMap<WorkDayTypes, WorkDayTypeResponse>()
				.ConstructUsing(x => new(x));
		}
	}
}
