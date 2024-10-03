using Application.Interfaces.Application.Common;
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
	private Mock<IDateTimeService> _dateTimeServiceMock = default!;
	private Mock<ILoggerService<AuthenticationService>> _loggerServiceMock = default!;
	private Mock<IRoleService> _roleServiceMock = default!;
	private Mock<IUserService> _userServiceMock = default!;

	private AuthenticationService CreateMockedInstance(BearerSettings? settings = null)
	{
		_bearerSettingsMock = new();
		if (settings is not null)
			_bearerSettingsMock.Setup(x => x.Value).Returns(settings);

		_dateTimeServiceMock = new();
		_dateTimeServiceMock.SetupAllProperties();

		_loggerServiceMock = new();
		_roleServiceMock = new();
		_userServiceMock = new();

		return new(_bearerSettingsMock.Object, _dateTimeServiceMock.Object, _loggerServiceMock.Object,
			_roleServiceMock.Object, _userServiceMock.Object, _mapper);
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
		};
		return user;
	}
}