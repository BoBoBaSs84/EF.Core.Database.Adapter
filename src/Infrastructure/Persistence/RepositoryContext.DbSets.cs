using Domain.Models.Attendance;
using Domain.Models.Finance;
using Domain.Models.Todo;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public sealed partial class RepositoryContext
{
	/// <inheritdoc/>
	public DbSet<AttendanceModel> Attendances { get; set; } = default!;
	/// <inheritdoc/>
	public DbSet<AccountModel> Accounts { get; set; } = default!;
	/// <inheritdoc/>
	public DbSet<CardModel> Cards { get; set; } = default!;
	/// <inheritdoc/>
	public DbSet<TransactionModel> Transactions { get; set; } = default!;
	/// <inheritdoc/>
	public DbSet<List> TodoLists { get; set; } = default!;
	/// <inheritdoc/>
	public DbSet<Item> TodoItems { get; set; } = default!;
}
