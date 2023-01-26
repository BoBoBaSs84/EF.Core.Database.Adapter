using DA.Base.Tests.Helpers;
using DA.Models.Extensions;
using DA.Models.Contexts.MasterData;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

namespace DA.Repositories.Tests.Contexts.MasterData;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "UnitTest")]
public class DayTypeRepositoryTests : RepositoriesBaseTest
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
		string dayTypeName = Models.Enumerators.DayType.PLANNEDVACATION.GetName();

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
