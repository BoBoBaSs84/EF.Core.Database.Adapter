using System;

using Infrastructure.Extensions;

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Persistence.Migrations
{
	/// <inheritdoc />
	public partial class InitialDeploy : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AddDatabaseEventLog();

			migrationBuilder.EnsureSchema(
					name: "finance");

			migrationBuilder.EnsureSchema(
					name: "attendance");

			migrationBuilder.EnsureSchema(
					name: "identity");

			migrationBuilder.CreateTable(
					name: "Account",
					schema: "finance",
					columns: table => new
					{
						Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()")
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Account")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Account")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						CreatedBy = table.Column<string>(type: "sysname", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Account")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						ModifiedBy = table.Column<string>(type: "sysname", nullable: true)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Account")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						IBAN = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Account")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						Type = table.Column<byte>(type: "tinyint", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Account")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						Provider = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Account")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						PeriodEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Account")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						PeriodStart = table.Column<DateTime>(type: "datetime2", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Account")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart")
					},
					constraints: table =>
					{
						table.PrimaryKey("PK_Account", x => x.Id)
											.Annotation("SqlServer:Clustered", false);
					})
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "Account")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.CreateTable(
					name: "Role",
					schema: "identity",
					columns: table => new
					{
						Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Role")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Role")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						PeriodEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Role")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						PeriodStart = table.Column<DateTime>(type: "datetime2", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Role")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Role")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Role")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Role")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart")
					},
					constraints: table =>
					{
						table.PrimaryKey("PK_Role", x => x.Id);
					})
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "Role")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.CreateTable(
					name: "Transaction",
					schema: "finance",
					columns: table => new
					{
						Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()")
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Transaction")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Transaction")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						CreatedBy = table.Column<string>(type: "sysname", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Transaction")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						ModifiedBy = table.Column<string>(type: "sysname", nullable: true)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Transaction")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						BookingDate = table.Column<DateTime>(type: "date", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Transaction")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						ValueDate = table.Column<DateTime>(type: "date", nullable: true)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Transaction")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						PostingText = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Transaction")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						ClientBeneficiary = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Transaction")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						Purpose = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Transaction")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						AccountNumber = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Transaction")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						BankCode = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Transaction")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						AmountEur = table.Column<decimal>(type: "money", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Transaction")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						CreditorId = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Transaction")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						MandateReference = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Transaction")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						CustomerReference = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Transaction")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						PeriodEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Transaction")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						PeriodStart = table.Column<DateTime>(type: "datetime2", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Transaction")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart")
					},
					constraints: table =>
					{
						table.PrimaryKey("PK_Transaction", x => x.Id)
											.Annotation("SqlServer:Clustered", false);
					})
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "Transaction")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.CreateTable(
					name: "User",
					schema: "identity",
					columns: table => new
					{
						Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "User")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "User")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						MiddleName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "User")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "User")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						DateOfBirth = table.Column<DateTime>(type: "date", nullable: true)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "User")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						Preferences = table.Column<string>(type: "xml", nullable: true)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "User")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						Picture = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "User")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						PeriodEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "User")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						PeriodStart = table.Column<DateTime>(type: "datetime2", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "User")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "User")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "User")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "User")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "User")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						EmailConfirmed = table.Column<bool>(type: "bit", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "User")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "User")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "User")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "User")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "User")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "User")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "User")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "User")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						LockoutEnabled = table.Column<bool>(type: "bit", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "User")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						AccessFailedCount = table.Column<int>(type: "int", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "User")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart")
					},
					constraints: table =>
					{
						table.PrimaryKey("PK_User", x => x.Id);
					})
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "User")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.CreateTable(
					name: "RoleClaim",
					schema: "identity",
					columns: table => new
					{
						Id = table.Column<int>(type: "int", nullable: false)
									.Annotation("SqlServer:Identity", "1, 1")
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "RoleClaim")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						PeriodEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "RoleClaim")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						PeriodStart = table.Column<DateTime>(type: "datetime2", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "RoleClaim")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "RoleClaim")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "RoleClaim")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "RoleClaim")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart")
					},
					constraints: table =>
					{
						table.PrimaryKey("PK_RoleClaim", x => x.Id);
						table.ForeignKey(
											name: "FK_RoleClaim_Role_RoleId",
											column: x => x.RoleId,
											principalSchema: "identity",
											principalTable: "Role",
											principalColumn: "Id",
											onDelete: ReferentialAction.Cascade);
					})
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "RoleClaim")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.CreateTable(
					name: "AccountTransaction",
					schema: "finance",
					columns: table => new
					{
						Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "AccountTransaction")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						CreatedBy = table.Column<string>(type: "sysname", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "AccountTransaction")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						ModifiedBy = table.Column<string>(type: "sysname", nullable: true)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "AccountTransaction")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "AccountTransaction")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						TransactionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "AccountTransaction")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						PeriodEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "AccountTransaction")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						PeriodStart = table.Column<DateTime>(type: "datetime2", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "AccountTransaction")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart")
					},
					constraints: table =>
					{
						table.PrimaryKey("PK_AccountTransaction", x => new { x.AccountId, x.TransactionId })
											.Annotation("SqlServer:Clustered", false);
						table.ForeignKey(
											name: "FK_AccountTransaction_Account_AccountId",
											column: x => x.AccountId,
											principalSchema: "finance",
											principalTable: "Account",
											principalColumn: "Id",
											onDelete: ReferentialAction.Cascade);
						table.ForeignKey(
											name: "FK_AccountTransaction_Transaction_TransactionId",
											column: x => x.TransactionId,
											principalSchema: "finance",
											principalTable: "Transaction",
											principalColumn: "Id",
											onDelete: ReferentialAction.Cascade);
					})
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "AccountTransaction")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.CreateTable(
					name: "AccountUser",
					schema: "finance",
					columns: table => new
					{
						Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "AccountUser")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						CreatedBy = table.Column<string>(type: "sysname", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "AccountUser")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						ModifiedBy = table.Column<string>(type: "sysname", nullable: true)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "AccountUser")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "AccountUser")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "AccountUser")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						PeriodEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "AccountUser")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						PeriodStart = table.Column<DateTime>(type: "datetime2", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "AccountUser")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart")
					},
					constraints: table =>
					{
						table.PrimaryKey("PK_AccountUser", x => new { x.AccountId, x.UserId })
											.Annotation("SqlServer:Clustered", false);
						table.ForeignKey(
											name: "FK_AccountUser_Account_AccountId",
											column: x => x.AccountId,
											principalSchema: "finance",
											principalTable: "Account",
											principalColumn: "Id",
											onDelete: ReferentialAction.Cascade);
						table.ForeignKey(
											name: "FK_AccountUser_User_UserId",
											column: x => x.UserId,
											principalSchema: "identity",
											principalTable: "User",
											principalColumn: "Id",
											onDelete: ReferentialAction.Cascade);
					})
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "AccountUser")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.CreateTable(
					name: "Attendance",
					schema: "attendance",
					columns: table => new
					{
						Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()")
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Attendance")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Attendance")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						CreatedBy = table.Column<string>(type: "sysname", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Attendance")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						ModifiedBy = table.Column<string>(type: "sysname", nullable: true)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Attendance")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Attendance")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						Date = table.Column<DateTime>(type: "date", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Attendance")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						Type = table.Column<byte>(type: "tinyint", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Attendance")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						StartTime = table.Column<TimeSpan>(type: "time(0)", nullable: true)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Attendance")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						EndTime = table.Column<TimeSpan>(type: "time(0)", nullable: true)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Attendance")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						BreakTime = table.Column<TimeSpan>(type: "time(0)", nullable: true)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Attendance")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						PeriodEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Attendance")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						PeriodStart = table.Column<DateTime>(type: "datetime2", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Attendance")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart")
					},
					constraints: table =>
					{
						table.PrimaryKey("PK_Attendance", x => x.Id)
											.Annotation("SqlServer:Clustered", false);
						table.ForeignKey(
											name: "FK_Attendance_User_UserId",
											column: x => x.UserId,
											principalSchema: "identity",
											principalTable: "User",
											principalColumn: "Id",
											onDelete: ReferentialAction.Cascade);
					})
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "Attendance")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.CreateTable(
					name: "Card",
					schema: "finance",
					columns: table => new
					{
						Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()")
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Card")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Card")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						CreatedBy = table.Column<string>(type: "sysname", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Card")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						ModifiedBy = table.Column<string>(type: "sysname", nullable: true)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Card")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Card")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Card")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						Type = table.Column<byte>(type: "tinyint", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Card")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						PAN = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Card")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						ValidUntil = table.Column<DateTime>(type: "date", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Card")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						PeriodEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Card")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						PeriodStart = table.Column<DateTime>(type: "datetime2", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Card")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart")
					},
					constraints: table =>
					{
						table.PrimaryKey("PK_Card", x => x.Id)
											.Annotation("SqlServer:Clustered", false);
						table.ForeignKey(
											name: "FK_Card_Account_AccountId",
											column: x => x.AccountId,
											principalSchema: "finance",
											principalTable: "Account",
											principalColumn: "Id",
											onDelete: ReferentialAction.Cascade);
						table.ForeignKey(
											name: "FK_Card_User_UserId",
											column: x => x.UserId,
											principalSchema: "identity",
											principalTable: "User",
											principalColumn: "Id",
											onDelete: ReferentialAction.Cascade);
					})
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "Card")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.CreateTable(
					name: "UserClaim",
					schema: "identity",
					columns: table => new
					{
						Id = table.Column<int>(type: "int", nullable: false)
									.Annotation("SqlServer:Identity", "1, 1")
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "UserClaim")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						PeriodEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "UserClaim")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						PeriodStart = table.Column<DateTime>(type: "datetime2", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "UserClaim")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "UserClaim")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "UserClaim")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "UserClaim")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart")
					},
					constraints: table =>
					{
						table.PrimaryKey("PK_UserClaim", x => x.Id);
						table.ForeignKey(
											name: "FK_UserClaim_User_UserId",
											column: x => x.UserId,
											principalSchema: "identity",
											principalTable: "User",
											principalColumn: "Id",
											onDelete: ReferentialAction.Cascade);
					})
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "UserClaim")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.CreateTable(
					name: "UserLogin",
					schema: "identity",
					columns: table => new
					{
						LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "UserLogin")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "UserLogin")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						PeriodEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "UserLogin")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						PeriodStart = table.Column<DateTime>(type: "datetime2", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "UserLogin")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "UserLogin")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "UserLogin")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart")
					},
					constraints: table =>
					{
						table.PrimaryKey("PK_UserLogin", x => new { x.LoginProvider, x.ProviderKey });
						table.ForeignKey(
											name: "FK_UserLogin_User_UserId",
											column: x => x.UserId,
											principalSchema: "identity",
											principalTable: "User",
											principalColumn: "Id",
											onDelete: ReferentialAction.Cascade);
					})
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "UserLogin")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.CreateTable(
					name: "UserRole",
					schema: "identity",
					columns: table => new
					{
						UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "UserRole")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "UserRole")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						PeriodEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "UserRole")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						PeriodStart = table.Column<DateTime>(type: "datetime2", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "UserRole")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart")
					},
					constraints: table =>
					{
						table.PrimaryKey("PK_UserRole", x => new { x.UserId, x.RoleId });
						table.ForeignKey(
											name: "FK_UserRole_Role_RoleId",
											column: x => x.RoleId,
											principalSchema: "identity",
											principalTable: "Role",
											principalColumn: "Id",
											onDelete: ReferentialAction.Cascade);
						table.ForeignKey(
											name: "FK_UserRole_User_UserId",
											column: x => x.UserId,
											principalSchema: "identity",
											principalTable: "User",
											principalColumn: "Id",
											onDelete: ReferentialAction.Cascade);
					})
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "UserRole")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.CreateTable(
					name: "UserToken",
					schema: "identity",
					columns: table => new
					{
						UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "UserToken")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "UserToken")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "UserToken")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						PeriodEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "UserToken")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						PeriodStart = table.Column<DateTime>(type: "datetime2", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "UserToken")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "UserToken")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart")
					},
					constraints: table =>
					{
						table.PrimaryKey("PK_UserToken", x => new { x.UserId, x.LoginProvider, x.Name });
						table.ForeignKey(
											name: "FK_UserToken_User_UserId",
											column: x => x.UserId,
											principalSchema: "identity",
											principalTable: "User",
											principalColumn: "Id",
											onDelete: ReferentialAction.Cascade);
					})
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "UserToken")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.CreateTable(
					name: "CardTransaction",
					schema: "finance",
					columns: table => new
					{
						Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "CardTransaction")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						CreatedBy = table.Column<string>(type: "sysname", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "CardTransaction")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						ModifiedBy = table.Column<string>(type: "sysname", nullable: true)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "CardTransaction")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						CardId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "CardTransaction")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						TransactionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "CardTransaction")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						PeriodEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "CardTransaction")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						PeriodStart = table.Column<DateTime>(type: "datetime2", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "CardTransaction")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart")
					},
					constraints: table =>
					{
						table.PrimaryKey("PK_CardTransaction", x => new { x.CardId, x.TransactionId })
											.Annotation("SqlServer:Clustered", false);
						table.ForeignKey(
											name: "FK_CardTransaction_Card_CardId",
											column: x => x.CardId,
											principalSchema: "finance",
											principalTable: "Card",
											principalColumn: "Id",
											onDelete: ReferentialAction.Cascade);
						table.ForeignKey(
											name: "FK_CardTransaction_Transaction_TransactionId",
											column: x => x.TransactionId,
											principalSchema: "finance",
											principalTable: "Transaction",
											principalColumn: "Id",
											onDelete: ReferentialAction.Cascade);
					})
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "CardTransaction")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.InsertData(
					schema: "identity",
					table: "Role",
					columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
					values: new object[,]
					{
										{ new Guid("3fb0d2c8-a452-46cc-a9e7-ff96e3ea11f8"), null, "This is the ultimate god role ... so to say.", "Administrator", "ADMINISTRATOR" },
										{ new Guid("70295553-c9db-4150-b013-30632611b551"), null, "This is a normal user with normal user rights.", "User", "USER" },
										{ new Guid("83e23b39-b89f-4367-a4ea-7c05b1e4a1f2"), null, "The user with extended user rights.", "Super user", "SUPERUSER" }
					});

			migrationBuilder.CreateIndex(
					name: "IX_Account_IBAN",
					schema: "finance",
					table: "Account",
					column: "IBAN",
					unique: true)
					.Annotation("SqlServer:Clustered", false);

			migrationBuilder.CreateIndex(
					name: "IX_AccountTransaction_TransactionId",
					schema: "finance",
					table: "AccountTransaction",
					column: "TransactionId");

			migrationBuilder.CreateIndex(
					name: "IX_AccountUser_UserId",
					schema: "finance",
					table: "AccountUser",
					column: "UserId");

			migrationBuilder.CreateIndex(
					name: "IX_Attendance_UserId_Date",
					schema: "attendance",
					table: "Attendance",
					columns: new[] { "UserId", "Date" },
					unique: true)
					.Annotation("SqlServer:Clustered", false);

			migrationBuilder.CreateIndex(
					name: "IX_Card_AccountId",
					schema: "finance",
					table: "Card",
					column: "AccountId");

			migrationBuilder.CreateIndex(
					name: "IX_Card_PAN",
					schema: "finance",
					table: "Card",
					column: "PAN",
					unique: true)
					.Annotation("SqlServer:Clustered", false);

			migrationBuilder.CreateIndex(
					name: "IX_Card_UserId",
					schema: "finance",
					table: "Card",
					column: "UserId");

			migrationBuilder.CreateIndex(
					name: "IX_CardTransaction_TransactionId",
					schema: "finance",
					table: "CardTransaction",
					column: "TransactionId");

			migrationBuilder.CreateIndex(
					name: "RoleNameIndex",
					schema: "identity",
					table: "Role",
					column: "NormalizedName",
					unique: true,
					filter: "[NormalizedName] IS NOT NULL");

			migrationBuilder.CreateIndex(
					name: "IX_RoleClaim_RoleId",
					schema: "identity",
					table: "RoleClaim",
					column: "RoleId");

			migrationBuilder.CreateIndex(
					name: "EmailIndex",
					schema: "identity",
					table: "User",
					column: "NormalizedEmail");

			migrationBuilder.CreateIndex(
					name: "UserNameIndex",
					schema: "identity",
					table: "User",
					column: "NormalizedUserName",
					unique: true,
					filter: "[NormalizedUserName] IS NOT NULL");

			migrationBuilder.CreateIndex(
					name: "IX_UserClaim_UserId",
					schema: "identity",
					table: "UserClaim",
					column: "UserId");

			migrationBuilder.CreateIndex(
					name: "IX_UserLogin_UserId",
					schema: "identity",
					table: "UserLogin",
					column: "UserId");

			migrationBuilder.CreateIndex(
					name: "IX_UserRole_RoleId",
					schema: "identity",
					table: "UserRole",
					column: "RoleId");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
					name: "AccountTransaction",
					schema: "finance")
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "AccountTransaction")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.DropTable(
					name: "AccountUser",
					schema: "finance")
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "AccountUser")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.DropTable(
					name: "Attendance",
					schema: "attendance")
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "Attendance")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.DropTable(
					name: "CardTransaction",
					schema: "finance")
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "CardTransaction")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.DropTable(
					name: "RoleClaim",
					schema: "identity")
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "RoleClaim")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.DropTable(
					name: "UserClaim",
					schema: "identity")
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "UserClaim")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.DropTable(
					name: "UserLogin",
					schema: "identity")
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "UserLogin")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.DropTable(
					name: "UserRole",
					schema: "identity")
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "UserRole")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.DropTable(
					name: "UserToken",
					schema: "identity")
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "UserToken")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.DropTable(
					name: "Card",
					schema: "finance")
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "Card")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.DropTable(
					name: "Transaction",
					schema: "finance")
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "Transaction")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.DropTable(
					name: "Role",
					schema: "identity")
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "Role")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.DropTable(
					name: "Account",
					schema: "finance")
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "Account")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.DropTable(
					name: "User",
					schema: "identity")
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "User")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");
		}
	}
}
