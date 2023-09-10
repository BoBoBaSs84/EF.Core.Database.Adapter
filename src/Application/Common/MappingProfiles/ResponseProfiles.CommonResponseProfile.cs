using Application.Contracts.Responses.Common;

using AutoMapper;

using Domain.Enumerators;
using Domain.Models.Common;

namespace Application.Common.MappingProfiles.Responses;

[SuppressMessage("Style", "IDE0058", Justification = "AutoMapper")]
internal static partial class ResponseProfiles
{
	/// <summary>
	/// The common response profile class.
	/// </summary>
	internal sealed class CommonResponseProfile : Profile
	{
		public CommonResponseProfile()
		{
			CreateMap<CalendarModel, CalendarResponse>();
		}
	}
}
