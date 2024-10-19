using Application.Interfaces.Application.Identity;
using Application.Interfaces.Infrastructure.Services;
using Application.Options;
using Application.Services.Identity;

using AutoMapper;

using Domain.Models.Identity;

using Microsoft.Extensions.Options;

using Moq;

namespace ApplicationTests.Services.Identity;

[TestClass]
[TestCategory("UnitTest")]
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, unit testing.")]
public sealed partial class AuthenticationServiceTests : ApplicationTestBase
{
	private readonly IMapper _mapper = GetService<IMapper>();
	private Mock<IOptions<BearerSettings>> _bearerSettingsMock = default!;
	private Mock<ITokenService> _tokenServiceMock = default!;
	private Mock<ILoggerService<AuthenticationService>> _loggerServiceMock = default!;
	private Mock<IRoleService> _roleServiceMock = default!;
	private Mock<IUserService> _userServiceMock = default!;

	private AuthenticationService CreateMockedInstance(BearerSettings? settings = null)
	{
		_bearerSettingsMock = new();

		if (settings is not null)
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

	private static UserModel CreateUser(Guid? userId = null)
	{
		UserModel user = new()
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
}