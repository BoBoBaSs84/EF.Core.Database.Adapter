using System;

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Persistence.Migrations
{
	/// <inheritdoc />
	public partial class UniqueKeyUpdate : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateIndex(
					name: "IX_Extension_Name",
					schema: "documents",
					table: "Extension",
					column: "Name",
					unique: true)
					.Annotation("SqlServer:Clustered", false);
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropIndex(
					name: "IX_Extension_Name",
					schema: "documents",
					table: "Extension");
		}
	}
}
