using System;

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations.Development
{
	/// <inheritdoc />
	public partial class UpdateMigration : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
					name: "DatabaseLog",
					schema: "Private",
					columns: table => new
					{
						Id = table.Column<int>(type: "int", nullable: false)
									.Annotation("SqlServer:Identity", "1, 1"),
						PostTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
						Login = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false, defaultValueSql: "(original_login())"),
						Application = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(program_name())"),
						Event = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
						Schema = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
						Object = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
						TSQL = table.Column<string>(type: "nvarchar(max)", nullable: false),
						XmlEvent = table.Column<string>(type: "xml", nullable: false)
					},
					constraints: table =>
					{
						table.PrimaryKey("PK_DatabaseLog", x => x.Id)
											.Annotation("SqlServer:Clustered", false);
					});

			migrationBuilder.Sql(@"CREATE TRIGGER [DatabaseTriggerLog] ON DATABASE 
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

		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
					name: "DatabaseLog",
					schema: "Private");
		}
	}
}
