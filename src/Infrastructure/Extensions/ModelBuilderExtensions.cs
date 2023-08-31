using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

using SqlDataType = Domain.Constants.DomainConstants.Sql.DataType;
using SqlSchema = Domain.Constants.DomainConstants.Sql.Schema;

namespace Infrastructure.Extensions;

/// <summary>
/// The model builder extensions class.
/// </summary>
[SuppressMessage("Style", "IDE0058", Justification = "Not relevant here.")]
internal static class ModelBuilderExtensions
{
	/// <summary>
	/// This extension method will do the following things:
	/// <list type="bullet">
	/// <item>Creates a table for logging ddl operations</item>
	/// <item>Creates a database trigger that logs all ddl opertations</item>
	/// </list>
	/// </summary>
	/// <param name="builder">The model builder instance to extend.</param>
	/// <returns>The same builder instance so that multiple calls can be chained.</returns>
	internal static ModelBuilder AuditDatabase(this ModelBuilder builder)
	{
		builder.Entity<DatabaseLog>(entity =>
		{
			entity.ToTable(nameof(DatabaseLog), SqlSchema.PRIVATE);

			entity.HasKey(key => key.Id)
			.IsClustered(false);

			entity.Property(property => property.Application).HasDefaultValueSql("(program_name())");

			entity.Property(property => property.Login).HasDefaultValueSql("(original_login())");

			entity.Property(property => property.PostTime).HasDefaultValueSql("(getdate())");

			entity.ToSqlQuery(@"CREATE OR ALTER TRIGGER [DatabaseTriggerLog] ON DATABASE 
FOR DDL_DATABASE_LEVEL_EVENTS AS 
BEGIN
    SET NOCOUNT ON;

    DECLARE @data XML;
    DECLARE @schema sysname;
    DECLARE @object sysname;
    DECLARE @eventType sysname;

    SET @data = EVENTDATA();
    SET @eventType = @data.value('(/EVENT_INSTANCE/EventType)[1]', 'sysname');
    SET @schema = @data.value('(/EVENT_INSTANCE/SchemaName)[1]', 'sysname');
    SET @object = @data.value('(/EVENT_INSTANCE/ObjectName)[1]', 'sysname') 

    IF @object IS NOT NULL
        PRINT '  ' + @eventType + ' - ' + @schema + '.' + @object;
    ELSE
        PRINT '  ' + @eventType + ' - ' + @schema;

    IF @eventType IS NULL
        PRINT CONVERT(nvarchar(max), @data);

    INSERT [private].[DatabaseLog] ([Event], [Schema], [Object], [TSQL], [XmlEvent])
    SELECT @eventType
			, CONVERT(sysname, @schema)
			, CONVERT(sysname, @object)
			, @data.value('(/EVENT_INSTANCE/TSQLCommand)[1]', 'nvarchar(max)')
			, @data;
END;");
		});

		return builder;
	}

	private class DatabaseLog
	{
		[Key, Column(Order = 1), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; } = default!;
		[Column(TypeName = SqlDataType.DATETIME)]
		public DateTime PostTime { get; set; } = default!;
		[StringLength(128)]
		public string Login { get; set; } = default!;
		[StringLength(128)]
		public string? Application { get; set; } = default!;
		[StringLength(128)]
		public string Event { get; set; } = default!;
		[StringLength(128)]
		public string? Schema { get; set; } = default!;
		[StringLength(128)]
		public string? Object { get; set; } = default!;
		[Column("TSQL")]
		public string Tsql { get; set; } = default!;
		[Column(TypeName = SqlDataType.XML)]
		public string XmlEvent { get; set; } = default!;
	}
}
