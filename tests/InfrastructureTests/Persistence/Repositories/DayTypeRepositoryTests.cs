using Application.Interfaces.Infrastructure;
using BaseTests.Helpers;
using Domain.Entities.Enumerator;
using Domain.Enumerators;
using Domain.Extensions;
using FluentAssertions;
using static BaseTests.Constants.TestConstants;

namespace InfrastructureTests.Persistence.Repositories;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "UnitTest")]
public sealed class DayTypeRepositoryTests : InfrastructureBaseTests
{
	private IUnitOfWork _unitOfWork = default!;

	[TestMethod, Owner(Bobo)]
	public async Task GetByNameAsyncTest()
	{
		_unitOfWork = GetRequiredService<IUnitOfWork>();

		string dayTypeName = DayTypes.PLANNEDVACATION.GetName();
		DayType dayType = await _unitOfWork.DayTypeRepository.GetByNameAsync(dayTypeName);

		AssertionHelper.AssertInScope(() =>
		{
			dayType.Should().NotBeNull();
			dayType.Name.Should().Be(dayTypeName);
		});
	}

	[TestMethod, Owner(Bobo)]
	public async Task GetByNamesAsyncTest()
	{
		_unitOfWork = GetRequiredService<IUnitOfWork>();

		IEnumerable<string> dayTypeNames = new[] { DayTypes.PLANNEDVACATION.GetName(), DayTypes.WORKDAY.GetName() };
		IEnumerable<DayType> dayTypes = await _unitOfWork.DayTypeRepository.GetByNamesAsync(dayTypeNames);

		AssertionHelper.AssertInScope(() =>
		{
			dayTypes.Should().NotBeNullOrEmpty();
			dayTypes.Should().HaveCount(dayTypeNames.Count());
		});
	}

	[TestMethod, Owner(Bobo)]
	public async Task GetAllActiveAsyncTest()
	{
		_unitOfWork = GetRequiredService<IUnitOfWork>();

		IEnumerable<DayType> dayTypes = await _unitOfWork.DayTypeRepository.GetAllActiveAsync();

		dayTypes.Should().NotBeNullOrEmpty();
	}
}
