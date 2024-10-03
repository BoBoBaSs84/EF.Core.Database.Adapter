using Domain.Models.Attendance;
using Domain.Models.Finance;
using Domain.Models.Todo;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public sealed partial class RepositoryContext
{
	/// <inheritdoc/>
	public required DbSet<AttendanceModel> Attendances { get; init; }
	/// <inheritdoc/>
	public required DbSet<AccountModel> Accounts { get; init; }
	/// <inheritdoc/>
	public required DbSet<CardModel> Cards { get; init; }
	/// <inheritdoc/>
	public required DbSet<TransactionModel> Transactions { get; init; }
	/// <inheritdoc/>
	public required DbSet<List> TodoLists { get; init; }
	/// <inheritdoc/>
	public required DbSet<Item> TodoItems { get; init; }
}
