using Database.Adapter.Entities.MasterData;
using Database.Adapter.Repositories.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Database.Adapter.Repositories.Tests;

[TestClass]
public class MasterDataRepositoryTests
{
	private IMasterDataRepository masterDataRepository;

	[TestInitialize]
	public void TestInitialize() => masterDataRepository = new MasterDataRepository();

	[TestMethod]
	public void DayTypeRepositoryGetAllSuccessTest()
	{
		IQueryable<DayType> test = masterDataRepository.DayTypeRepository.GetAll();
		Assert.IsNotNull(test);
	}
}
