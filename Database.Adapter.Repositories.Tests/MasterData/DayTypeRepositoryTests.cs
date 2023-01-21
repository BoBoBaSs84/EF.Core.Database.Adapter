using Database.Adapter.Base.Tests;
using Database.Adapter.Base.Tests.Helpers;
using Database.Adapter.Entities.Contexts.MasterData;
using Database.Adapter.Entities.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

namespace Database.Adapter.Repositories.Tests.MasterData;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "UnitTest")]
public class DayTypeRepositoryTests : BaseTest
{
	[TestMethod]
	public async Task GetByNameFailedTest()
	{
		string dayTypeName = RandomHelper.GetString(12);

		DayType dayType = await RepositoryManager.DayTypeRepository.GetByNameAsync(dayTypeName);

		dayType.Should().BeNull();
	}

	[TestMethod]
	public async Task GetByNameSuccessTest()
	{
		string dayTypeName = Entities.Enumerators.DayType.PLANNEDVACATION.GetName();

		DayType dayType = await RepositoryManager.DayTypeRepository.GetByNameAsync(dayTypeName);

		dayType.Should().NotBeNull();
		dayType.Name.Should().Be(dayTypeName);
	}

	[TestMethod]
	public async Task GetAllActiveTest()
	{
		IEnumerable<DayType> dayTypes = await RepositoryManager.DayTypeRepository.GetAllActiveAsync();
		dayTypes.Should().NotBeNullOrEmpty();
	}
}
