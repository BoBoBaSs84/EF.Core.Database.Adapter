using Application.Contracts.Requests.Finance;

using AutoMapper;

using BB84.Home.Domain.Entities.Finance;

namespace Application.Profiles.Requests;

/// <summary>
/// The finance request profile class.
/// </summary>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, auto mapper profile.")]
internal sealed class FinanceRequestProfile : Profile
{
	public FinanceRequestProfile()
	{
		CreateMap<AccountCreateRequest, AccountEntity>();
		CreateMap<AccountUpdateRequest, AccountEntity>();

		CreateMap<CardCreateRequest, CardEntity>();
		CreateMap<CardUpdateRequest, CardEntity>();

		CreateMap<TransactionCreateRequest, TransactionEntity>();
		CreateMap<TransactionUpdateRequest, TransactionEntity>();
	}
}
