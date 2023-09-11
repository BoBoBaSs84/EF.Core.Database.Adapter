using System;

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Persistence.Migrations.Development
{
	/// <inheritdoc />
	public partial class UpdateMigrationCalendarModel : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropIndex(
					name: "IX_Calendar_Month",
					schema: "Common",
					table: "Calendar");

			migrationBuilder.DropIndex(
					name: "IX_Calendar_Year",
					schema: "Common",
					table: "Calendar");

			migrationBuilder.DropColumn(
					name: "Day",
					schema: "Common",
					table: "Calendar")
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "Calendar")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.DropColumn(
					name: "DayOfYear",
					schema: "Common",
					table: "Calendar")
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "Calendar")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.DropColumn(
					name: "EndOfMonth",
					schema: "Common",
					table: "Calendar")
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "Calendar")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.DropColumn(
					name: "IsoWeek",
					schema: "Common",
					table: "Calendar")
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "Calendar")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.DropColumn(
					name: "Month",
					schema: "Common",
					table: "Calendar")
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "Calendar")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.DropColumn(
					name: "MonthName",
					schema: "Common",
					table: "Calendar")
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "Calendar")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.DropColumn(
					name: "Week",
					schema: "Common",
					table: "Calendar")
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "Calendar")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.DropColumn(
					name: "WeekDay",
					schema: "Common",
					table: "Calendar")
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "Calendar")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.DropColumn(
					name: "WeekDayName",
					schema: "Common",
					table: "Calendar")
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "Calendar")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.DropColumn(
					name: "Year",
					schema: "Common",
					table: "Calendar")
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "Calendar")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AddColumn<int>(
					name: "Day",
					schema: "Common",
					table: "Calendar",
					type: "int",
					nullable: false,
					computedColumnSql: "(datepart(day,[Date]))",
					stored: true)
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "Calendar")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.AddColumn<int>(
					name: "DayOfYear",
					schema: "Common",
					table: "Calendar",
					type: "int",
					nullable: false,
					computedColumnSql: "(datepart(dayofyear,[Date]))",
					stored: true)
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "Calendar")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.AddColumn<DateTime>(
					name: "EndOfMonth",
					schema: "Common",
					table: "Calendar",
					type: "datetime2",
					nullable: false,
					computedColumnSql: "(eomonth([Date]))",
					stored: true)
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "Calendar")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.AddColumn<int>(
					name: "IsoWeek",
					schema: "Common",
					table: "Calendar",
					type: "int",
					nullable: false,
					computedColumnSql: "(datepart(iso_week,[Date]))",
					stored: true)
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "Calendar")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.AddColumn<int>(
					name: "Month",
					schema: "Common",
					table: "Calendar",
					type: "int",
					nullable: false,
					computedColumnSql: "(datepart(month,[Date]))",
					stored: true)
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "Calendar")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.AddColumn<string>(
					name: "MonthName",
					schema: "Common",
					table: "Calendar",
					type: "nvarchar(max)",
					nullable: false,
					computedColumnSql: "(datename(month,[Date]))",
					stored: false)
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "Calendar")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.AddColumn<int>(
					name: "Week",
					schema: "Common",
					table: "Calendar",
					type: "int",
					nullable: false,
					computedColumnSql: "(datepart(week,[Date]))",
					stored: false)
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "Calendar")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.AddColumn<int>(
					name: "WeekDay",
					schema: "Common",
					table: "Calendar",
					type: "int",
					nullable: false,
					computedColumnSql: "(datepart(weekday,[Date]))",
					stored: false)
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "Calendar")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.AddColumn<string>(
					name: "WeekDayName",
					schema: "Common",
					table: "Calendar",
					type: "nvarchar(max)",
					nullable: false,
					computedColumnSql: "(datename(weekday,[Date]))",
					stored: false)
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "Calendar")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.AddColumn<int>(
					name: "Year",
					schema: "Common",
					table: "Calendar",
					type: "int",
					nullable: false,
					computedColumnSql: "(datepart(year,[Date]))",
					stored: true)
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "Calendar")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.CreateIndex(
					name: "IX_Calendar_Month",
					schema: "Common",
					table: "Calendar",
					column: "Month");

			migrationBuilder.CreateIndex(
					name: "IX_Calendar_Year",
					schema: "Common",
					table: "Calendar",
					column: "Year");
		}
	}
}
