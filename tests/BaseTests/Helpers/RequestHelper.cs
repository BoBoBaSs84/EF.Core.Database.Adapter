using Application.Contracts.Requests.Identity;

using Tynamix.ObjectFiller;

using RH = BaseTests.Helpers.RandomHelper;
using TU = BaseTests.Constants.TestConstants.TestUser;

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
	public static UserCreateRequest GetUserCreateRequest(this UserCreateRequest request, string password = TU.PassGood)
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
			.OnProperty(p => p.DateOfBirth).Use(RH.GetDateTime())
			.OnProperty(p => p.Email).Use(new EmailAddresses())
			.OnProperty(p => p.PhoneNumber).Use("{N:3}-{N:3}-{N:4}");

		return filler.Fill(request);
	}
}