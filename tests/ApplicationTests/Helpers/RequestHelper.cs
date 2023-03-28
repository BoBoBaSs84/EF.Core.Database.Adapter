using Application.Contracts.Requests.Identity;
using BaseTests.Helpers;
using Tynamix.ObjectFiller;
using RP = Domain.Constants.DomainConstants.RegexPatterns;

namespace ApplicationTests.Helpers;

public static class RequestHelper
{
	public static UserCreateRequest GetUserCreateRequest(this UserCreateRequest request, string userName, string password)
	{
		Filler<UserCreateRequest> filler = new();
		request = filler.Fill(request);
		request.Email = RandomHelper.GetString(RP.Email);
		request.UserName = userName;
		request.Password = password;

		return request;
	}

	public static UserUpdateRequest GetUserUpdateRequest(this UserUpdateRequest request)
	{
		Filler<UserUpdateRequest> filler = new();
		request = filler.Fill(request);
		request.Email = RandomHelper.GetString(RP.Email);
		
		return request;
	}
}