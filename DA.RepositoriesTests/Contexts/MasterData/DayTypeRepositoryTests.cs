using DA.Domain.Extensions;
using DA.Domain.Models.MasterData;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using static DA.BaseTests.Constants;
using static DA.BaseTests.Helpers.AssertionHelper;
using static DA.BaseTests.Helpers.RandomHelper;

namespace DA.RepositoriesTests.Contexts.MasterData;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "UnitTest")]
public class DayTypeRepositoryTests : RepositoriesBaseTest
{
	[TestMethod, Owner(Bobo)]
	public async Task GetByNameFailedTest()
	{
		string dayTypeName = GetString(12);

		DayType dayType = await RepositoryManager.DayTypeRepository.GetByNameAsync(dayTypeName);

		dayType.Should().BeNull();
	}

	[TestMethod, Owner(Bobo)]
	public async Task GetByNamesFailedTest()
	{
		IEnumerable<string> dayTypeNames = new[]
		{
			GetString(12),
			GetString(12)
		};

		IEnumerable<DayType> dayTypes = await RepositoryManager.DayTypeRepository.GetByNamesAsync(dayTypeNames);

		dayTypes.Should().BeNullOrEmpty();
	}

	[TestMethod, Owner(Bobo)]
	public async Task GetByNameSuccessTest()
	{
		string dayTypeName = Domain.Enumerators.DayType.PLANNEDVACATION.GetName();

		DayType dayType = await RepositoryManager.DayTypeRepository.GetByNameAsync(dayTypeName);

		AssertInScope(() =>
		{
			dayType.Should().NotBeNull();
			dayType.Name.Should().Be(dayTypeName);
		});
	}

	[TestMethod, Owner(Bobo)]
	public async Task GetByNamesSuccessTest()
	{
		IEnumerable<string> dayTypeNames = new[]
		{
			Domain.Enumerators.DayType.PLANNEDVACATION.GetName(),
			Domain.Enumerators.DayType.WORKDAY.GetName()
		};

		IEnumerable<DayType> dayTypes = await RepositoryManager.DayTypeRepository.GetByNamesAsync(dayTypeNames);

		AssertInScope(() =>
		{
			dayTypes.Should().NotBeNullOrEmpty();
			dayTypes.Should().HaveCount(dayTypeNames.Count());
		});
	}

	[TestMethod, Owner(Bobo)]
	public async Task GetAllActiveTest()
	{
		IEnumerable<DayType> dayTypes = await RepositoryManager.DayTypeRepository.GetAllActiveAsync();
		dayTypes.Should().NotBeNullOrEmpty();
	}
}
