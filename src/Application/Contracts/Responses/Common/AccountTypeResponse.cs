using BB84.Home.Application.Contracts.Responses.Common.Base;
using BB84.Home.Domain.Enumerators.Finance;

namespace BB84.Home.Application.Contracts.Responses.Common;

/// <summary>
/// The bank account type response.
/// </summary>
/// <inheritdoc/>
public sealed class AccountTypeResponse(AccountType enumValue) : EnumeratorBaseResponse<AccountType>(enumValue)
{ }
