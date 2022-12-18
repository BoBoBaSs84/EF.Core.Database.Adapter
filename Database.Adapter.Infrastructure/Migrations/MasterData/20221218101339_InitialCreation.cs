using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

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
                    DayTypeId = table.Column<int>(type: "int", nullable: false)
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
                    table.ForeignKey(
                        name: "FK_Calendar_EnDayType_DayTypeId",
                        column: x => x.DayTypeId,
                        principalSchema: "private",
                        principalTable: "EnDayType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "Calendar")
                .Annotation("SqlServer:TemporalHistoryTableSchema", "history")
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

            migrationBuilder.InsertData(
                schema: "private",
                table: "EnDayType",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 0, "Is an authorised prolonged absence from work, for any reason authorised by the workplace.", "Absence" },
                    { 1, "Business travel is travel undertaken for work or business purposes, as opposed to other types of travel, such as for leisure purposes.", "Buisness trip" },
                    { 2, "In the case of a suspension, the employee is permanently or temporarily released from his or her contractual work duties.", "Suspension" },
                    { 3, "A holiday is a day set aside by custom or by law on which normal activities, especially business or work including school, are suspended or reduced.", "Holiday" },
                    { 4, "The place of work is usually in the employee's own home, and in the case of mobile work also in third locations.", "Mobile working" },
                    { 5, "Is either the plan to leave of absence from a regular job or an instance of leisure travel away from home.", "Planned vacation" },
                    { 6, "A business day means any day except any Saturday, any Sunday, or any day which is a legal holiday.", "Business day" },
                    { 7, "Short-time work in the employment relationship means the temporary reduction of regular working hours in a company due to a significant loss of work.", "Short time work" },
                    { 8, "The employee can no longer perform his or her most recently performed work tasks due to illness or can only do so at the risk of aggravating the illness", "Sickness" },
                    { 9, "Is either a leave of absence from a regular job or an instance of leisure travel away from home.", "Vacation" },
                    { 10, "With the vacation block, employers prohibit their employees from taking vacation during a certain period of time.", "Vacation block" },
                    { 11, "Generally refers to the period between the end of a usual work week and the beginning of the new work week.", "Weekend" },
                    { 12, "Weekend work means working on days that are usually non-working days.", "Weekend workday" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Calendar_Date",
                schema: "private",
                table: "Calendar",
                column: "Date",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Calendar_DayTypeId",
                schema: "private",
                table: "Calendar",
                column: "DayTypeId");

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
