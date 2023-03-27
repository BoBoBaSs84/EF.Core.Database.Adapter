using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations.Development
{
    /// <inheritdoc />
    public partial class UpdateMigration_20230327 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountUser_User_UserId",
                schema: "Finance",
                table: "AccountUser");

            migrationBuilder.DropForeignKey(
                name: "FK_Attendance_User_UserId",
                schema: "Private",
                table: "Attendance");

            migrationBuilder.DropForeignKey(
                name: "FK_Card_User_UserId",
                schema: "Finance",
                table: "Card");

            migrationBuilder.DropForeignKey(
                name: "FK_UserClaim_User_UserId",
                schema: "Identity",
                table: "UserClaim");

            migrationBuilder.DropForeignKey(
                name: "FK_UserLogin_User_UserId",
                schema: "Identity",
                table: "UserLogin");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_User_UserId",
                schema: "Identity",
                table: "UserRole");

            migrationBuilder.DropForeignKey(
                name: "FK_UserToken_User_UserId",
                schema: "Identity",
                table: "UserToken");

            migrationBuilder.DropIndex(
                name: "IX_Attendance_UserId_CalendarDayId_IsDeleted",
                schema: "Private",
                table: "Attendance");

            migrationBuilder.DropIndex(
                name: "IX_Account_IBAN_IsDeleted",
                schema: "Finance",
                table: "Account");

            migrationBuilder.CreateIndex(
                name: "IX_Attendance_UserId_CalendarDayId",
                schema: "Private",
                table: "Attendance",
                columns: new[] { "UserId", "CalendarDayId" },
                unique: true,
                filter: "[IsDeleted]<>(1)")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_Account_IBAN",
                schema: "Finance",
                table: "Account",
                column: "IBAN",
                unique: true,
                filter: "[IsDeleted]<>(1)")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountUser_User_UserId",
                schema: "Finance",
                table: "AccountUser",
                column: "UserId",
                principalSchema: "Identity",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Attendance_User_UserId",
                schema: "Private",
                table: "Attendance",
                column: "UserId",
                principalSchema: "Identity",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Card_User_UserId",
                schema: "Finance",
                table: "Card",
                column: "UserId",
                principalSchema: "Identity",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserClaim_User_UserId",
                schema: "Identity",
                table: "UserClaim",
                column: "UserId",
                principalSchema: "Identity",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserLogin_User_UserId",
                schema: "Identity",
                table: "UserLogin",
                column: "UserId",
                principalSchema: "Identity",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_User_UserId",
                schema: "Identity",
                table: "UserRole",
                column: "UserId",
                principalSchema: "Identity",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserToken_User_UserId",
                schema: "Identity",
                table: "UserToken",
                column: "UserId",
                principalSchema: "Identity",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountUser_User_UserId",
                schema: "Finance",
                table: "AccountUser");

            migrationBuilder.DropForeignKey(
                name: "FK_Attendance_User_UserId",
                schema: "Private",
                table: "Attendance");

            migrationBuilder.DropForeignKey(
                name: "FK_Card_User_UserId",
                schema: "Finance",
                table: "Card");

            migrationBuilder.DropForeignKey(
                name: "FK_UserClaim_User_UserId",
                schema: "Identity",
                table: "UserClaim");

            migrationBuilder.DropForeignKey(
                name: "FK_UserLogin_User_UserId",
                schema: "Identity",
                table: "UserLogin");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_User_UserId",
                schema: "Identity",
                table: "UserRole");

            migrationBuilder.DropForeignKey(
                name: "FK_UserToken_User_UserId",
                schema: "Identity",
                table: "UserToken");

            migrationBuilder.DropIndex(
                name: "IX_Attendance_UserId_CalendarDayId",
                schema: "Private",
                table: "Attendance");

            migrationBuilder.DropIndex(
                name: "IX_Account_IBAN",
                schema: "Finance",
                table: "Account");

            migrationBuilder.CreateIndex(
                name: "IX_Attendance_UserId_CalendarDayId_IsDeleted",
                schema: "Private",
                table: "Attendance",
                columns: new[] { "UserId", "CalendarDayId", "IsDeleted" },
                unique: true,
                filter: "[IsDeleted]<>(1)")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.CreateIndex(
                name: "IX_Account_IBAN_IsDeleted",
                schema: "Finance",
                table: "Account",
                columns: new[] { "IBAN", "IsDeleted" },
                unique: true,
                filter: "[IsDeleted]<>(1)")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountUser_User_UserId",
                schema: "Finance",
                table: "AccountUser",
                column: "UserId",
                principalSchema: "Identity",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Attendance_User_UserId",
                schema: "Private",
                table: "Attendance",
                column: "UserId",
                principalSchema: "Identity",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Card_User_UserId",
                schema: "Finance",
                table: "Card",
                column: "UserId",
                principalSchema: "Identity",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserClaim_User_UserId",
                schema: "Identity",
                table: "UserClaim",
                column: "UserId",
                principalSchema: "Identity",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserLogin_User_UserId",
                schema: "Identity",
                table: "UserLogin",
                column: "UserId",
                principalSchema: "Identity",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_User_UserId",
                schema: "Identity",
                table: "UserRole",
                column: "UserId",
                principalSchema: "Identity",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserToken_User_UserId",
                schema: "Identity",
                table: "UserToken",
                column: "UserId",
                principalSchema: "Identity",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
