using Application.Contracts.Requests.Finance;

using AutoMapper;

using Domain.Models.Finance;

namespace Application.Common.MappingProfiles;

[SuppressMessage("Style", "IDE0058", Justification = "AutoMapper")]
internal static partial class RequestProfiles
{
	/// <summary>
	/// The finance request profile class.
	/// </summary>
	internal sealed class FinanceRequestProfile : Profile
	{
		public FinanceRequestProfile()
		{
			CreateMap<AccountCreateRequest, AccountModel>();
			CreateMap<AccountUpdateRequest, AccountModel>();

			CreateMap<CardCreateRequest, CardModel>();
			CreateMap<CardUpdateRequest, CardModel>();
		}
	}
}
