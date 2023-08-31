using Application.Interfaces.Infrastructure.Persistence.Repositories;
using Application.Interfaces.Infrastructure.Services;

using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;

namespace Infrastructure.Services;

/// <summary>
/// The unit of work class.
/// </summary>
/// <remarks>
/// Implements the members of the <see cref="IRepositoryService"/> interface.
/// </remarks>
internal sealed class RepositoryService : IRepositoryService
{
	private readonly RepositoryContext _context;

	private readonly Lazy<IAccountRepository> _lazyAccountRepository;
	private readonly Lazy<ICardRepository> _lazyCardRepository;
	private readonly Lazy<ITransactionRepository> _lazyTransactionRepository;
	private readonly Lazy<ICalendarDayRepository> _lazyCalendarRepository;
	private readonly Lazy<IDayTypeRepository> _lazyDayTypeRepository;
	private readonly Lazy<ICardTypeRepository> _lazyCardTypeRepository;
	private readonly Lazy<IAttendanceRepository> _lazyAttendanceRepository;

	/// <summary>
	/// Initializes a new instance of the <see cref="RepositoryService"/> class.
	/// </summary>
	/// <param name="context">The database context to work with.</param>
	public RepositoryService(RepositoryContext context)
	{
		_context = context ?? throw new ArgumentNullException(nameof(context));

		_lazyAccountRepository = new Lazy<IAccountRepository>(() => new AccountRepository(_context));
		_lazyCardRepository = new Lazy<ICardRepository>(() => new CardRepository(_context));
		_lazyTransactionRepository = new Lazy<ITransactionRepository>(() => new TransactionRepository(_context));
		_lazyCalendarRepository = new Lazy<ICalendarDayRepository>(() => new CalendarDayRepository(_context));
		_lazyDayTypeRepository = new Lazy<IDayTypeRepository>(() => new DayTypeRepository(_context));
		_lazyCardTypeRepository = new Lazy<ICardTypeRepository>(() => new CardTypeRepository(_context));
		_lazyAttendanceRepository = new Lazy<IAttendanceRepository>(() => new AttendanceRepository(_context));
	}

	/// <inheritdoc/>
	public IAccountRepository AccountRepository
		=> _lazyAccountRepository.Value;

	/// <inheritdoc/>
	public IAttendanceRepository AttendanceRepository
		=> _lazyAttendanceRepository.Value;

	/// <inheritdoc/>
	public ICalendarDayRepository CalendarDayRepository
		=> _lazyCalendarRepository.Value;

	/// <inheritdoc/>
	public ICardRepository CardRepository
		=> _lazyCardRepository.Value;

	/// <inheritdoc/>
	public ICardTypeRepository CardTypeRepository
		=> _lazyCardTypeRepository.Value;

	/// <inheritdoc/>
	public IDayTypeRepository DayTypeRepository
		=> _lazyDayTypeRepository.Value;

	/// <inheritdoc/>
	public ITransactionRepository TransactionRepository
		=> _lazyTransactionRepository.Value;

	/// <inheritdoc/>
	public async Task<int> CommitChangesAsync(CancellationToken cancellationToken = default)
		=> await _context.SaveChangesAsync(cancellationToken);
}
