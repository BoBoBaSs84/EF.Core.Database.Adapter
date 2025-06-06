﻿using System.ComponentModel.DataAnnotations;

using BB84.Home.Application.Contracts.Responses.Base;

namespace BB84.Home.Application.Contracts.Responses.Todo;

/// <summary>
/// The response for the todo list.
/// </summary>
public sealed class ListResponse : IdentityResponse
{
	/// <summary>
	/// The title of the todo list.
	/// </summary>
	[Required, DataType(DataType.Text)]
	public required string Title { get; init; }

	/// <summary>
	/// The color of the todo list.
	/// </summary>
	[DataType(DataType.Text)]
	public string? Color { get; init; }

	/// <summary>
	/// The items within the todo list.
	/// </summary>
	public ItemResponse[]? Items { get; init; }
}
