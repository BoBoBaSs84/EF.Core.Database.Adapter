﻿using System;

using Infrastructure.Extensions;

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Persistence.Migrations
{
	/// <inheritdoc />
	public partial class PreferencesUpdate : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateDatabaseLog();

			migrationBuilder.EnsureSchema(
					name: "Finance");

			migrationBuilder.EnsureSchema(
					name: "Attendance");

			migrationBuilder.EnsureSchema(
					name: "Common");

			migrationBuilder.EnsureSchema(
					name: "Identity");

			migrationBuilder.CreateTable(
					name: "Account",
					schema: "Finance",
					columns: table => new
					{
						Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
						CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Account")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
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
						table.PrimaryKey("PK_Account", x => x.Id)
											.Annotation("SqlServer:Clustered", false);
					})
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "Account")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.CreateTable(
					name: "Calendar",
					schema: "Common",
					columns: table => new
					{
						Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Calendar")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Calendar")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						Date = table.Column<DateTime>(type: "date", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Calendar")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						PeriodEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Calendar")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						PeriodStart = table.Column<DateTime>(type: "datetime2", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Calendar")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart")
					},
					constraints: table =>
					{
						table.PrimaryKey("PK_Calendar", x => x.Id)
											.Annotation("SqlServer:Clustered", false);
					})
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "Calendar")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.CreateTable(
					name: "Role",
					schema: "Identity",
					columns: table => new
					{
						Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
						table.PrimaryKey("PK_Role", x => x.Id);
					})
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "Role")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.CreateTable(
					name: "Transaction",
					schema: "Finance",
					columns: table => new
					{
						Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
						CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Transaction")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
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
						table.PrimaryKey("PK_Transaction", x => x.Id)
											.Annotation("SqlServer:Clustered", false);
					})
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "Transaction")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.CreateTable(
					name: "User",
					schema: "Identity",
					columns: table => new
					{
						Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
						table.PrimaryKey("PK_User", x => x.Id);
					})
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "User")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.CreateTable(
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
						RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
						table.PrimaryKey("PK_RoleClaim", x => x.Id);
						table.ForeignKey(
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

			migrationBuilder.CreateTable(
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
						CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "AccountTransaction")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "AccountTransaction")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "AccountTransaction")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						TransactionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
						table.PrimaryKey("PK_AccountTransaction", x => new { x.AccountId, x.TransactionId })
											.Annotation("SqlServer:Clustered", false);
						table.ForeignKey(
											name: "FK_AccountTransaction_Account_AccountId",
											column: x => x.AccountId,
											principalSchema: "Finance",
											principalTable: "Account",
											principalColumn: "Id",
											onDelete: ReferentialAction.Cascade);
						table.ForeignKey(
											name: "FK_AccountTransaction_Transaction_TransactionId",
											column: x => x.TransactionId,
											principalSchema: "Finance",
											principalTable: "Transaction",
											principalColumn: "Id",
											onDelete: ReferentialAction.Cascade);
					})
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "AccountTransaction")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.CreateTable(
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
						CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "AccountUser")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "AccountUser")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "AccountUser")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
						table.PrimaryKey("PK_AccountUser", x => new { x.AccountId, x.UserId })
											.Annotation("SqlServer:Clustered", false);
						table.ForeignKey(
											name: "FK_AccountUser_Account_AccountId",
											column: x => x.AccountId,
											principalSchema: "Finance",
											principalTable: "Account",
											principalColumn: "Id",
											onDelete: ReferentialAction.Cascade);
						table.ForeignKey(
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

			migrationBuilder.CreateTable(
					name: "Attendance",
					schema: "Attendance",
					columns: table => new
					{
						Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
						CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Attendance")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Attendance")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Attendance")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						CalendarId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Attendance")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						AttendanceType = table.Column<int>(type: "int", nullable: false)
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
						table.PrimaryKey("PK_Attendance", x => x.Id)
											.Annotation("SqlServer:Clustered", false);
						table.ForeignKey(
											name: "FK_Attendance_Calendar_CalendarId",
											column: x => x.CalendarId,
											principalSchema: "Common",
											principalTable: "Calendar",
											principalColumn: "Id",
											onDelete: ReferentialAction.Restrict);
						table.ForeignKey(
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

			migrationBuilder.CreateTable(
					name: "Card",
					schema: "Finance",
					columns: table => new
					{
						Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
						CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Card")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Card")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Card")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						AccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Card")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						CardType = table.Column<int>(type: "int", nullable: false)
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
						table.PrimaryKey("PK_Card", x => x.Id)
											.Annotation("SqlServer:Clustered", false);
						table.ForeignKey(
											name: "FK_Card_Account_AccountId",
											column: x => x.AccountId,
											principalSchema: "Finance",
											principalTable: "Account",
											principalColumn: "Id",
											onDelete: ReferentialAction.Cascade);
						table.ForeignKey(
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

			migrationBuilder.CreateTable(
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
						UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
						table.PrimaryKey("PK_UserClaim", x => x.Id);
						table.ForeignKey(
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

			migrationBuilder.CreateTable(
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
						UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "UserLogin")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart")
					},
					constraints: table =>
					{
						table.PrimaryKey("PK_UserLogin", x => new { x.LoginProvider, x.ProviderKey });
						table.ForeignKey(
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

			migrationBuilder.CreateTable(
					name: "UserRole",
					schema: "Identity",
					columns: table => new
					{
						UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "UserRole")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
						table.PrimaryKey("PK_UserRole", x => new { x.UserId, x.RoleId });
						table.ForeignKey(
											name: "FK_UserRole_Role_RoleId",
											column: x => x.RoleId,
											principalSchema: "Identity",
											principalTable: "Role",
											principalColumn: "Id",
											onDelete: ReferentialAction.Cascade);
						table.ForeignKey(
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

			migrationBuilder.CreateTable(
					name: "UserToken",
					schema: "Identity",
					columns: table => new
					{
						UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
						table.PrimaryKey("PK_UserToken", x => new { x.UserId, x.LoginProvider, x.Name });
						table.ForeignKey(
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

			migrationBuilder.CreateTable(
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
						CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "CardTransaction")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "CardTransaction")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						CardId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "CardTransaction")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						TransactionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
						table.PrimaryKey("PK_CardTransaction", x => new { x.CardId, x.TransactionId })
											.Annotation("SqlServer:Clustered", false);
						table.ForeignKey(
											name: "FK_CardTransaction_Card_CardId",
											column: x => x.CardId,
											principalSchema: "Finance",
											principalTable: "Card",
											principalColumn: "Id",
											onDelete: ReferentialAction.Cascade);
						table.ForeignKey(
											name: "FK_CardTransaction_Transaction_TransactionId",
											column: x => x.TransactionId,
											principalSchema: "Finance",
											principalTable: "Transaction",
											principalColumn: "Id",
											onDelete: ReferentialAction.Cascade);
					})
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "CardTransaction")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.InsertData(
					schema: "Identity",
					table: "Role",
					columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
					values: new object[,]
					{
										{ new Guid("19d6a624-529b-440c-84cb-e02635c03413"), "14d37d25-20c3-410c-903a-e7d4ac5a8f05", "This is the ultimate god role ... so to say.", "Administrator", "ADMINISTRATOR" },
										{ new Guid("24aafab6-9179-4e34-920e-70d3e352a479"), "4b51e445-aa0e-4203-ae1c-c708d029a453", "The user with extended user rights.", "Super user", "SUPERUSER" },
										{ new Guid("8fa8f063-865a-47de-a3b0-23148d27ece7"), "2ac4ef7d-f8de-4d32-8419-414e636cb047", "This is a normal user with normal user rights.", "User", "USER" }
					});

			migrationBuilder.CreateIndex(
					name: "IX_Account_IBAN",
					schema: "Finance",
					table: "Account",
					column: "IBAN",
					unique: true)
					.Annotation("SqlServer:Clustered", false);

			migrationBuilder.CreateIndex(
					name: "IX_AccountTransaction_TransactionId",
					schema: "Finance",
					table: "AccountTransaction",
					column: "TransactionId");

			migrationBuilder.CreateIndex(
					name: "IX_AccountUser_UserId",
					schema: "Finance",
					table: "AccountUser",
					column: "UserId");

			migrationBuilder.CreateIndex(
					name: "IX_Attendance_CalendarId",
					schema: "Attendance",
					table: "Attendance",
					column: "CalendarId");

			migrationBuilder.CreateIndex(
					name: "IX_Attendance_UserId_CalendarId",
					schema: "Attendance",
					table: "Attendance",
					columns: new[] { "UserId", "CalendarId" },
					unique: true)
					.Annotation("SqlServer:Clustered", false);

			migrationBuilder.CreateIndex(
					name: "IX_Calendar_Date",
					schema: "Common",
					table: "Calendar",
					column: "Date",
					unique: true);

			migrationBuilder.CreateIndex(
					name: "IX_Card_AccountId",
					schema: "Finance",
					table: "Card",
					column: "AccountId");

			migrationBuilder.CreateIndex(
					name: "IX_Card_PAN",
					schema: "Finance",
					table: "Card",
					column: "PAN",
					unique: true)
					.Annotation("SqlServer:Clustered", false);

			migrationBuilder.CreateIndex(
					name: "IX_Card_UserId",
					schema: "Finance",
					table: "Card",
					column: "UserId");

			migrationBuilder.CreateIndex(
					name: "IX_CardTransaction_TransactionId",
					schema: "Finance",
					table: "CardTransaction",
					column: "TransactionId");

			migrationBuilder.CreateIndex(
					name: "RoleNameIndex",
					schema: "Identity",
					table: "Role",
					column: "NormalizedName",
					unique: true,
					filter: "[NormalizedName] IS NOT NULL");

			migrationBuilder.CreateIndex(
					name: "IX_RoleClaim_RoleId",
					schema: "Identity",
					table: "RoleClaim",
					column: "RoleId");

			migrationBuilder.CreateIndex(
					name: "EmailIndex",
					schema: "Identity",
					table: "User",
					column: "NormalizedEmail");

			migrationBuilder.CreateIndex(
					name: "UserNameIndex",
					schema: "Identity",
					table: "User",
					column: "NormalizedUserName",
					unique: true,
					filter: "[NormalizedUserName] IS NOT NULL");

			migrationBuilder.CreateIndex(
					name: "IX_UserClaim_UserId",
					schema: "Identity",
					table: "UserClaim",
					column: "UserId");

			migrationBuilder.CreateIndex(
					name: "IX_UserLogin_UserId",
					schema: "Identity",
					table: "UserLogin",
					column: "UserId");

			migrationBuilder.CreateIndex(
					name: "IX_UserRole_RoleId",
					schema: "Identity",
					table: "UserRole",
					column: "RoleId");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
					name: "AccountTransaction",
					schema: "Finance")
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "AccountTransaction")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.DropTable(
					name: "AccountUser",
					schema: "Finance")
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "AccountUser")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.DropTable(
					name: "Attendance",
					schema: "Attendance")
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "Attendance")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.DropTable(
					name: "CardTransaction",
					schema: "Finance")
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "CardTransaction")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.DropTable(
					name: "RoleClaim",
					schema: "Identity")
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "RoleClaim")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.DropTable(
					name: "UserClaim",
					schema: "Identity")
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "UserClaim")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.DropTable(
					name: "UserLogin",
					schema: "Identity")
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "UserLogin")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.DropTable(
					name: "UserRole",
					schema: "Identity")
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "UserRole")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.DropTable(
					name: "UserToken",
					schema: "Identity")
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "UserToken")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.DropTable(
					name: "Calendar",
					schema: "Common")
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "Calendar")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.DropTable(
					name: "Card",
					schema: "Finance")
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "Card")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.DropTable(
					name: "Transaction",
					schema: "Finance")
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "Transaction")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.DropTable(
					name: "Role",
					schema: "Identity")
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "Role")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.DropTable(
					name: "Account",
					schema: "Finance")
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "Account")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.DropTable(
					name: "User",
					schema: "Identity")
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "User")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");
		}
	}
}
