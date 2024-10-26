using Application.Contracts.Responses.Finance;

using AutoMapper;

using Domain.Models.Finance;

namespace Application.Profiles.Responses;

/// <summary>
/// The finance response class.
/// </summary>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, auto mapper profile.")]
internal sealed class FinanceResponseProfile : Profile
{
	public FinanceResponseProfile()
	{
		CreateMap<AccountModel, AccountResponse>();
		CreateMap<CardModel, CardResponse>();
		CreateMap<TransactionModel, TransactionResponse>();
	}
}
