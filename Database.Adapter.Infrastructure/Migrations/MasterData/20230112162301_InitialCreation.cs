using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Adapter.Infrastructure.Migrations.MasterData
{
    public partial class InitialCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "private");

            migrationBuilder.EnsureSchema(
                name: "enumerator");

            migrationBuilder.CreateTable(
                name: "DayType",
                schema: "enumerator",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    PeriodEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
                    PeriodStart = table.Column<DateTime>(type: "datetime2", nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DayType", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                })
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "DayType")
                .Annotation("SqlServer:TemporalHistoryTableSchema", "history")
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

            migrationBuilder.CreateTable(
                name: "CalendarDay",
                schema: "private",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false, computedColumnSql: "(datepart(year,[Date]))", stored: true),
                    Month = table.Column<int>(type: "int", nullable: false, computedColumnSql: "(datepart(month,[Date]))", stored: true),
                    Day = table.Column<int>(type: "int", nullable: false, computedColumnSql: "(datepart(day,[Date]))", stored: true),
                    Week = table.Column<int>(type: "int", nullable: false, computedColumnSql: "(datepart(week,[Date]))", stored: false),
                    IsoWeek = table.Column<int>(type: "int", nullable: false, computedColumnSql: "(datepart(iso_week,[Date]))", stored: true),
                    DayOfYear = table.Column<int>(type: "int", nullable: false, computedColumnSql: "(datepart(dayofyear,[Date]))", stored: true),
                    WeekDay = table.Column<int>(type: "int", nullable: false, computedColumnSql: "(datepart(weekday,[Date]))", stored: false),
                    EndOfMonth = table.Column<DateTime>(type: "datetime2", nullable: false, computedColumnSql: "(eomonth([Date]))", stored: true),
                    WeekDayName = table.Column<string>(type: "nvarchar(max)", nullable: false, computedColumnSql: "(datename(weekday,[Date]))", stored: false),
                    MonthName = table.Column<string>(type: "nvarchar(max)", nullable: false, computedColumnSql: "(datename(month,[Date]))", stored: false),
                    DayTypeId = table.Column<int>(type: "int", nullable: false),
                    PeriodEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
                    PeriodStart = table.Column<DateTime>(type: "datetime2", nullable: false)
                        .Annotation("SqlServer:IsTemporal", true)
                        .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                        .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalendarDay", x => x.Id)
                        .Annotation("SqlServer:Clustered", false);
                    table.ForeignKey(
                        name: "FK_CalendarDay_DayType_DayTypeId",
                        column: x => x.DayTypeId,
                        principalSchema: "enumerator",
                        principalTable: "DayType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "CalendarDay")
                .Annotation("SqlServer:TemporalHistoryTableSchema", "history")
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

            migrationBuilder.InsertData(
                schema: "enumerator",
                table: "DayType",
                columns: new[] { "Id", "Description", "IsActive", "Name" },
                values: new object[,]
                {
                    { 1, "A weekday day means any day except any Saturday, any Sunday, or any day which is a legal holiday.", true, "Weekday" },
                    { 2, "Generally refers to the period between the end of a usual work week and the beginning of the new work week.", true, "Weekend day" },
                    { 3, "Day on which professional work is performed or is to be performed.", true, "Workday" },
                    { 4, "Weekend work means working on days that are usually non-working days.", true, "Weekend workday" },
                    { 5, "Is an authorised prolonged absence from work, for any reason authorised by the workplace.", true, "Absence" },
                    { 6, "Business travel is travel undertaken for work or business purposes, as opposed to other types of travel, such as for leisure purposes.", true, "Buisness trip" },
                    { 7, "In the case of a suspension, the employee is permanently or temporarily released from his or her contractual work duties.", true, "Suspension" },
                    { 8, "A holiday is a day set aside by custom or by law on which normal activities, especially business or work including school, are suspended or reduced.", true, "Holiday" },
                    { 9, "The place of work is usually in the employee's own home, and in the case of mobile work also in third locations.", true, "Mobile working" },
                    { 10, "Is either the plan to leave of absence from a regular job or an instance of leisure travel away from home.", true, "Planned vacation" },
                    { 11, "Short-time work in the employment relationship means the temporary reduction of regular working hours in a company due to a significant loss of work.", true, "Short time work" },
                    { 12, "The employee can no longer perform his or her most recently performed work tasks due to illness or can only do so at the risk of aggravating the illness.", true, "Sickness" },
                    { 13, "Is either a leave of absence from a regular job or an instance of leisure travel away from home.", true, "Vacation" },
                    { 14, "With the vacation block, employers prohibit their employees from taking vacation during a certain period of time.", true, "Vacation block" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CalendarDay_Date",
                schema: "private",
                table: "CalendarDay",
                column: "Date",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CalendarDay_DayTypeId",
                schema: "private",
                table: "CalendarDay",
                column: "DayTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CalendarDay_Year",
                schema: "private",
                table: "CalendarDay",
                column: "Year");

            migrationBuilder.CreateIndex(
                name: "IX_DayType_Name",
                schema: "enumerator",
                table: "DayType",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CalendarDay",
                schema: "private")
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "CalendarDay")
                .Annotation("SqlServer:TemporalHistoryTableSchema", "history")
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

            migrationBuilder.DropTable(
                name: "DayType",
                schema: "enumerator")
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "DayType")
                .Annotation("SqlServer:TemporalHistoryTableSchema", "history")
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");
        }
    }
}
