﻿using Domain.Models.Identity;

namespace Domain.Models.Finance;

public partial class CardModel
{
	/// <summary>
	/// The <see cref="User"/> property.
	/// </summary>
	public virtual UserModel User { get; set; } = default!;

	/// <summary>
	/// The <see cref="Account"/> property.
	/// </summary>
	public virtual AccountModel Account { get; set; } = default!;

	/// <summary>
	/// The <see cref="CardTransactions"/> property.
	/// </summary>
	public virtual ICollection<CardTransactionModel> CardTransactions { get; set; } = default!;
}
