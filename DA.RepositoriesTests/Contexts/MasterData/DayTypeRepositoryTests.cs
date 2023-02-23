using DA.BaseTests.Helpers;
using DA.Models.Contexts.MasterData;
using DA.Models.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using static DA.BaseTests.Constants;
using static DA.BaseTests.Helpers.AssertionHelper;

namespace DA.RepositoriesTests.Contexts.MasterData;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "UnitTest")]
public class DayTypeRepositoryTests : RepositoriesBaseTest
{
	[TestMethod, Owner(Bobo)]
	public async Task GetByNameFailedTest()
	{
		string dayTypeName = RandomHelper.GetString(12);

		DayType dayType = await RepositoryManager.DayTypeRepository.GetByNameAsync(dayTypeName);

		dayType.Should().BeNull();
	}

	[TestMethod, Owner(Bobo)]
	public async Task GetByNamesFailedTest()
	{
		IEnumerable<string> dayTypeNames = new[]
		{
			RandomHelper.GetString(12),
			RandomHelper.GetString(12)
		};

		IEnumerable<DayType> dayTypes = await RepositoryManager.DayTypeRepository.GetByNamesAsync(dayTypeNames);

		dayTypes.Should().BeNullOrEmpty();
	}

	[TestMethod, Owner(Bobo)]
	public async Task GetByNameSuccessTest()
	{
		string dayTypeName = Models.Enumerators.DayType.PLANNEDVACATION.GetName();

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
			Models.Enumerators.DayType.PLANNEDVACATION.GetName(),
			Models.Enumerators.DayType.WORKDAY.GetName()
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
