﻿using Application.Contracts.Responses.Base;
using System.ComponentModel.DataAnnotations;

namespace Application.Contracts.Responses.Identity;

/// <summary>
/// The user response class.
/// </summary>
/// <remarks>
/// Derives from the <see cref="ResponseModel"/> class.
/// </remarks>
public sealed class UserResponse : ResponseModel
{
	/// <summary>
	/// The first name of the user.
	/// </summary>
	[DataType(DataType.Text)]
	public string FirstName { get; set; } = default!;

	/// <summary>
	/// The middle name of the user.
	/// </summary>
	[DataType(DataType.Text)]
	public string? MiddleName { get; set; } = default!;

	/// <summary>
	/// The last name of the user.
	/// </summary>
	[DataType(DataType.Text)]
	public string LastName { get; set; } = default!;

	/// <summary>
	/// The date of birth of the user.
	/// </summary>
	[DataType(DataType.Date)]
	public DateTime? DateOfBirth { get; set; } = default!;

	/// <summary>
	/// The user name of the user.
	/// </summary>
	[DataType(DataType.Text)]
	public string UserName { get; set; } = default!;

	/// <summary>
	/// The email of the user.
	/// </summary>
	[DataType(DataType.EmailAddress)]
	public string Email { get; set; } = default!;

	/// <summary>
	/// The phone number of the user.
	/// </summary>
	[DataType(DataType.PhoneNumber)]
	public string? PhoneNumber { get; set; } = default!;

	/// <summary>
	/// The picture of the user.
	/// </summary>
	[DataType(DataType.Text)]
	public byte[]? Picture { get; set; } = default!;
}