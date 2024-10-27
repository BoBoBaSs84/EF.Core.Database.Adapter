﻿using Application.Interfaces.Application.Providers;
using Application.Interfaces.Infrastructure.Services;
using Application.Options;
using Application.Services.Identity;

using BaseTests.Helpers;

using Domain.Models.Identity;

using Microsoft.Extensions.Options;

using Moq;

namespace ApplicationTests.Services.Identity;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here, unit testing.")]
public sealed partial class TokenServiceTests : ApplicationTestBase
{
	private Mock<IOptions<BearerSettings>> _bearerSettingsMock = default!;
	private Mock<IDateTimeProvider> _dateTimeServiceMock = default!;
	private Mock<IUserService> _userServiceMock = default!;

	private TokenService CreateMockedInstance(BearerSettings? settings = null)
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

		_dateTimeServiceMock = new();
		_dateTimeServiceMock.SetupAllProperties();

		_userServiceMock = new();

		TokenService tokenService = new(
			options: _bearerSettingsMock.Object,
			dateTimeService: _dateTimeServiceMock.Object,
			userService: _userServiceMock.Object
			);

		return tokenService;
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
