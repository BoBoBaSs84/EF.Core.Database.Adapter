﻿using BB84.Home.Application.Interfaces.Infrastructure.Persistence;
using BB84.Home.Application.Interfaces.Infrastructure.Persistence.Repositories;
using BB84.Home.Application.Interfaces.Infrastructure.Persistence.Repositories.Documents;
using BB84.Home.Application.Interfaces.Infrastructure.Persistence.Repositories.Todo;
using BB84.Home.Application.Interfaces.Infrastructure.Services;

using BB84.Home.Infrastructure.Persistence.Repositories;
using BB84.Home.Infrastructure.Persistence.Repositories.Documents;
using BB84.Home.Infrastructure.Persistence.Repositories.Finance;
using BB84.Home.Infrastructure.Persistence.Repositories.Todo;

namespace BB84.Home.Infrastructure.Services;

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
	private readonly Lazy<IAttendanceRepository> _lazyAttendanceRepository;
	private readonly Lazy<ICardRepository> _lazyCardRepository;
	private readonly Lazy<IDocumentRepository> _lazyDocumentRepository;
	private readonly Lazy<IDocumentDataRepository> _lazyDocumentDataRepository;
	private readonly Lazy<IDocumentExtensionRepository> _lazyDocumentExtensionRepository;
	private readonly Lazy<IItemRepository> _lazyTodoItemRepository;
	private readonly Lazy<IListRepository> _lazyTodoListRepository;
	private readonly Lazy<ITransactionRepository> _lazyTransactionRepository;

	/// <summary>
	/// Initializes a new instance of the repository service class.
	/// </summary>
	/// <param name="context">The database context to work with.</param>
	public RepositoryService(IRepositoryContext context)
	{
		_context = context;

		_lazyAccountRepository = new Lazy<IAccountRepository>(() => new AccountRepository(_context));
		_lazyAttendanceRepository = new Lazy<IAttendanceRepository>(() => new AttendanceRepository(_context));
		_lazyCardRepository = new Lazy<ICardRepository>(() => new CardRepository(_context));
		_lazyDocumentRepository = new Lazy<IDocumentRepository>(() => new DocumentRepository(_context));
		_lazyDocumentDataRepository = new Lazy<IDocumentDataRepository>(() => new DocumentDataRepository(_context));
		_lazyDocumentExtensionRepository = new Lazy<IDocumentExtensionRepository>(() => new DocumentExtensionRepository(_context));
		_lazyTodoItemRepository = new Lazy<IItemRepository>(() => new ItemRepository(_context));
		_lazyTodoListRepository = new Lazy<IListRepository>(() => new ListRepository(_context));
		_lazyTransactionRepository = new Lazy<ITransactionRepository>(() => new TransactionRepository(_context));
	}

	public IAccountRepository AccountRepository => _lazyAccountRepository.Value;
	public IAttendanceRepository AttendanceRepository => _lazyAttendanceRepository.Value;
	public ICardRepository CardRepository => _lazyCardRepository.Value;
	public IDocumentRepository DocumentRepository => _lazyDocumentRepository.Value;
	public IDocumentDataRepository DocumentDataRepository => _lazyDocumentDataRepository.Value;
	public IDocumentExtensionRepository DocumentExtensionRepository => _lazyDocumentExtensionRepository.Value;
	public IItemRepository TodoItemRepository => _lazyTodoItemRepository.Value;
	public IListRepository TodoListRepository => _lazyTodoListRepository.Value;
	public ITransactionRepository TransactionRepository => _lazyTransactionRepository.Value;

	public async Task<int> CommitChangesAsync(CancellationToken token = default)
		=> await _context.SaveChangesAsync(token);
}
