using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable


namespace Infrastructure.Persistence.Migrations.Development;

/// <inheritdoc />
public partial class InitialMigration : Migration
{
	/// <inheritdoc />
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		_ = migrationBuilder.EnsureSchema(
				name: "Finance");

		_ = migrationBuilder.EnsureSchema(
				name: "Private");

		_ = migrationBuilder.EnsureSchema(
				name: "Enumerator");

		_ = migrationBuilder.EnsureSchema(
				name: "Identity");

		_ = migrationBuilder.CreateTable(
				name: "Account",
				schema: "Finance",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
								.Annotation("SqlServer:Identity", "1, 1")
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "Account")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "Account")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					CreatedBy = table.Column<int>(type: "int", nullable: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "Account")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					ModifiedBy = table.Column<int>(type: "int", nullable: true)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "Account")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					IBAN = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "Account")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					Provider = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "Account")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					PeriodEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "Account")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					PeriodStart = table.Column<DateTime>(type: "datetime2", nullable: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "Account")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart")
				},
				constraints: table =>
				{
					_ = table.PrimaryKey("PK_Account", x => x.Id)
											.Annotation("SqlServer:Clustered", false);
				})
				.Annotation("SqlServer:IsTemporal", true)
				.Annotation("SqlServer:TemporalHistoryTableName", "Account")
				.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
				.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
				.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

		_ = migrationBuilder.CreateTable(
				name: "CardType",
				schema: "Enumerator",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "CardType")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "CardType")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "CardType")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "CardType")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					PeriodEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "CardType")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					PeriodStart = table.Column<DateTime>(type: "datetime2", nullable: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "CardType")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart")
				},
				constraints: table =>
				{
					_ = table.PrimaryKey("PK_CardType", x => x.Id)
											.Annotation("SqlServer:Clustered", true);
				})
				.Annotation("SqlServer:IsTemporal", true)
				.Annotation("SqlServer:TemporalHistoryTableName", "CardType")
				.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
				.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
				.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

		_ = migrationBuilder.CreateTable(
				name: "DayType",
				schema: "Enumerator",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "DayType")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "DayType")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "DayType")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "DayType")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					PeriodEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "DayType")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					PeriodStart = table.Column<DateTime>(type: "datetime2", nullable: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "DayType")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart")
				},
				constraints: table =>
				{
					_ = table.PrimaryKey("PK_DayType", x => x.Id)
											.Annotation("SqlServer:Clustered", true);
				})
				.Annotation("SqlServer:IsTemporal", true)
				.Annotation("SqlServer:TemporalHistoryTableName", "DayType")
				.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
				.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
				.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

		_ = migrationBuilder.CreateTable(
				name: "Role",
				schema: "Identity",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
								.Annotation("SqlServer:Identity", "1, 1")
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "Role")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "Role")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					PeriodEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "Role")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					PeriodStart = table.Column<DateTime>(type: "datetime2", nullable: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "Role")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "Role")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "Role")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "Role")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart")
				},
				constraints: table =>
				{
					_ = table.PrimaryKey("PK_Role", x => x.Id);
				})
				.Annotation("SqlServer:IsTemporal", true)
				.Annotation("SqlServer:TemporalHistoryTableName", "Role")
				.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
				.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
				.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

		_ = migrationBuilder.CreateTable(
				name: "Transaction",
				schema: "Finance",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
								.Annotation("SqlServer:Identity", "1, 1")
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "Transaction")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "Transaction")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					CreatedBy = table.Column<int>(type: "int", nullable: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "Transaction")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					ModifiedBy = table.Column<int>(type: "int", nullable: true)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "Transaction")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					BookingDate = table.Column<DateTime>(type: "date", nullable: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "Transaction")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					ValueDate = table.Column<DateTime>(type: "date", nullable: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "Transaction")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					PostingText = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "Transaction")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					ClientBeneficiary = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "Transaction")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					Purpose = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "Transaction")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					AccountNumber = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "Transaction")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					BankCode = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "Transaction")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					AmountEur = table.Column<decimal>(type: "money", nullable: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "Transaction")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					CreditorId = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "Transaction")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					MandateReference = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "Transaction")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					CustomerReference = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "Transaction")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					PeriodEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "Transaction")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					PeriodStart = table.Column<DateTime>(type: "datetime2", nullable: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "Transaction")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart")
				},
				constraints: table =>
				{
					_ = table.PrimaryKey("PK_Transaction", x => x.Id)
											.Annotation("SqlServer:Clustered", false);
				})
				.Annotation("SqlServer:IsTemporal", true)
				.Annotation("SqlServer:TemporalHistoryTableName", "Transaction")
				.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
				.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
				.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

		_ = migrationBuilder.CreateTable(
				name: "User",
				schema: "Identity",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
								.Annotation("SqlServer:Identity", "1, 1")
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "User")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "User")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					MiddleName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "User")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "User")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					DateOfBirth = table.Column<DateTime>(type: "date", nullable: true)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "User")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					Preferences = table.Column<string>(type: "xml", nullable: true)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "User")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					Picture = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "User")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					PeriodEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "User")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					PeriodStart = table.Column<DateTime>(type: "datetime2", nullable: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "User")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "User")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "User")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "User")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "User")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					EmailConfirmed = table.Column<bool>(type: "bit", nullable: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "User")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "User")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "User")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "User")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "User")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "User")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "User")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "User")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					LockoutEnabled = table.Column<bool>(type: "bit", nullable: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "User")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					AccessFailedCount = table.Column<int>(type: "int", nullable: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "User")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart")
				},
				constraints: table =>
				{
					_ = table.PrimaryKey("PK_User", x => x.Id);
				})
				.Annotation("SqlServer:IsTemporal", true)
				.Annotation("SqlServer:TemporalHistoryTableName", "User")
				.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
				.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
				.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

		_ = migrationBuilder.CreateTable(
				name: "CalendarDay",
				schema: "Private",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
								.Annotation("SqlServer:Identity", "1, 1")
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "CalendarDay")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "CalendarDay")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					Date = table.Column<DateTime>(type: "date", nullable: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "CalendarDay")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					Year = table.Column<int>(type: "int", nullable: false, computedColumnSql: "(datepart(year,[Date]))", stored: true)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "CalendarDay")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					Month = table.Column<int>(type: "int", nullable: false, computedColumnSql: "(datepart(month,[Date]))", stored: true)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "CalendarDay")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					Day = table.Column<int>(type: "int", nullable: false, computedColumnSql: "(datepart(day,[Date]))", stored: true)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "CalendarDay")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					Week = table.Column<int>(type: "int", nullable: false, computedColumnSql: "(datepart(week,[Date]))", stored: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "CalendarDay")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					IsoWeek = table.Column<int>(type: "int", nullable: false, computedColumnSql: "(datepart(iso_week,[Date]))", stored: true)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "CalendarDay")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					DayOfYear = table.Column<int>(type: "int", nullable: false, computedColumnSql: "(datepart(dayofyear,[Date]))", stored: true)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "CalendarDay")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					WeekDay = table.Column<int>(type: "int", nullable: false, computedColumnSql: "(datepart(weekday,[Date]))", stored: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "CalendarDay")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					EndOfMonth = table.Column<DateTime>(type: "datetime2", nullable: false, computedColumnSql: "(eomonth([Date]))", stored: true)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "CalendarDay")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					WeekDayName = table.Column<string>(type: "nvarchar(max)", nullable: false, computedColumnSql: "(datename(weekday,[Date]))", stored: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "CalendarDay")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					MonthName = table.Column<string>(type: "nvarchar(max)", nullable: false, computedColumnSql: "(datename(month,[Date]))", stored: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "CalendarDay")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					DayTypeId = table.Column<int>(type: "int", nullable: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "CalendarDay")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					PeriodEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "CalendarDay")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					PeriodStart = table.Column<DateTime>(type: "datetime2", nullable: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "CalendarDay")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart")
				},
				constraints: table =>
				{
					_ = table.PrimaryKey("PK_CalendarDay", x => x.Id)
											.Annotation("SqlServer:Clustered", false);
					_ = table.ForeignKey(
											name: "FK_CalendarDay_DayType_DayTypeId",
											column: x => x.DayTypeId,
											principalSchema: "Enumerator",
											principalTable: "DayType",
											principalColumn: "Id",
											onDelete: ReferentialAction.Restrict);
				})
				.Annotation("SqlServer:IsTemporal", true)
				.Annotation("SqlServer:TemporalHistoryTableName", "CalendarDay")
				.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
				.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
				.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

		_ = migrationBuilder.CreateTable(
				name: "RoleClaim",
				schema: "Identity",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
								.Annotation("SqlServer:Identity", "1, 1")
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "RoleClaim")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					PeriodEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "RoleClaim")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					PeriodStart = table.Column<DateTime>(type: "datetime2", nullable: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "RoleClaim")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					RoleId = table.Column<int>(type: "int", nullable: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "RoleClaim")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "RoleClaim")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "RoleClaim")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart")
				},
				constraints: table =>
				{
					_ = table.PrimaryKey("PK_RoleClaim", x => x.Id);
					_ = table.ForeignKey(
											name: "FK_RoleClaim_Role_RoleId",
											column: x => x.RoleId,
											principalSchema: "Identity",
											principalTable: "Role",
											principalColumn: "Id",
											onDelete: ReferentialAction.Cascade);
				})
				.Annotation("SqlServer:IsTemporal", true)
				.Annotation("SqlServer:TemporalHistoryTableName", "RoleClaim")
				.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
				.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
				.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

		_ = migrationBuilder.CreateTable(
				name: "AccountTransaction",
				schema: "Finance",
				columns: table => new
				{
					Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "AccountTransaction")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					CreatedBy = table.Column<int>(type: "int", nullable: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "AccountTransaction")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					ModifiedBy = table.Column<int>(type: "int", nullable: true)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "AccountTransaction")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					AccountId = table.Column<int>(type: "int", nullable: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "AccountTransaction")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					TransactionId = table.Column<int>(type: "int", nullable: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "AccountTransaction")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					PeriodEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "AccountTransaction")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					PeriodStart = table.Column<DateTime>(type: "datetime2", nullable: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "AccountTransaction")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart")
				},
				constraints: table =>
				{
					_ = table.PrimaryKey("PK_AccountTransaction", x => new { x.AccountId, x.TransactionId })
											.Annotation("SqlServer:Clustered", false);
					_ = table.ForeignKey(
											name: "FK_AccountTransaction_Account_AccountId",
											column: x => x.AccountId,
											principalSchema: "Finance",
											principalTable: "Account",
											principalColumn: "Id",
											onDelete: ReferentialAction.Restrict);
					_ = table.ForeignKey(
											name: "FK_AccountTransaction_Transaction_TransactionId",
											column: x => x.TransactionId,
											principalSchema: "Finance",
											principalTable: "Transaction",
											principalColumn: "Id",
											onDelete: ReferentialAction.Restrict);
				})
				.Annotation("SqlServer:IsTemporal", true)
				.Annotation("SqlServer:TemporalHistoryTableName", "AccountTransaction")
				.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
				.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
				.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

		_ = migrationBuilder.CreateTable(
				name: "AccountUser",
				schema: "Finance",
				columns: table => new
				{
					Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "AccountUser")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					CreatedBy = table.Column<int>(type: "int", nullable: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "AccountUser")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					ModifiedBy = table.Column<int>(type: "int", nullable: true)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "AccountUser")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					AccountId = table.Column<int>(type: "int", nullable: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "AccountUser")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					UserId = table.Column<int>(type: "int", nullable: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "AccountUser")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					PeriodEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "AccountUser")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					PeriodStart = table.Column<DateTime>(type: "datetime2", nullable: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "AccountUser")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart")
				},
				constraints: table =>
				{
					_ = table.PrimaryKey("PK_AccountUser", x => new { x.AccountId, x.UserId })
											.Annotation("SqlServer:Clustered", false);
					_ = table.ForeignKey(
											name: "FK_AccountUser_Account_AccountId",
											column: x => x.AccountId,
											principalSchema: "Finance",
											principalTable: "Account",
											principalColumn: "Id",
											onDelete: ReferentialAction.Restrict);
					_ = table.ForeignKey(
											name: "FK_AccountUser_User_UserId",
											column: x => x.UserId,
											principalSchema: "Identity",
											principalTable: "User",
											principalColumn: "Id",
											onDelete: ReferentialAction.Cascade);
				})
				.Annotation("SqlServer:IsTemporal", true)
				.Annotation("SqlServer:TemporalHistoryTableName", "AccountUser")
				.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
				.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
				.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

		_ = migrationBuilder.CreateTable(
				name: "Card",
				schema: "Finance",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
								.Annotation("SqlServer:Identity", "1, 1")
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "Card")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "Card")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					CreatedBy = table.Column<int>(type: "int", nullable: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "Card")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					ModifiedBy = table.Column<int>(type: "int", nullable: true)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "Card")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					UserId = table.Column<int>(type: "int", nullable: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "Card")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					AccountId = table.Column<int>(type: "int", nullable: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "Card")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					CardTypeId = table.Column<int>(type: "int", nullable: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "Card")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					PAN = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "Card")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					ValidUntil = table.Column<DateTime>(type: "date", nullable: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "Card")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					PeriodEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "Card")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					PeriodStart = table.Column<DateTime>(type: "datetime2", nullable: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "Card")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart")
				},
				constraints: table =>
				{
					_ = table.PrimaryKey("PK_Card", x => x.Id)
											.Annotation("SqlServer:Clustered", false);
					_ = table.ForeignKey(
											name: "FK_Card_Account_AccountId",
											column: x => x.AccountId,
											principalSchema: "Finance",
											principalTable: "Account",
											principalColumn: "Id",
											onDelete: ReferentialAction.Restrict);
					_ = table.ForeignKey(
											name: "FK_Card_CardType_CardTypeId",
											column: x => x.CardTypeId,
											principalSchema: "Enumerator",
											principalTable: "CardType",
											principalColumn: "Id",
											onDelete: ReferentialAction.Restrict);
					_ = table.ForeignKey(
											name: "FK_Card_User_UserId",
											column: x => x.UserId,
											principalSchema: "Identity",
											principalTable: "User",
											principalColumn: "Id",
											onDelete: ReferentialAction.Cascade);
				})
				.Annotation("SqlServer:IsTemporal", true)
				.Annotation("SqlServer:TemporalHistoryTableName", "Card")
				.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
				.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
				.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

		_ = migrationBuilder.CreateTable(
				name: "UserClaim",
				schema: "Identity",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
								.Annotation("SqlServer:Identity", "1, 1")
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "UserClaim")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					PeriodEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "UserClaim")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					PeriodStart = table.Column<DateTime>(type: "datetime2", nullable: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "UserClaim")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					UserId = table.Column<int>(type: "int", nullable: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "UserClaim")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "UserClaim")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "UserClaim")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart")
				},
				constraints: table =>
				{
					_ = table.PrimaryKey("PK_UserClaim", x => x.Id);
					_ = table.ForeignKey(
											name: "FK_UserClaim_User_UserId",
											column: x => x.UserId,
											principalSchema: "Identity",
											principalTable: "User",
											principalColumn: "Id",
											onDelete: ReferentialAction.Cascade);
				})
				.Annotation("SqlServer:IsTemporal", true)
				.Annotation("SqlServer:TemporalHistoryTableName", "UserClaim")
				.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
				.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
				.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

		_ = migrationBuilder.CreateTable(
				name: "UserLogin",
				schema: "Identity",
				columns: table => new
				{
					LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "UserLogin")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "UserLogin")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					PeriodEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "UserLogin")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					PeriodStart = table.Column<DateTime>(type: "datetime2", nullable: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "UserLogin")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "UserLogin")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					UserId = table.Column<int>(type: "int", nullable: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "UserLogin")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart")
				},
				constraints: table =>
				{
					_ = table.PrimaryKey("PK_UserLogin", x => new { x.LoginProvider, x.ProviderKey });
					_ = table.ForeignKey(
											name: "FK_UserLogin_User_UserId",
											column: x => x.UserId,
											principalSchema: "Identity",
											principalTable: "User",
											principalColumn: "Id",
											onDelete: ReferentialAction.Cascade);
				})
				.Annotation("SqlServer:IsTemporal", true)
				.Annotation("SqlServer:TemporalHistoryTableName", "UserLogin")
				.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
				.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
				.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

		_ = migrationBuilder.CreateTable(
				name: "UserRole",
				schema: "Identity",
				columns: table => new
				{
					UserId = table.Column<int>(type: "int", nullable: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "UserRole")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					RoleId = table.Column<int>(type: "int", nullable: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "UserRole")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					PeriodEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "UserRole")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					PeriodStart = table.Column<DateTime>(type: "datetime2", nullable: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "UserRole")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart")
				},
				constraints: table =>
				{
					_ = table.PrimaryKey("PK_UserRole", x => new { x.UserId, x.RoleId });
					_ = table.ForeignKey(
											name: "FK_UserRole_Role_RoleId",
											column: x => x.RoleId,
											principalSchema: "Identity",
											principalTable: "Role",
											principalColumn: "Id",
											onDelete: ReferentialAction.Cascade);
					_ = table.ForeignKey(
											name: "FK_UserRole_User_UserId",
											column: x => x.UserId,
											principalSchema: "Identity",
											principalTable: "User",
											principalColumn: "Id",
											onDelete: ReferentialAction.Cascade);
				})
				.Annotation("SqlServer:IsTemporal", true)
				.Annotation("SqlServer:TemporalHistoryTableName", "UserRole")
				.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
				.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
				.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

		_ = migrationBuilder.CreateTable(
				name: "UserToken",
				schema: "Identity",
				columns: table => new
				{
					UserId = table.Column<int>(type: "int", nullable: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "UserToken")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "UserToken")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "UserToken")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					PeriodEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "UserToken")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					PeriodStart = table.Column<DateTime>(type: "datetime2", nullable: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "UserToken")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "UserToken")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart")
				},
				constraints: table =>
				{
					_ = table.PrimaryKey("PK_UserToken", x => new { x.UserId, x.LoginProvider, x.Name });
					_ = table.ForeignKey(
											name: "FK_UserToken_User_UserId",
											column: x => x.UserId,
											principalSchema: "Identity",
											principalTable: "User",
											principalColumn: "Id",
											onDelete: ReferentialAction.Cascade);
				})
				.Annotation("SqlServer:IsTemporal", true)
				.Annotation("SqlServer:TemporalHistoryTableName", "UserToken")
				.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
				.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
				.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

		_ = migrationBuilder.CreateTable(
				name: "Attendance",
				schema: "Private",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
								.Annotation("SqlServer:Identity", "1, 1")
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "Attendance")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "Attendance")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					CreatedBy = table.Column<int>(type: "int", nullable: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "Attendance")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					ModifiedBy = table.Column<int>(type: "int", nullable: true)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "Attendance")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					UserId = table.Column<int>(type: "int", nullable: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "Attendance")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					CalendarDayId = table.Column<int>(type: "int", nullable: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "Attendance")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					DayTypeId = table.Column<int>(type: "int", nullable: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "Attendance")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					StartTime = table.Column<TimeSpan>(type: "time(0)", nullable: true)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "Attendance")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					EndTime = table.Column<TimeSpan>(type: "time(0)", nullable: true)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "Attendance")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					BreakTime = table.Column<TimeSpan>(type: "time(0)", nullable: true)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "Attendance")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					PeriodEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "Attendance")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					PeriodStart = table.Column<DateTime>(type: "datetime2", nullable: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "Attendance")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart")
				},
				constraints: table =>
				{
					_ = table.PrimaryKey("PK_Attendance", x => x.Id)
											.Annotation("SqlServer:Clustered", false);
					_ = table.ForeignKey(
											name: "FK_Attendance_CalendarDay_CalendarDayId",
											column: x => x.CalendarDayId,
											principalSchema: "Private",
											principalTable: "CalendarDay",
											principalColumn: "Id",
											onDelete: ReferentialAction.Restrict);
					_ = table.ForeignKey(
											name: "FK_Attendance_DayType_DayTypeId",
											column: x => x.DayTypeId,
											principalSchema: "Enumerator",
											principalTable: "DayType",
											principalColumn: "Id",
											onDelete: ReferentialAction.Restrict);
					_ = table.ForeignKey(
											name: "FK_Attendance_User_UserId",
											column: x => x.UserId,
											principalSchema: "Identity",
											principalTable: "User",
											principalColumn: "Id",
											onDelete: ReferentialAction.Cascade);
				})
				.Annotation("SqlServer:IsTemporal", true)
				.Annotation("SqlServer:TemporalHistoryTableName", "Attendance")
				.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
				.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
				.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

		_ = migrationBuilder.CreateTable(
				name: "CardTransaction",
				schema: "Finance",
				columns: table => new
				{
					Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "CardTransaction")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					CreatedBy = table.Column<int>(type: "int", nullable: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "CardTransaction")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					ModifiedBy = table.Column<int>(type: "int", nullable: true)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "CardTransaction")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					CardId = table.Column<int>(type: "int", nullable: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "CardTransaction")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					TransactionId = table.Column<int>(type: "int", nullable: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "CardTransaction")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					PeriodEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "CardTransaction")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
					PeriodStart = table.Column<DateTime>(type: "datetime2", nullable: false)
								.Annotation("SqlServer:IsTemporal", true)
								.Annotation("SqlServer:TemporalHistoryTableName", "CardTransaction")
								.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
								.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
								.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart")
				},
				constraints: table =>
				{
					_ = table.PrimaryKey("PK_CardTransaction", x => new { x.CardId, x.TransactionId })
											.Annotation("SqlServer:Clustered", false);
					_ = table.ForeignKey(
											name: "FK_CardTransaction_Card_CardId",
											column: x => x.CardId,
											principalSchema: "Finance",
											principalTable: "Card",
											principalColumn: "Id",
											onDelete: ReferentialAction.Restrict);
					_ = table.ForeignKey(
											name: "FK_CardTransaction_Transaction_TransactionId",
											column: x => x.TransactionId,
											principalSchema: "Finance",
											principalTable: "Transaction",
											principalColumn: "Id",
											onDelete: ReferentialAction.Restrict);
				})
				.Annotation("SqlServer:IsTemporal", true)
				.Annotation("SqlServer:TemporalHistoryTableName", "CardTransaction")
				.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
				.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
				.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

		_ = migrationBuilder.InsertData(
				schema: "Enumerator",
				table: "CardType",
				columns: new[] { "Id", "Description", "Name" },
				values: new object[,]
				{
										{ 1, "A credit card is a payment card issued to users to enable the cardholder to pay a merchant for goods and services based on the cardholder's accrued debt.", "Credit card" },
										{ 2, "A debit card, also known as a check card or bank card is a payment card that can be used in place of cash to make purchases.", "Debit card" }
				});

		_ = migrationBuilder.InsertData(
				schema: "Enumerator",
				table: "DayType",
				columns: new[] { "Id", "Description", "Name" },
				values: new object[,]
				{
										{ 1, "A holiday is a day set aside by custom or by law on which normal activities, especially business or work including school, are suspended or reduced.", "Holiday" },
										{ 2, "A weekday day means any day except any Saturday, any Sunday, or any day which is a legal holiday.", "Weekday" },
										{ 3, "Generally refers to the period between the end of a usual work week and the beginning of the new work week.", "Weekend day" },
										{ 4, "Day on which professional work is performed or is to be performed.", "Workday" },
										{ 5, "Weekend work means working on days that are usually non-working days.", "Weekend workday" },
										{ 6, "Is an authorised prolonged absence from work, for any reason authorised by the workplace.", "Absence" },
										{ 7, "Business travel is travel undertaken for work or business purposes, as opposed to other types of travel, such as for leisure purposes.", "Buisness trip" },
										{ 8, "In the case of a suspension, the employee is permanently or temporarily released from his or her contractual work duties.", "Suspension" },
										{ 9, "The place of work is usually in the employee's own home, and in the case of mobile work also in third locations.", "Mobile working" },
										{ 10, "Is either the plan to leave of absence from a regular job or an instance of leisure travel away from home.", "Planned vacation" },
										{ 11, "Short-time work in the employment relationship means the temporary reduction of regular working hours in a company due to a significant loss of work.", "Short time work" },
										{ 12, "The employee can no longer perform his or her most recently performed work tasks due to illness or can only do so at the risk of aggravating the illness.", "Sickness" },
										{ 13, "Is either a leave of absence from a regular job or an instance of leisure travel away from home.", "Vacation" },
										{ 14, "With the vacation block, employers prohibit their employees from taking vacation during a certain period of time.", "Vacation block" }
				});

		_ = migrationBuilder.InsertData(
				schema: "Identity",
				table: "Role",
				columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
				values: new object[,]
				{
										{ 1, "4220afd5-5886-40c9-adfd-7198d532feb8", "This is the ultimate god role ... so to say.", "Administrator", "ADMINISTRATOR" },
										{ 2, "5e3efd9e-376c-4b43-b537-2c0c1dca2015", "This is a normal user with normal user rights.", "User", "USER" },
										{ 3, "c3a36d82-f1a9-41eb-b120-aa09d6af2d71", "The user with extended user rights.", "Super user", "SUPERUSER" }
				});

		_ = migrationBuilder.CreateIndex(
				name: "IX_Account_IBAN",
				schema: "Finance",
				table: "Account",
				column: "IBAN",
				unique: true)
				.Annotation("SqlServer:Clustered", false);

		_ = migrationBuilder.CreateIndex(
				name: "IX_AccountTransaction_TransactionId",
				schema: "Finance",
				table: "AccountTransaction",
				column: "TransactionId");

		_ = migrationBuilder.CreateIndex(
				name: "IX_AccountUser_UserId",
				schema: "Finance",
				table: "AccountUser",
				column: "UserId");

		_ = migrationBuilder.CreateIndex(
				name: "IX_Attendance_CalendarDayId",
				schema: "Private",
				table: "Attendance",
				column: "CalendarDayId");

		_ = migrationBuilder.CreateIndex(
				name: "IX_Attendance_DayTypeId",
				schema: "Private",
				table: "Attendance",
				column: "DayTypeId");

		_ = migrationBuilder.CreateIndex(
				name: "IX_Attendance_UserId_CalendarDayId",
				schema: "Private",
				table: "Attendance",
				columns: new[] { "UserId", "CalendarDayId" },
				unique: true)
				.Annotation("SqlServer:Clustered", false);

		_ = migrationBuilder.CreateIndex(
				name: "IX_CalendarDay_Date",
				schema: "Private",
				table: "CalendarDay",
				column: "Date",
				unique: true);

		_ = migrationBuilder.CreateIndex(
				name: "IX_CalendarDay_DayTypeId",
				schema: "Private",
				table: "CalendarDay",
				column: "DayTypeId");

		_ = migrationBuilder.CreateIndex(
				name: "IX_CalendarDay_Month",
				schema: "Private",
				table: "CalendarDay",
				column: "Month");

		_ = migrationBuilder.CreateIndex(
				name: "IX_CalendarDay_Year",
				schema: "Private",
				table: "CalendarDay",
				column: "Year");

		_ = migrationBuilder.CreateIndex(
				name: "IX_Card_AccountId",
				schema: "Finance",
				table: "Card",
				column: "AccountId");

		_ = migrationBuilder.CreateIndex(
				name: "IX_Card_CardTypeId",
				schema: "Finance",
				table: "Card",
				column: "CardTypeId");

		_ = migrationBuilder.CreateIndex(
				name: "IX_Card_PAN",
				schema: "Finance",
				table: "Card",
				column: "PAN",
				unique: true)
				.Annotation("SqlServer:Clustered", false);

		_ = migrationBuilder.CreateIndex(
				name: "IX_Card_UserId",
				schema: "Finance",
				table: "Card",
				column: "UserId");

		_ = migrationBuilder.CreateIndex(
				name: "IX_CardTransaction_TransactionId",
				schema: "Finance",
				table: "CardTransaction",
				column: "TransactionId");

		_ = migrationBuilder.CreateIndex(
				name: "IX_CardType_Name",
				schema: "Enumerator",
				table: "CardType",
				column: "Name",
				unique: true)
				.Annotation("SqlServer:Clustered", false);

		_ = migrationBuilder.CreateIndex(
				name: "IX_DayType_Name",
				schema: "Enumerator",
				table: "DayType",
				column: "Name",
				unique: true)
				.Annotation("SqlServer:Clustered", false);

		_ = migrationBuilder.CreateIndex(
				name: "RoleNameIndex",
				schema: "Identity",
				table: "Role",
				column: "NormalizedName",
				unique: true,
				filter: "[NormalizedName] IS NOT NULL");

		_ = migrationBuilder.CreateIndex(
				name: "IX_RoleClaim_RoleId",
				schema: "Identity",
				table: "RoleClaim",
				column: "RoleId");

		_ = migrationBuilder.CreateIndex(
				name: "EmailIndex",
				schema: "Identity",
				table: "User",
				column: "NormalizedEmail");

		_ = migrationBuilder.CreateIndex(
				name: "UserNameIndex",
				schema: "Identity",
				table: "User",
				column: "NormalizedUserName",
				unique: true,
				filter: "[NormalizedUserName] IS NOT NULL");

		_ = migrationBuilder.CreateIndex(
				name: "IX_UserClaim_UserId",
				schema: "Identity",
				table: "UserClaim",
				column: "UserId");

		_ = migrationBuilder.CreateIndex(
				name: "IX_UserLogin_UserId",
				schema: "Identity",
				table: "UserLogin",
				column: "UserId");

		_ = migrationBuilder.CreateIndex(
				name: "IX_UserRole_RoleId",
				schema: "Identity",
				table: "UserRole",
				column: "RoleId");
	}

	/// <inheritdoc />
	protected override void Down(MigrationBuilder migrationBuilder)
	{
		_ = migrationBuilder.DropTable(
				name: "AccountTransaction",
				schema: "Finance")
				.Annotation("SqlServer:IsTemporal", true)
				.Annotation("SqlServer:TemporalHistoryTableName", "AccountTransaction")
				.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
				.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
				.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

		_ = migrationBuilder.DropTable(
				name: "AccountUser",
				schema: "Finance")
				.Annotation("SqlServer:IsTemporal", true)
				.Annotation("SqlServer:TemporalHistoryTableName", "AccountUser")
				.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
				.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
				.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

		_ = migrationBuilder.DropTable(
				name: "Attendance",
				schema: "Private")
				.Annotation("SqlServer:IsTemporal", true)
				.Annotation("SqlServer:TemporalHistoryTableName", "Attendance")
				.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
				.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
				.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

		_ = migrationBuilder.DropTable(
				name: "CardTransaction",
				schema: "Finance")
				.Annotation("SqlServer:IsTemporal", true)
				.Annotation("SqlServer:TemporalHistoryTableName", "CardTransaction")
				.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
				.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
				.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

		_ = migrationBuilder.DropTable(
				name: "RoleClaim",
				schema: "Identity")
				.Annotation("SqlServer:IsTemporal", true)
				.Annotation("SqlServer:TemporalHistoryTableName", "RoleClaim")
				.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
				.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
				.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

		_ = migrationBuilder.DropTable(
				name: "UserClaim",
				schema: "Identity")
				.Annotation("SqlServer:IsTemporal", true)
				.Annotation("SqlServer:TemporalHistoryTableName", "UserClaim")
				.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
				.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
				.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

		_ = migrationBuilder.DropTable(
				name: "UserLogin",
				schema: "Identity")
				.Annotation("SqlServer:IsTemporal", true)
				.Annotation("SqlServer:TemporalHistoryTableName", "UserLogin")
				.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
				.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
				.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

		_ = migrationBuilder.DropTable(
				name: "UserRole",
				schema: "Identity")
				.Annotation("SqlServer:IsTemporal", true)
				.Annotation("SqlServer:TemporalHistoryTableName", "UserRole")
				.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
				.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
				.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

		_ = migrationBuilder.DropTable(
				name: "UserToken",
				schema: "Identity")
				.Annotation("SqlServer:IsTemporal", true)
				.Annotation("SqlServer:TemporalHistoryTableName", "UserToken")
				.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
				.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
				.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

		_ = migrationBuilder.DropTable(
				name: "CalendarDay",
				schema: "Private")
				.Annotation("SqlServer:IsTemporal", true)
				.Annotation("SqlServer:TemporalHistoryTableName", "CalendarDay")
				.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
				.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
				.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

		_ = migrationBuilder.DropTable(
				name: "Card",
				schema: "Finance")
				.Annotation("SqlServer:IsTemporal", true)
				.Annotation("SqlServer:TemporalHistoryTableName", "Card")
				.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
				.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
				.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

		_ = migrationBuilder.DropTable(
				name: "Transaction",
				schema: "Finance")
				.Annotation("SqlServer:IsTemporal", true)
				.Annotation("SqlServer:TemporalHistoryTableName", "Transaction")
				.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
				.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
				.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

		_ = migrationBuilder.DropTable(
				name: "Role",
				schema: "Identity")
				.Annotation("SqlServer:IsTemporal", true)
				.Annotation("SqlServer:TemporalHistoryTableName", "Role")
				.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
				.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
				.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

		_ = migrationBuilder.DropTable(
				name: "DayType",
				schema: "Enumerator")
				.Annotation("SqlServer:IsTemporal", true)
				.Annotation("SqlServer:TemporalHistoryTableName", "DayType")
				.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
				.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
				.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

		_ = migrationBuilder.DropTable(
				name: "Account",
				schema: "Finance")
				.Annotation("SqlServer:IsTemporal", true)
				.Annotation("SqlServer:TemporalHistoryTableName", "Account")
				.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
				.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
				.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

		_ = migrationBuilder.DropTable(
				name: "CardType",
				schema: "Enumerator")
				.Annotation("SqlServer:IsTemporal", true)
				.Annotation("SqlServer:TemporalHistoryTableName", "CardType")
				.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
				.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
				.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

		_ = migrationBuilder.DropTable(
				name: "User",
				schema: "Identity")
				.Annotation("SqlServer:IsTemporal", true)
				.Annotation("SqlServer:TemporalHistoryTableName", "User")
				.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
				.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
				.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");
	}
}
