﻿using Database.Adapter.Base.Tests.Helpers;
using Database.Adapter.Entities.Contexts.Application.MasterData;
using Database.Adapter.Repositories.Interfaces;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using System.Transactions;

namespace Database.Adapter.Repositories.Tests.MasterData;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "UnitTest")]
public class DayTypeRepositoryTests
{
	private TransactionScope transactionScope = default!;
	private IRepositoryManager repositoryManager = default!;

	[TestInitialize]
	public void TestInitialize()
	{
		transactionScope = new TransactionScope();
		repositoryManager = new RepositoryManager();
	}

	[TestCleanup]
	public void TestCleanup() => transactionScope.Dispose();

	[TestMethod]
	public void GetByNameFailedTest()
	{
		string dayTypeName = RandomHelper.GetString(12);

		DayType dayType = repositoryManager.DayTypeRepository.GetByName(dayTypeName);

		dayType.Should().BeNull();
	}

	[TestMethod]
	public void GetByNameSuccessTest()
	{
		string dayTypeName = "Holiday";

		DayType dayType = repositoryManager.DayTypeRepository.GetByName(dayTypeName);

		dayType.Should().NotBeNull();
		dayType.Name.Should().Be(dayTypeName);
	}

	[TestMethod]
	public void GetAllActiveTest()
	{
		IEnumerable<DayType> dayTypes = repositoryManager.DayTypeRepository.GetAllActive();
		dayTypes.Should().NotBeNullOrEmpty();
	}
}
