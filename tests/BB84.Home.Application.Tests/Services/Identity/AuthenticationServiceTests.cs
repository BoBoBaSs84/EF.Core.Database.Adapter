using System.Security.Claims;
using System.Security.Principal;

using AutoMapper;

using BB84.Home.Application.Contracts.Requests.Identity;
using BB84.Home.Application.Interfaces.Application.Services.Identity;
using BB84.Home.Application.Interfaces.Infrastructure.Services;
using BB84.Home.Application.Options;
using BB84.Home.Application.Services.Identity;
using BB84.Home.Application.Tests;
using BB84.Home.Base.Tests.Helpers;
using BB84.Home.Domain.Entities.Identity;

using Microsoft.Extensions.Options;

using Moq;

namespace ApplicationTests.Services.Identity;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, unit testing.")]
public sealed partial class AuthenticationServiceTests : ApplicationTestBase
{
	private readonly IMapper _mapper = GetService<IMapper>();
	private Mock<IOptions<BearerSettingsOption>> _bearerSettingsMock = default!;
	private Mock<ITokenService> _tokenServiceMock = default!;
	private Mock<ILoggerService<AuthenticationService>> _loggerServiceMock = default!;
	private Mock<IRoleService> _roleServiceMock = default!;
	private Mock<IUserService> _userServiceMock = default!;
	private Mock<IIdentity> _identityMock = default!;

	private AuthenticationService CreateMockedInstance(BearerSettingsOption? settings = null)
	{
		_bearerSettingsMock = new();

		settings ??= new()
		{
			Issuer = "UnitTest",
			Audience = "http://UnitTest.org",
			ExpiryInMinutes = 5,
			SecurityKey = RandomHelper.GetString(64)
		};

		_bearerSettingsMock.Setup(x => x.Value).Returns(settings);

		_tokenServiceMock = new();
		_loggerServiceMock = new();
		_roleServiceMock = new();
		_userServiceMock = new();

		AuthenticationService authenticationService = new(
			logger: _loggerServiceMock.Object,
			tokenService: _tokenServiceMock.Object,
			roleService: _roleServiceMock.Object,
			userService: _userServiceMock.Object,
			mapper: _mapper
			);

		return authenticationService;
	}

	private ClaimsPrincipal CreatePrincipal(string name = "UnitTest", bool isAuthenticated = true)
	{
		_identityMock = new();
		_identityMock.SetupAllProperties();
		_identityMock.Setup(x => x.Name).Returns(name);
		_identityMock.Setup(x => x.IsAuthenticated).Returns(isAuthenticated);

		return new(_identityMock.Object);
	}

	private static UserEntity CreateUser(Guid? userId = null)
	{
		UserEntity user = new()
		{
			Id = userId ?? Guid.NewGuid(),
			FirstName = "UnitTest",
			MiddleName = "UnitTest",
			LastName = "UnitTest",
			DateOfBirth = DateTime.Today,
			Email = "unit.test@example.com",
			EmailConfirmed = true,
			PhoneNumber = "1234567890",
			PhoneNumberConfirmed = false
		};
		return user;
	}

	private static AuthenticationRequest CreateAuthenticationRequest() => new()
	{
		UserName = RandomHelper.GetString(16),
		Password = RandomHelper.GetString(16)
	};

	private static TokenRequest CreateTokenRequest() => new()
	{
		AccessToken = RandomHelper.GetString(64),
		RefreshToken = RandomHelper.GetString(64)
	};
}