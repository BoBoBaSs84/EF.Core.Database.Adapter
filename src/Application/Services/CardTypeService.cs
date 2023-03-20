using Application.Contracts.Responses;
using Application.Errors.Base;
using Application.Features.Requests;
using Application.Features.Responses;
using Application.Interfaces.Application;
using Application.Interfaces.Infrastructure;
using AutoMapper;
using Domain.Entities.Enumerator;
using Domain.Errors;
using Domain.Extensions.QueryExtensions;
using Microsoft.Extensions.Logging;

namespace Application.Services;

[SuppressMessage("Globalization", "CA1309",
	Justification = "Translation of the 'string.Equals' overload with a 'StringComparison' parameter is not supported.")]
internal sealed class CardTypeService : ICardTypeService
{
	private readonly ILogger<CardTypeService> _logger;
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	/// <summary>
	/// Initilizes an instance of <see cref="CardTypeService"/> class.
	/// </summary>
	/// <param name="logger">The logger service.</param>
	/// <param name="unitOfWork">The unit of work.</param>
	/// <param name="mapper">The auto mapper.</param>
	public CardTypeService(ILogger<CardTypeService> logger, IUnitOfWork unitOfWork, IMapper mapper)
	{
		_logger = logger;
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<ErrorOr<CardTypeResponse>> GetById(int id, bool trackChanges = false, CancellationToken cancellationToken = default)
	{
		try
		{
			CardType? cardType = await _unitOfWork.CardTypeRepository.GetByIdAsync(id, cancellationToken);

			if (cardType is null)
				return ApiError.CreateNotFound("", "");

			CardTypeResponse response = _mapper.Map<CardTypeResponse>(cardType);

			return response;

		}
		catch (Exception ex)
		{
			_logger.LogError(ex.Message, ex);
			return ApiError.CreateFailed("", "");
		}
	}

	public async Task<ErrorOr<CardTypeResponse>> GetByName(string name, bool trackChanges = false, CancellationToken cancellationToken = default)
	{
		try
		{
			CardType? cardType = await _unitOfWork.CardTypeRepository.GetByConditionAsync(
				expression: x => x.Name.Equals(name),
				trackChanges: trackChanges,
				cancellationToken: cancellationToken
				);

			if (cardType is null)
				return ApiError.CreateNotFound("", "");

			CardTypeResponse response = _mapper.Map<CardTypeResponse>(cardType);

			return response;

		}
		catch (Exception ex)
		{
			_logger.LogError(ex.Message, ex);
			return ApiError.CreateFailed("", "");
		}
	}

	public async Task<ErrorOr<IPagedList<CardTypeResponse>>> GetPagedByParameters(CardTypeParameters parameters, bool trackChanges = false, CancellationToken cancellationToken = default)
	{
		try
		{
			IEnumerable<CardType> cardTypes = await _unitOfWork.CardTypeRepository.GetManyByConditionAsync(
				filterBy: x => x.FilterByIsActive(parameters.IsActive).SearchByName(parameters.Name).SearchByDescription(parameters.Description),
				orderBy: x => x.OrderBy(x => x.Id),
				take: parameters.PageSize,
				skip: (parameters.PageNumber - 1) * parameters.PageSize,
				trackChanges: trackChanges,
				cancellationToken: cancellationToken
				);

			if (!cardTypes.Any())
				return ApiError.CreateNotFound("", "");

			IEnumerable<CardTypeResponse> response = _mapper.Map<IEnumerable<CardTypeResponse>>(cardTypes);

			int totalCount = _unitOfWork.CardRepository.TotalCount;

			return new PagedList<CardTypeResponse>(response, totalCount, parameters.PageNumber, parameters.PageSize);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex.Message, ex);
			return ApiError.CreateFailed("", "");
		}
	}
}
