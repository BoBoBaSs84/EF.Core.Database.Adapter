using Application.Contracts.Responses.Common;

using AutoMapper;

using BB84.Extensions;

using Domain.Enumerators;
using Domain.Enumerators.Attendance;
using Domain.Enumerators.Documents;
using Domain.Enumerators.Finance;
using Domain.Enumerators.Todo;

namespace Application.Profiles.Responses;

/// <summary>
/// The common response profile class
/// </summary>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, auto mapper profile.")]
internal sealed class CommonResponseProfile : Profile
{
	public CommonResponseProfile()
	{
		CreateMap<DateTime, CalendarResponse>()
			.ForMember(t => t.Year, o => o.MapFrom(s => s.Date.Year))
			.ForMember(t => t.Month, o => o.MapFrom(s => s.Date.Month))
			.ForMember(t => t.Week, o => o.MapFrom(s => s.Date.WeekOfYear()))
			.ForMember(t => t.DayOfWeek, o => o.MapFrom(s => s.Date.DayOfWeek))
			.ForMember(t => t.DayOfYear, o => o.MapFrom(s => s.Date.DayOfYear))
			.ForMember(t => t.StartOfWeek, o => o.MapFrom(s => s.Date.StartOfWeek(DayOfWeek.Monday)))
			.ForMember(t => t.EndOfWeek, o => o.MapFrom(s => s.Date.EndOfWeek(DayOfWeek.Monday)))
			.ForMember(t => t.StartOfMonth, o => o.MapFrom(s => s.Date.StartOfMonth()))
			.ForMember(t => t.EndOfMonth, o => o.MapFrom(s => s.Date.EndOfMonth()));

		CreateMap<AccountType, AccountTypeResponse>()
			.ConstructUsing(x => new(x));

		CreateMap<AttendanceType, AttendanceTypeResponse>()
			.ConstructUsing(x => new(x));

		CreateMap<CardType, CardTypeResponse>()
			.ConstructUsing(x => new(x));

		CreateMap<DocumentTypes, DocumentTypeResponse>()
			.ConstructUsing(x => new(x));

		CreateMap<PriorityLevelType, PriorityLevelTypeResponse>()
			.ConstructUsing(x => new(x));

		CreateMap<RoleType, RoleTypeResponse>()
			.ConstructUsing(x => new(x));

		CreateMap<WorkDayTypes, WorkDayTypeResponse>()
			.ConstructUsing(x => new(x));
	}
}