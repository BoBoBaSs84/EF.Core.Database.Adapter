using Microsoft.VisualStudio.TestTools.UnitTesting;
using Database.Adapter.Infrastructure.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Adapter.Infrastructure.Configurations.Tests;

[TestClass]
public class ConfigurationTests
{
	[TestMethod]
	public void GetConnectionStringSuccessTest()
	{
		Configuration configuration = new();
		string connectionString = configuration.GetConnectionString("MasterContext");
		Assert.IsFalse(string.IsNullOrWhiteSpace(connectionString));
	}
}