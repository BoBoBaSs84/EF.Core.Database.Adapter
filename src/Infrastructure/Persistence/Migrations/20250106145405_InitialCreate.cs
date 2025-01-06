using System;

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Persistence.Migrations
{
	/// <inheritdoc />
	public partial class InitialCreate : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.EnsureSchema(
					name: "finance");

			migrationBuilder.EnsureSchema(
					name: "attendance");

			migrationBuilder.EnsureSchema(
					name: "documents");

			migrationBuilder.EnsureSchema(
					name: "todo");

			migrationBuilder.EnsureSchema(
					name: "identity");

			migrationBuilder.CreateTable(
					name: "Account",
					schema: "finance",
					columns: table => new
					{
						Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
						Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
						Creator = table.Column<string>(type: "sysname", nullable: false),
						Editor = table.Column<string>(type: "sysname", nullable: true),
						IBAN = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: false),
						Type = table.Column<byte>(type: "tinyint", nullable: false),
						Provider = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
						PeriodEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
									.Annotation("SqlServer:TemporalIsPeriodEndColumn", true),
						PeriodStart = table.Column<DateTime>(type: "datetime2", nullable: false)
									.Annotation("SqlServer:TemporalIsPeriodStartColumn", true)
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
					name: "Data",
					schema: "documents",
					columns: table => new
					{
						Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
						Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
						Creator = table.Column<string>(type: "sysname", nullable: false),
						Editor = table.Column<string>(type: "sysname", nullable: true),
						MD5Hash = table.Column<byte[]>(type: "binary(16)", nullable: false),
						Length = table.Column<long>(type: "bigint", nullable: false),
						Content = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
						PeriodEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
									.Annotation("SqlServer:TemporalIsPeriodEndColumn", true),
						PeriodStart = table.Column<DateTime>(type: "datetime2", nullable: false)
									.Annotation("SqlServer:TemporalIsPeriodStartColumn", true)
					},
					constraints: table =>
					{
						table.PrimaryKey("PK_Data", x => x.Id)
											.Annotation("SqlServer:Clustered", false);
					})
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "Data")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.CreateTable(
					name: "Extension",
					schema: "documents",
					columns: table => new
					{
						Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
						Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
						Creator = table.Column<string>(type: "sysname", nullable: false),
						Editor = table.Column<string>(type: "sysname", nullable: true),
						Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
						MimeType = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
						PeriodEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
									.Annotation("SqlServer:TemporalIsPeriodEndColumn", true),
						PeriodStart = table.Column<DateTime>(type: "datetime2", nullable: false)
									.Annotation("SqlServer:TemporalIsPeriodStartColumn", true)
					},
					constraints: table =>
					{
						table.PrimaryKey("PK_Extension", x => x.Id)
											.Annotation("SqlServer:Clustered", false);
					})
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "Extension")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.CreateTable(
					name: "Role",
					schema: "identity",
					columns: table => new
					{
						Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
						Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
						PeriodEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
									.Annotation("SqlServer:TemporalIsPeriodEndColumn", true),
						PeriodStart = table.Column<DateTime>(type: "datetime2", nullable: false)
									.Annotation("SqlServer:TemporalIsPeriodStartColumn", true),
						Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
						NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
						ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
						Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
						Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
						Creator = table.Column<string>(type: "sysname", nullable: false),
						Editor = table.Column<string>(type: "sysname", nullable: true),
						BookingDate = table.Column<DateTime>(type: "date", nullable: false),
						ValueDate = table.Column<DateTime>(type: "date", nullable: true),
						PostingText = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
						ClientBeneficiary = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
						Purpose = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
						AccountNumber = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: false),
						BankCode = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: false),
						AmountEur = table.Column<decimal>(type: "money", nullable: false),
						CreditorId = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
						MandateReference = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
						CustomerReference = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
						PeriodEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
									.Annotation("SqlServer:TemporalIsPeriodEndColumn", true),
						PeriodStart = table.Column<DateTime>(type: "datetime2", nullable: false)
									.Annotation("SqlServer:TemporalIsPeriodStartColumn", true)
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
						Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
						FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
						MiddleName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
						LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
						DateOfBirth = table.Column<DateTime>(type: "date", nullable: true),
						Preferences = table.Column<string>(type: "xml", nullable: true),
						Picture = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
						PeriodEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
									.Annotation("SqlServer:TemporalIsPeriodEndColumn", true),
						PeriodStart = table.Column<DateTime>(type: "datetime2", nullable: false)
									.Annotation("SqlServer:TemporalIsPeriodStartColumn", true),
						UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
						NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
						Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
						NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
						EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
						PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
						SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
						ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
						PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
						PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
						TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
						LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
						LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
						AccessFailedCount = table.Column<int>(type: "int", nullable: false)
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
									.Annotation("SqlServer:Identity", "1, 1"),
						PeriodEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
									.Annotation("SqlServer:TemporalIsPeriodEndColumn", true),
						PeriodStart = table.Column<DateTime>(type: "datetime2", nullable: false)
									.Annotation("SqlServer:TemporalIsPeriodStartColumn", true),
						RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
						ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
						ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
						Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
						Creator = table.Column<string>(type: "sysname", nullable: false),
						Editor = table.Column<string>(type: "sysname", nullable: true),
						AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
						TransactionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
						PeriodEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
									.Annotation("SqlServer:TemporalIsPeriodEndColumn", true),
						PeriodStart = table.Column<DateTime>(type: "datetime2", nullable: false)
									.Annotation("SqlServer:TemporalIsPeriodStartColumn", true)
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
						Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
						Creator = table.Column<string>(type: "sysname", nullable: false),
						Editor = table.Column<string>(type: "sysname", nullable: true),
						AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
						UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
						PeriodEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
									.Annotation("SqlServer:TemporalIsPeriodEndColumn", true),
						PeriodStart = table.Column<DateTime>(type: "datetime2", nullable: false)
									.Annotation("SqlServer:TemporalIsPeriodStartColumn", true)
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
						Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
						Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
						Creator = table.Column<string>(type: "sysname", nullable: false),
						Editor = table.Column<string>(type: "sysname", nullable: true),
						UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
						Date = table.Column<DateTime>(type: "date", nullable: false),
						Type = table.Column<byte>(type: "tinyint", nullable: false),
						StartTime = table.Column<TimeSpan>(type: "time(0)", nullable: true),
						EndTime = table.Column<TimeSpan>(type: "time(0)", nullable: true),
						BreakTime = table.Column<TimeSpan>(type: "time(0)", nullable: true),
						PeriodEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
									.Annotation("SqlServer:TemporalIsPeriodEndColumn", true),
						PeriodStart = table.Column<DateTime>(type: "datetime2", nullable: false)
									.Annotation("SqlServer:TemporalIsPeriodStartColumn", true)
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
						Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
						Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
						Creator = table.Column<string>(type: "sysname", nullable: false),
						Editor = table.Column<string>(type: "sysname", nullable: true),
						UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
						AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
						Type = table.Column<byte>(type: "tinyint", nullable: false),
						PAN = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: false),
						ValidUntil = table.Column<DateTime>(type: "date", nullable: false),
						PeriodEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
									.Annotation("SqlServer:TemporalIsPeriodEndColumn", true),
						PeriodStart = table.Column<DateTime>(type: "datetime2", nullable: false)
									.Annotation("SqlServer:TemporalIsPeriodStartColumn", true)
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
					name: "Document",
					schema: "documents",
					columns: table => new
					{
						Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
						Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
						Creator = table.Column<string>(type: "sysname", nullable: false),
						Editor = table.Column<string>(type: "sysname", nullable: true),
						Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
						Directory = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
						Flags = table.Column<long>(type: "bigint", nullable: false),
						CreationTime = table.Column<DateTime>(type: "smalldatetime", nullable: false),
						LastWriteTime = table.Column<DateTime>(type: "smalldatetime", nullable: true),
						LastAccessTime = table.Column<DateTime>(type: "smalldatetime", nullable: true),
						DataId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
						ExtensionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
						UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
						PeriodEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
									.Annotation("SqlServer:TemporalIsPeriodEndColumn", true),
						PeriodStart = table.Column<DateTime>(type: "datetime2", nullable: false)
									.Annotation("SqlServer:TemporalIsPeriodStartColumn", true)
					},
					constraints: table =>
					{
						table.PrimaryKey("PK_Document", x => x.Id)
											.Annotation("SqlServer:Clustered", false);
						table.ForeignKey(
											name: "FK_Document_Data_DataId",
											column: x => x.DataId,
											principalSchema: "documents",
											principalTable: "Data",
											principalColumn: "Id",
											onDelete: ReferentialAction.Cascade);
						table.ForeignKey(
											name: "FK_Document_Extension_ExtensionId",
											column: x => x.ExtensionId,
											principalSchema: "documents",
											principalTable: "Extension",
											principalColumn: "Id",
											onDelete: ReferentialAction.Cascade);
						table.ForeignKey(
											name: "FK_Document_User_UserId",
											column: x => x.UserId,
											principalSchema: "identity",
											principalTable: "User",
											principalColumn: "Id",
											onDelete: ReferentialAction.Cascade);
					})
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "Document")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.CreateTable(
					name: "List",
					schema: "todo",
					columns: table => new
					{
						Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
						Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
						Creator = table.Column<string>(type: "sysname", nullable: false),
						Editor = table.Column<string>(type: "sysname", nullable: true),
						Title = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
						Color = table.Column<byte[]>(type: "binary(3)", nullable: true),
						UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
						PeriodEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
									.Annotation("SqlServer:TemporalIsPeriodEndColumn", true),
						PeriodStart = table.Column<DateTime>(type: "datetime2", nullable: false)
									.Annotation("SqlServer:TemporalIsPeriodStartColumn", true)
					},
					constraints: table =>
					{
						table.PrimaryKey("PK_List", x => x.Id)
											.Annotation("SqlServer:Clustered", false);
						table.ForeignKey(
											name: "FK_List_User_UserId",
											column: x => x.UserId,
											principalSchema: "identity",
											principalTable: "User",
											principalColumn: "Id",
											onDelete: ReferentialAction.Cascade);
					})
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "List")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.CreateTable(
					name: "UserClaim",
					schema: "identity",
					columns: table => new
					{
						Id = table.Column<int>(type: "int", nullable: false)
									.Annotation("SqlServer:Identity", "1, 1"),
						PeriodEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
									.Annotation("SqlServer:TemporalIsPeriodEndColumn", true),
						PeriodStart = table.Column<DateTime>(type: "datetime2", nullable: false)
									.Annotation("SqlServer:TemporalIsPeriodStartColumn", true),
						UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
						ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
						ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
						LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
						ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
						PeriodEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
									.Annotation("SqlServer:TemporalIsPeriodEndColumn", true),
						PeriodStart = table.Column<DateTime>(type: "datetime2", nullable: false)
									.Annotation("SqlServer:TemporalIsPeriodStartColumn", true),
						ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
						UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
						UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
						RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
						PeriodEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
									.Annotation("SqlServer:TemporalIsPeriodEndColumn", true),
						PeriodStart = table.Column<DateTime>(type: "datetime2", nullable: false)
									.Annotation("SqlServer:TemporalIsPeriodStartColumn", true)
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
						UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
						LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
						Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
						PeriodEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
									.Annotation("SqlServer:TemporalIsPeriodEndColumn", true),
						PeriodStart = table.Column<DateTime>(type: "datetime2", nullable: false)
									.Annotation("SqlServer:TemporalIsPeriodStartColumn", true),
						Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
						Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
						Creator = table.Column<string>(type: "sysname", nullable: false),
						Editor = table.Column<string>(type: "sysname", nullable: true),
						CardId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
						TransactionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
						PeriodEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
									.Annotation("SqlServer:TemporalIsPeriodEndColumn", true),
						PeriodStart = table.Column<DateTime>(type: "datetime2", nullable: false)
									.Annotation("SqlServer:TemporalIsPeriodStartColumn", true)
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

			migrationBuilder.CreateTable(
					name: "Item",
					schema: "todo",
					columns: table => new
					{
						Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
						Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
						Creator = table.Column<string>(type: "sysname", nullable: false),
						Editor = table.Column<string>(type: "sysname", nullable: true),
						ListId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
						Title = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
						Note = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
						Priority = table.Column<byte>(type: "tinyint", nullable: false),
						Reminder = table.Column<DateTime>(type: "datetime2", nullable: true),
						Done = table.Column<bool>(type: "bit", nullable: false),
						PeriodEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
									.Annotation("SqlServer:TemporalIsPeriodEndColumn", true),
						PeriodStart = table.Column<DateTime>(type: "datetime2", nullable: false)
									.Annotation("SqlServer:TemporalIsPeriodStartColumn", true)
					},
					constraints: table =>
					{
						table.PrimaryKey("PK_Item", x => x.Id)
											.Annotation("SqlServer:Clustered", false);
						table.ForeignKey(
											name: "FK_Item_List_ListId",
											column: x => x.ListId,
											principalSchema: "todo",
											principalTable: "List",
											principalColumn: "Id",
											onDelete: ReferentialAction.Cascade);
					})
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "Item")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.InsertData(
					schema: "identity",
					table: "Role",
					columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
					values: new object[,]
					{
										{ new Guid("1c155003-9817-4e56-be8e-14d8bd3faa33"), null, "The user with extended user rights.", "Super user", "SUPERUSER" },
										{ new Guid("5de724b3-f1c2-4cbc-b148-745befb6229e"), null, "This is a normal user with normal user rights.", "User", "USER" },
										{ new Guid("9d7c9575-1508-4e82-a7d7-b43e7723b63e"), null, "This is the ultimate god role ... so to say.", "Administrator", "ADMINISTRATOR" }
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
					name: "IX_Data_MD5Hash",
					schema: "documents",
					table: "Data",
					column: "MD5Hash",
					unique: true)
					.Annotation("SqlServer:Clustered", false);

			migrationBuilder.CreateIndex(
					name: "IX_Document_DataId",
					schema: "documents",
					table: "Document",
					column: "DataId");

			migrationBuilder.CreateIndex(
					name: "IX_Document_ExtensionId",
					schema: "documents",
					table: "Document",
					column: "ExtensionId");

			migrationBuilder.CreateIndex(
					name: "IX_Document_UserId",
					schema: "documents",
					table: "Document",
					column: "UserId");

			migrationBuilder.CreateIndex(
					name: "IX_Extension_Name",
					schema: "documents",
					table: "Extension",
					column: "Name",
					unique: true)
					.Annotation("SqlServer:Clustered", false);

			migrationBuilder.CreateIndex(
					name: "IX_Item_ListId",
					schema: "todo",
					table: "Item",
					column: "ListId");

			migrationBuilder.CreateIndex(
					name: "IX_List_UserId",
					schema: "todo",
					table: "List",
					column: "UserId");

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
					name: "Document",
					schema: "documents")
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "Document")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.DropTable(
					name: "Item",
					schema: "todo")
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "Item")
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
					name: "Data",
					schema: "documents")
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "Data")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.DropTable(
					name: "Extension",
					schema: "documents")
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "Extension")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.DropTable(
					name: "List",
					schema: "todo")
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "List")
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
