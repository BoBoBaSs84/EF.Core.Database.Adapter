﻿using Domain.Common.EntityBaseTypes;
using Domain.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static Domain.Constants;
using static Domain.Constants.Sql;

namespace Domain.Entities.Finance;

/// <summary>
/// The account entity class.
/// </summary>
/// <remarks>
/// Derives from the <see cref="AuditedModel"/> class.
/// </remarks>
[Index(nameof(IBAN), IsUnique = true)]
public partial class Account : AuditedModel
{
	private string iban = default!;

	/// <summary>
	/// The <see cref="IBAN"/> property.
	/// </summary>
	[MaxLength(MaxLength.MAX_25), Unicode(false), RegularExpression(Regex.IBAN)]
	public string IBAN
	{
		get => iban;
		set
		{
			if (value != iban)
				iban = value.RemoveWhitespace();
		}
	}

	/// <summary>
	/// The <see cref="Provider"/> property.
	/// </summary>
	[MaxLength(MaxLength.MAX_500)]
	public string Provider { get; set; } = default!;
}
