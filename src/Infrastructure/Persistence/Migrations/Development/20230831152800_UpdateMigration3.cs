using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations.Development
{
    /// <inheritdoc />
    public partial class UpdateMigration3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountTransaction_Transaction_TransactionId",
                schema: "Finance",
                table: "AccountTransaction");

            migrationBuilder.DropForeignKey(
                name: "FK_CardTransaction_Transaction_TransactionId",
                schema: "Finance",
                table: "CardTransaction");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountTransaction_Transaction_TransactionId",
                schema: "Finance",
                table: "AccountTransaction",
                column: "TransactionId",
                principalSchema: "Finance",
                principalTable: "Transaction",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CardTransaction_Transaction_TransactionId",
                schema: "Finance",
                table: "CardTransaction",
                column: "TransactionId",
                principalSchema: "Finance",
                principalTable: "Transaction",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountTransaction_Transaction_TransactionId",
                schema: "Finance",
                table: "AccountTransaction");

            migrationBuilder.DropForeignKey(
                name: "FK_CardTransaction_Transaction_TransactionId",
                schema: "Finance",
                table: "CardTransaction");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountTransaction_Transaction_TransactionId",
                schema: "Finance",
                table: "AccountTransaction",
                column: "TransactionId",
                principalSchema: "Finance",
                principalTable: "Transaction",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CardTransaction_Transaction_TransactionId",
                schema: "Finance",
                table: "CardTransaction",
                column: "TransactionId",
                principalSchema: "Finance",
                principalTable: "Transaction",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
