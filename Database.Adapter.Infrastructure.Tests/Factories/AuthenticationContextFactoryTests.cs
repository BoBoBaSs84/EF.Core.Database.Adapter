using Database.Adapter.Infrastructure.Contexts;
using Database.Adapter.Infrastructure.Factories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.SqlServer.Infrastructure.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

namespace Database.Adapter.Infrastructure.Tests.Factories;

[TestClass]
[SuppressMessage("Style", "IDE0058", Justification = "UnitTest")]
public class AuthenticationContextFactoryTests
{
	[TestMethod]
	public void CreateDbContextTest()
	{
		AuthenticationContext context = new(AuthenticationContextFactory.DbContextOptions);
		context.Should().NotBeNull();
	}

	[TestMethod]
	public void DbContextOptionsTest()
	{
		DbContextOptions<AuthenticationContext> options = AuthenticationContextFactory.DbContextOptions;
		options.Should().NotBeNull();
		options.Extensions.Should().NotBeNullOrEmpty();
		foreach (IDbContextOptionsExtension key in options.Extensions.Where(x=>x.Info.IsDatabaseProvider.Equals(true)))
		{
			SqlServerOptionsExtension? sqlServerOptions = key as SqlServerOptionsExtension;
			sqlServerOptions.Should().NotBeNull();
			sqlServerOptions!.ConnectionString.Should().NotBeNullOrWhiteSpace();
		}
	}
}
