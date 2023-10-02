using Application.Contracts.Requests.Finance;
using Application.Contracts.Requests.Identity;

using Tynamix.ObjectFiller;

using static Domain.Constants.DomainConstants;

using TestUser = BaseTests.Constants.TestConstants.TestUser;

namespace BaseTests.Helpers;

/// <summary>
/// The request helper class.
/// </summary>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, UnitTest.")]
public static class RequestHelper
{
	/// <summary>
	/// Returns a user create request.
	/// </summary>
	/// <param name="request">The request to enrich.</param>
	/// <param name="password">The password for the request.</param>
	/// <returns>The enriched request.</returns>
	public static UserCreateRequest GetUserCreateRequest(this UserCreateRequest request, string password = TestUser.PassGood)
	{
		Filler<UserCreateRequest> filler = new();
		filler.Setup()
			.OnProperty(p => p.FirstName).Use(new RealNames(NameStyle.FirstName))
			.OnProperty(p => p.LastName).Use(new RealNames(NameStyle.LastName))
			.OnProperty(p => p.Email).Use(new EmailAddresses())
			.OnProperty(p => p.Password).Use(password);

		return filler.Fill(request);
	}

	/// <summary>
	/// Returns a user update request.
	/// </summary>
	/// <param name="request">The request to enrich.</param>
	/// <returns>The enriched request.</returns>
	public static UserUpdateRequest GetUserUpdateRequest(this UserUpdateRequest request)
	{
		Filler<UserUpdateRequest> filler = new();
		filler.Setup()
			.OnProperty(p => p.FirstName).Use(new RealNames(NameStyle.FirstName))
			.OnProperty(p => p.LastName).Use(new RealNames(NameStyle.LastName))
			.OnProperty(p => p.MiddleName).Use(new RealNames(NameStyle.FirstName))
			.OnProperty(p => p.DateOfBirth).Use(RandomHelper.GetDateTime())
			.OnProperty(p => p.Email).Use(new EmailAddresses())
			.OnProperty(p => p.Picture).IgnoreIt()
			.OnProperty(p => p.PhoneNumber).IgnoreIt();

		return filler.Fill(request);
	}

	public static AccountCreateRequest GetAccountCreateRequest(this AccountCreateRequest request)
	{
		Filler<AccountCreateRequest> filler = new();
		filler.Setup()
			.OnProperty(p => p.IBAN).Use(RandomHelper.GetString(RegexPatterns.IBAN));

		request = filler.Fill(request);

		if (request.Cards is null)
			return request;

		foreach (var card in request.Cards)
			card.PAN = RandomHelper.GetString(RegexPatterns.PAN);

		return request;
	}

	public static CardCreateRequest GetCardCreateRequest(this CardCreateRequest request)
	{
		Filler<CardCreateRequest> filler = new();
		filler.Setup()
			.OnProperty(p => p.PAN).Use(RandomHelper.GetString(RegexPatterns.PAN));
		return filler.Fill(request);
	}
}