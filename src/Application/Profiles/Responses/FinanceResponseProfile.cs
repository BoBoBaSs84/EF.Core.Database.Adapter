using Application.Contracts.Responses.Finance;

using AutoMapper;

using BB84.Home.Domain.Entities.Finance;

namespace Application.Profiles.Responses;

/// <summary>
/// The finance response class.
/// </summary>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, auto mapper profile.")]
internal sealed class FinanceResponseProfile : Profile
{
	public FinanceResponseProfile()
	{
		CreateMap<AccountEntity, AccountResponse>();
		CreateMap<CardEntity, CardResponse>();
		CreateMap<TransactionEntity, TransactionResponse>();
	}
}
