using Application.Contracts.Responses.Finance;

using AutoMapper;

using Domain.Models.Finance;

namespace Application.Common.MappingProfiles;

[SuppressMessage("Style", "IDE0058", Justification = "AutoMapper")]
internal static partial class ResponseProfiles
{
	/// <summary>
	/// The finance response class.
	/// </summary>
	internal sealed class FinanceResponseProfile : Profile
	{
		public FinanceResponseProfile()
		{
			CreateMap<AccountModel, AccountResponse>();
			CreateMap<CardModel, CardResponse>();
			CreateMap<TransactionModel, TransactionResponse>();
		}
	}
}
