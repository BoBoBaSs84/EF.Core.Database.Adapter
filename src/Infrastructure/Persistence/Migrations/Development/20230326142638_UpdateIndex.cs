using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations.Development
{
    public partial class UpdateIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Attendance_UserId_CalendarDayId",
                schema: "Private",
                table: "Attendance");

            migrationBuilder.CreateIndex(
                name: "IX_Attendance_UserId_CalendarDayId_IsDeleted",
                schema: "Private",
                table: "Attendance",
                columns: new[] { "UserId", "CalendarDayId", "IsDeleted" },
                unique: true,
                filter: "[IsDeleted] != 0");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Attendance_UserId_CalendarDayId_IsDeleted",
                schema: "Private",
                table: "Attendance");

            migrationBuilder.CreateIndex(
                name: "IX_Attendance_UserId_CalendarDayId",
                schema: "Private",
                table: "Attendance",
                columns: new[] { "UserId", "CalendarDayId" },
                unique: true);
        }
    }
}
