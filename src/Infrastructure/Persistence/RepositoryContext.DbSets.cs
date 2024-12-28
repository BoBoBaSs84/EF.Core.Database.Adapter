using Domain.Entities.Attendance;
using Domain.Entities.Documents;
using Domain.Entities.Finance;
using Domain.Entities.Todo;

using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

internal sealed partial class RepositoryContext
{
	public required DbSet<AttendanceEntity> Attendances { get; init; }
	public required DbSet<AccountEntity> Accounts { get; init; }
	public required DbSet<CardEntity> Cards { get; init; }
	public required DbSet<DocumentEntity> Documents { get; init; }
	public required DbSet<TransactionEntity> Transactions { get; init; }
	public required DbSet<ListEntity> TodoLists { get; init; }
	public required DbSet<ItemEntity> TodoItems { get; init; }
}
