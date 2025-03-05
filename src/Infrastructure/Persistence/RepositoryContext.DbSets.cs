using BB84.Home.Domain.Entities.Attendance;
using BB84.Home.Domain.Entities.Documents;
using BB84.Home.Domain.Entities.Finance;
using BB84.Home.Domain.Entities.Todo;

using Microsoft.EntityFrameworkCore;

namespace BB84.Home.Infrastructure.Persistence;

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
