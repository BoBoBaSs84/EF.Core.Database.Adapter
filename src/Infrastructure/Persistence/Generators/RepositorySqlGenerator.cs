using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Update;

namespace Infrastructure.Persistence.Generators;

internal sealed class RepositorySqlGenerator : SqlServerMigrationsSqlGenerator
{
	private bool DbAuditCreated;

	public RepositorySqlGenerator(MigrationsSqlGeneratorDependencies dependencies, ICommandBatchPreparer commandBatchPreparer) : base(dependencies, commandBatchPreparer)
	{
	}

	protected override void Generate(MigrationOperation operation, IModel? model, MigrationCommandListBuilder builder)
	{
		if (operation is EnsureSchemaOperation schemaOperation)
		{
			if (model is not null)
			{

			}
		}

		base.Generate(operation, model, builder);
	}
}
