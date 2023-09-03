using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Persistence.Migrations.Development
{
    /// <inheritdoc />
    public partial class UpdateMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendance_Calendar_CalendarDayId",
                schema: "Attendance",
                table: "Attendance");

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("7bb400f3-9ccf-48bd-9d40-6ba78e71605d"));

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("833699d6-2a6e-4c6c-b7b6-1dca6fa3c991"));

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("a7114b38-d9a5-45b4-ac78-626ba38c5723"));

            migrationBuilder.RenameColumn(
                name: "CalendarDayId",
                schema: "Attendance",
                table: "Attendance",
                newName: "CalendarId")
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "Attendance")
                .Annotation("SqlServer:TemporalHistoryTableSchema", "History")
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

            migrationBuilder.RenameIndex(
                name: "IX_Attendance_UserId_CalendarDayId",
                schema: "Attendance",
                table: "Attendance",
                newName: "IX_Attendance_UserId_CalendarId");

            migrationBuilder.RenameIndex(
                name: "IX_Attendance_CalendarDayId",
                schema: "Attendance",
                table: "Attendance",
                newName: "IX_Attendance_CalendarId");

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("207fdc70-a5f0-454e-9312-5ae73991000a"), "2d7e9692-d588-485b-8104-0f8dba3a5d64", "This is the ultimate god role ... so to say.", "Administrator", "ADMINISTRATOR" },
                    { new Guid("760d142d-30b6-44e1-bd31-afb76ef20565"), "42380bb0-f5dc-4d0b-8285-0ea9915e6ad1", "This is a normal user with normal user rights.", "User", "USER" },
                    { new Guid("c368cd92-dca4-4104-aaaa-c9d926553b42"), "fe91b749-661a-4acf-8317-48a83db3226b", "The user with extended user rights.", "Super user", "SUPERUSER" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Attendance_Calendar_CalendarId",
                schema: "Attendance",
                table: "Attendance",
                column: "CalendarId",
                principalSchema: "Common",
                principalTable: "Calendar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendance_Calendar_CalendarId",
                schema: "Attendance",
                table: "Attendance");

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("207fdc70-a5f0-454e-9312-5ae73991000a"));

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("760d142d-30b6-44e1-bd31-afb76ef20565"));

            migrationBuilder.DeleteData(
                schema: "Identity",
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("c368cd92-dca4-4104-aaaa-c9d926553b42"));

            migrationBuilder.RenameColumn(
                name: "CalendarId",
                schema: "Attendance",
                table: "Attendance",
                newName: "CalendarDayId")
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "Attendance")
                .Annotation("SqlServer:TemporalHistoryTableSchema", "History")
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

            migrationBuilder.RenameIndex(
                name: "IX_Attendance_UserId_CalendarId",
                schema: "Attendance",
                table: "Attendance",
                newName: "IX_Attendance_UserId_CalendarDayId");

            migrationBuilder.RenameIndex(
                name: "IX_Attendance_CalendarId",
                schema: "Attendance",
                table: "Attendance",
                newName: "IX_Attendance_CalendarDayId");

            migrationBuilder.InsertData(
                schema: "Identity",
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("7bb400f3-9ccf-48bd-9d40-6ba78e71605d"), "23795774-3ff7-4715-acde-84165cceb0c5", "This is the ultimate god role ... so to say.", "Administrator", "ADMINISTRATOR" },
                    { new Guid("833699d6-2a6e-4c6c-b7b6-1dca6fa3c991"), "572fdc32-df11-4677-8cbf-b768fe84749d", "The user with extended user rights.", "Super user", "SUPERUSER" },
                    { new Guid("a7114b38-d9a5-45b4-ac78-626ba38c5723"), "f9099ec0-46fe-4898-990b-df4c337b0b76", "This is a normal user with normal user rights.", "User", "USER" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Attendance_Calendar_CalendarDayId",
                schema: "Attendance",
                table: "Attendance",
                column: "CalendarDayId",
                principalSchema: "Common",
                principalTable: "Calendar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
