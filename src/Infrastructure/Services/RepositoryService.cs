using Application.Interfaces.Infrastructure.Persistence.Repositories;
using Application.Interfaces.Infrastructure.Services;

using Infrastructure.Interfaces.Persistence;
using Infrastructure.Persistence.Repositories;

namespace Infrastructure.Services;

/// <summary>
/// The repository service class.
/// </summary>
/// <remarks>
/// Implements the members of the <see cref="IRepositoryService"/> interface.
/// </remarks>
internal sealed class RepositoryService : IRepositoryService
{
	private readonly IRepositoryContext _context;

	private readonly Lazy<IAccountRepository> _lazyAccountRepository;
	private readonly Lazy<ICardRepository> _lazyCardRepository;
	private readonly Lazy<ITransactionRepository> _lazyTransactionRepository;
	private readonly Lazy<ICalendarRepository> _lazyCalendarRepository;
	private readonly Lazy<IAttendanceRepository> _lazyAttendanceRepository;

	/// <summary>
	/// Initializes a new instance of the repository service class.
	/// </summary>
	/// <param name="context">The database context to work with.</param>
	public RepositoryService(IRepositoryContext context)
	{
		_context = context ?? throw new ArgumentNullException(nameof(context));

		_lazyAccountRepository = new Lazy<IAccountRepository>(() => new AccountRepository(_context));
		_lazyCardRepository = new Lazy<ICardRepository>(() => new CardRepository(_context));
		_lazyTransactionRepository = new Lazy<ITransactionRepository>(() => new TransactionRepository(_context));
		_lazyCalendarRepository = new Lazy<ICalendarRepository>(() => new CalendarRepository(_context));
		_lazyAttendanceRepository = new Lazy<IAttendanceRepository>(() => new AttendanceRepository(_context));
	}

	public IAccountRepository AccountRepository
		=> _lazyAccountRepository.Value;

	public ICalendarRepository CalendarRepository
		=> _lazyCalendarRepository.Value;

	public ICardRepository CardRepository
		=> _lazyCardRepository.Value;

	public ITransactionRepository TransactionRepository
		=> _lazyTransactionRepository.Value;

	public IAttendanceRepository AttendanceRepository
		=> _lazyAttendanceRepository.Value;

	public async Task<int> CommitChangesAsync(CancellationToken cancellationToken = default)
		=> await _context.SaveChangesAsync(cancellationToken);
}
