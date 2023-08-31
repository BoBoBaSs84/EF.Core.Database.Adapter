using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations.Development
{
    /// <inheritdoc />
    public partial class UpdateMigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountTransaction_Account_AccountId",
                schema: "Finance",
                table: "AccountTransaction");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountUser_Account_AccountId",
                schema: "Finance",
                table: "AccountUser");

            migrationBuilder.DropForeignKey(
                name: "FK_Card_Account_AccountId",
                schema: "Finance",
                table: "Card");

            migrationBuilder.DropForeignKey(
                name: "FK_CardTransaction_Card_CardId",
                schema: "Finance",
                table: "CardTransaction");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountTransaction_Account_AccountId",
                schema: "Finance",
                table: "AccountTransaction",
                column: "AccountId",
                principalSchema: "Finance",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountUser_Account_AccountId",
                schema: "Finance",
                table: "AccountUser",
                column: "AccountId",
                principalSchema: "Finance",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Card_Account_AccountId",
                schema: "Finance",
                table: "Card",
                column: "AccountId",
                principalSchema: "Finance",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CardTransaction_Card_CardId",
                schema: "Finance",
                table: "CardTransaction",
                column: "CardId",
                principalSchema: "Finance",
                principalTable: "Card",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountTransaction_Account_AccountId",
                schema: "Finance",
                table: "AccountTransaction");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountUser_Account_AccountId",
                schema: "Finance",
                table: "AccountUser");

            migrationBuilder.DropForeignKey(
                name: "FK_Card_Account_AccountId",
                schema: "Finance",
                table: "Card");

            migrationBuilder.DropForeignKey(
                name: "FK_CardTransaction_Card_CardId",
                schema: "Finance",
                table: "CardTransaction");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountTransaction_Account_AccountId",
                schema: "Finance",
                table: "AccountTransaction",
                column: "AccountId",
                principalSchema: "Finance",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountUser_Account_AccountId",
                schema: "Finance",
                table: "AccountUser",
                column: "AccountId",
                principalSchema: "Finance",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Card_Account_AccountId",
                schema: "Finance",
                table: "Card",
                column: "AccountId",
                principalSchema: "Finance",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CardTransaction_Card_CardId",
                schema: "Finance",
                table: "CardTransaction",
                column: "CardId",
                principalSchema: "Finance",
                principalTable: "Card",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
