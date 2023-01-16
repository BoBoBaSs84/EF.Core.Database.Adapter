using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Adapter.Infrastructure.Migrations.Authentication
{
    public partial class RoleUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "identity",
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[] { 1, "d8a9fa11-007d-4623-b0bf-f8f8673ac672", "This is the ultimate god role ... so to say.", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                schema: "identity",
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[] { 2, "a23469b1-1999-4d55-bc4c-c9f4aa3beeea", "This is a normal user with normal user rights.", "User", "USER" });

            migrationBuilder.InsertData(
                schema: "identity",
                table: "Role",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[] { 3, "4312a531-dee1-4465-9047-977af0521100", "The user with extended user rights.", "Super user", "SUPERUSER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "identity",
                table: "Role",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "identity",
                table: "Role",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "identity",
                table: "Role",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
