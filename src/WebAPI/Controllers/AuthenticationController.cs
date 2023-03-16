using Application.Common.Interfaces.Identity;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Contracts.Requests.Identity;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
	private readonly ILogger<AuthenticationController> _logger;
	private readonly IUserService _userService;

	/// <summary>
	/// 
	/// </summary>
	/// <param name="logger">The logger.</param>
	/// <param name="userService">The user service.</param>
	public AuthenticationController(ILogger<AuthenticationController> logger, IUserService userService)
	{
		_logger = logger;
		_userService = userService;
	}

	/// <summary>
	/// Creates a new user.
	/// </summary>
	/// <param name="createRequest">The user create request.</param>
	/// <response code="201">If the new user was created.</response>
	/// <response code="400">If something is not right with the request.</response>
	[HttpPost]
	[ProducesResponseType(StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	public async Task<IActionResult> CreateUser([FromBody] UserCreateRequest createRequest)
	{
		if (createRequest is null)
			return BadRequest(createRequest);

		User user = new()
		{
			FirstName = createRequest.FirstName,
			LastName = createRequest.LastName,
			Email = createRequest.Email,
			UserName = createRequest.UserName
		};

		IdentityResult result = await _userService.CreateAsync(user, createRequest.Password);
		if (!result.Succeeded)
		{
			foreach (IdentityError error in result.Errors)
				ModelState.TryAddModelError(error.Code, error.Description);
			return BadRequest(ModelState);
		}

		result = await _userService.AddToRolesAsync(user, createRequest.Roles);
		if (!result.Succeeded)
		{
			foreach (IdentityError error in result.Errors)
				ModelState.TryAddModelError(error.Code, error.Description);
			return BadRequest(ModelState);
		}

		return StatusCode(StatusCodes.Status201Created);
	}

	/// <summary>
	/// Updates an existing user.
	/// </summary>
	/// <param name="userName">The user to update.</param>
	/// <param name="updateRequest">The user update request.</param>
	/// <response code="200">If the user was updated.</response>
	/// <response code="400">If something is not right with the request.</response>
	/// <response code="401">If you are not authorize to update the user.</response>
	/// <response code="404">If the user to update was not found.</response>
	[HttpPut("{userName}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> UpdateUser(string userName, [FromBody] UserUpdateRequest updateRequest)
	{
		if (updateRequest is null)
			return BadRequest(updateRequest);

		User user = await _userService.FindByNameAsync(userName);

		if (user is null)
			return NotFound(userName);

		bool success = await _userService.CheckPasswordAsync(user, updateRequest.Password);

		if (!success)
			return Unauthorized();

		user.FirstName = updateRequest.FirstName;
		user.MiddleName = updateRequest.MiddleName;
		user.LastName = updateRequest.LastName;
		user.DateOfBirth = updateRequest.DateOfBirth;
		user.Email = updateRequest.Email;

		IdentityResult result = await _userService.UpdateAsync(user);

		if (!result.Succeeded)
		{
			foreach (IdentityError error in result.Errors)
				ModelState.TryAddModelError(error.Code, error.Description);
			return BadRequest(ModelState);
		}

		return Ok();
	}

	/// <summary>
	/// Checks if the user can login.
	/// </summary>
	/// <param name="loginRequest">The user login request.</param>
	/// <response code="200">If the user can login.</response>
	/// <response code="400">If something is not right with the request.</response>
	/// <response code="401">If you are not authorize to login.</response>
	/// <response code="404">If the user to login was not found.</response>
	[HttpPost("Login")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> Authenticate([FromBody] UserLoginRequest loginRequest)
	{
		if (loginRequest is null)
			return BadRequest(loginRequest);

		User? user = await _userService.FindByNameAsync(loginRequest.UserName);

		if (user is null)
			return NotFound(loginRequest.UserName);

		bool success = await _userService.CheckPasswordAsync(user, loginRequest.Password);

		return !success ? Unauthorized() : Ok();
	}
}
