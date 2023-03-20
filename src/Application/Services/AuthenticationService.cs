using Application.Contracts.Requests.Identity;
using Application.Errors.Services;
using Application.Interfaces.Application;
using Application.Interfaces.Infrastructure.Identity;
using AutoMapper;
using Domain.Entities.Identity;
using Domain.Errors;
using Domain.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Application.Services;

internal sealed class AuthenticationService : IAuthenticationService
{
	private readonly ILogger<AuthenticationService> _logger;
	private readonly IUserService _userService;
	private readonly IMapper _mapper;

	/// <summary>
	/// Initilizes an instance of <see cref="AuthenticationService"/> class.
	/// </summary>
	/// <param name="logger">The logger service.</param>
	/// <param name="userService">The user service.</param>
	public AuthenticationService(ILogger<AuthenticationService> logger, IUserService userService, IMapper mapper)
	{
		_logger = logger;
		_userService = userService;
		_mapper = mapper;
	}

	public async Task<ErrorOr<Success>> AuthenticateUser(UserLoginRequest loginRequest)
	{
		try
		{
			User? user = await _userService.FindByNameAsync(loginRequest.UserName);

			if (user is null)
				return AuthenticationServiceErrors.UserNotFound(loginRequest.UserName);

			bool success = await _userService.CheckPasswordAsync(user, loginRequest.Password);

			if (!success)
				return AuthenticationServiceErrors.UserUnauthorized(loginRequest.UserName);

			return Result.Success;
		}
		catch (Exception ex)
		{
			_logger.LogError(ex.Message, ex);
			return AuthenticationServiceErrors.AuthenticateUserFailed;
		}
	}

	public async Task<ErrorOr<Created>> CreateUser(UserCreateRequest createRequest)
	{
		try
		{
			User user = _mapper.Map<User>(createRequest);

			IdentityResult result = await _userService.CreateAsync(user, createRequest.Password);
			if (!result.Succeeded)
			{
				foreach (IdentityError error in result.Errors)
					_logger.LogError($"{error.Code} - {error.Description}", error);
				return AuthenticationServiceErrors.CreateUserFailed;
			}

			result = await _userService.AddToRolesAsync(user, createRequest.Roles);
			if (!result.Succeeded)
			{
				foreach (IdentityError error in result.Errors)
					_logger.LogError($"{error.Code} - {error.Description}", error);
				return AuthenticationServiceErrors.CreateUserRolesFailed;
			}

			return Result.Created;
		}
		catch (Exception ex)
		{
			_logger.LogError(ex.Message, ex);
			return AuthenticationServiceErrors.CreateUserFailed;
		}
	}

	public async Task<ErrorOr<Updated>> UpdateUser(string userName, UserUpdateRequest updateRequest)
	{
		try
		{
			User user = await _userService.FindByNameAsync(userName);

			if (user is null)
				return AuthenticationServiceErrors.UserNotFound(userName);

			bool success = await _userService.CheckPasswordAsync(user, updateRequest.Password);

			if (!success)
				return AuthenticationServiceErrors.UserUnauthorized(userName);

			user.FirstName = updateRequest.FirstName;
			user.MiddleName = updateRequest.MiddleName;
			user.LastName = updateRequest.LastName;
			user.DateOfBirth = updateRequest.DateOfBirth;
			user.Email = updateRequest.Email;

			IdentityResult result = await _userService.UpdateAsync(user);
			if (!result.Succeeded)
			{
				foreach (IdentityError error in result.Errors)
					_logger.LogError($"{error.Code} - {error.Description}", error);
				return AuthenticationServiceErrors.UpdateUserFailed;
			}

			return Result.Updated;
		}
		catch (Exception ex)
		{
			_logger.LogError(ex.Message, ex);
			return AuthenticationServiceErrors.UpdateUserFailed;
		}
	}
}
