using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Adapter.Infrastructure.Migrations.MasterData
{
    /// <inheritdoc />
    public partial class InitialCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "private");

            migrationBuilder.CreateTable(
                name: "Calendar",
                schema: "private",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "Calendar")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", "history")
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
                    Date = table.Column<DateTime>(type: "date", nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "Calendar")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", "history")
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
                    Year = table.Column<int>(type: "int", nullable: false, computedColumnSql: "(datepart(year,[Date]))", stored: true)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "Calendar")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", "history")
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
                    Month = table.Column<int>(type: "int", nullable: false, computedColumnSql: "(datepart(month,[Date]))", stored: true)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "Calendar")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", "history")
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
                    Day = table.Column<int>(type: "int", nullable: false, computedColumnSql: "(datepart(day,[Date]))", stored: true)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "Calendar")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", "history")
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
                    Week = table.Column<int>(type: "int", nullable: false, computedColumnSql: "(datepart(week,[Date]))", stored: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "Calendar")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", "history")
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
                    IsoWeek = table.Column<int>(type: "int", nullable: false, computedColumnSql: "(datepart(iso_week,[Date]))", stored: true)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "Calendar")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", "history")
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
                    DayOfYear = table.Column<int>(type: "int", nullable: false, computedColumnSql: "(datepart(dayofyear,[Date]))", stored: true)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "Calendar")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", "history")
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
                    WeekDay = table.Column<int>(type: "int", nullable: false, computedColumnSql: "(datepart(weekday,[Date]))", stored: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "Calendar")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", "history")
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
                    EndOfMonth = table.Column<DateTime>(type: "datetime2", nullable: false, computedColumnSql: "(eomonth([Date]))", stored: true)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "Calendar")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", "history")
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
                    WeekDayName = table.Column<string>(type: "nvarchar(max)", nullable: false, computedColumnSql: "(datename(weekday,[Date]))", stored: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "Calendar")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", "history")
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
                    MonthName = table.Column<string>(type: "nvarchar(max)", nullable: false, computedColumnSql: "(datename(month,[Date]))", stored: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "Calendar")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", "history")
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
                    PeriodEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "Calendar")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", "history")
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
                    PeriodStart = table.Column<DateTime>(type: "datetime2", nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "Calendar")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", "history")
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "Calendar")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", "history")
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calendar", x => x.Id);
                })
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "Calendar")
                .Annotation("SqlServer:TemporalHistoryTableSchema", "history")
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

            migrationBuilder.CreateTable(
                name: "EnDayType",
                schema: "private",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "EnDayType")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", "history")
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
                    PeriodEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "EnDayType")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", "history")
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
                    PeriodStart = table.Column<DateTime>(type: "datetime2", nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "EnDayType")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", "history")
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "EnDayType")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", "history")
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
                    Description = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "EnDayType")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", "history")
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalHistoryTableName", "EnDayType")
                        .Annotation("SqlServer:TemporalHistoryTableSchema", "history")
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnDayType", x => x.Id);
                })
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "EnDayType")
                .Annotation("SqlServer:TemporalHistoryTableSchema", "history")
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

            migrationBuilder.CreateIndex(
                name: "IX_Calendar_Date",
                schema: "private",
                table: "Calendar",
                column: "Date",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EnDayType_Name",
                schema: "private",
                table: "EnDayType",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Calendar",
                schema: "private")
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "Calendar")
                .Annotation("SqlServer:TemporalHistoryTableSchema", "history")
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

            migrationBuilder.DropTable(
                name: "EnDayType",
                schema: "private")
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "EnDayType")
                .Annotation("SqlServer:TemporalHistoryTableSchema", "history")
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");
        }
    }
}
