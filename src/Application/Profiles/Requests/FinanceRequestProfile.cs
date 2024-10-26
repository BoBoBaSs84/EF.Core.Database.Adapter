using Application.Contracts.Requests.Finance;

using AutoMapper;

using Domain.Models.Finance;

namespace Application.Profiles.Requests;

/// <summary>
/// The finance request profile class.
/// </summary>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, auto mapper profile.")]
internal sealed class FinanceRequestProfile : Profile
{
	public FinanceRequestProfile()
	{
		CreateMap<AccountCreateRequest, AccountModel>();
		CreateMap<AccountUpdateRequest, AccountModel>();

		CreateMap<CardCreateRequest, CardModel>();
		CreateMap<CardUpdateRequest, CardModel>();

		CreateMap<TransactionCreateRequest, TransactionModel>();
		CreateMap<TransactionUpdateRequest, TransactionModel>();
	}
}
