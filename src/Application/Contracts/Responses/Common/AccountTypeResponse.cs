using Application.Contracts.Responses.Common.Base;

using Domain.Enumerators.Finance;

namespace Application.Contracts.Responses.Common;

/// <summary>
/// The bank account type response.
/// </summary>
/// <inheritdoc/>
public sealed class AccountTypeResponse(AccountType enumValue) : EnumeratorBaseResponse<AccountType>(enumValue)
{ }
