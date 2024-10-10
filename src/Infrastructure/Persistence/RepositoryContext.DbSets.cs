using Domain.Models.Attendance;
using Domain.Models.Finance;
using Domain.Models.Todo;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

internal sealed partial class RepositoryContext
{
	public required DbSet<AttendanceModel> Attendances { get; init; }
	public required DbSet<AccountModel> Accounts { get; init; }
	public required DbSet<CardModel> Cards { get; init; }
	public required DbSet<TransactionModel> Transactions { get; init; }
	public required DbSet<List> TodoLists { get; init; }
	public required DbSet<Item> TodoItems { get; init; }
}
