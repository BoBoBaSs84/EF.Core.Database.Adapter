using System;

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Persistence.Migrations
{
	/// <inheritdoc />
	public partial class DocumentsAdded : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.EnsureSchema(
					name: "documents");

			migrationBuilder.CreateTable(
					name: "Data",
					schema: "documents",
					columns: table => new
					{
						Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()")
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Data")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Data")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						CreatedBy = table.Column<string>(type: "sysname", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Data")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						ModifiedBy = table.Column<string>(type: "sysname", nullable: true)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Data")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						MD5Hash = table.Column<byte[]>(type: "binary(16)", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Data")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						Length = table.Column<long>(type: "bigint", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Data")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						Content = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Data")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						PeriodEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Data")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						PeriodStart = table.Column<DateTime>(type: "datetime2", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Data")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart")
					},
					constraints: table =>
					{
						table.PrimaryKey("PK_Data", x => x.Id)
											.Annotation("SqlServer:Clustered", false);
					})
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "Data")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.CreateTable(
					name: "Extension",
					schema: "documents",
					columns: table => new
					{
						Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()")
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Extension")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Extension")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						CreatedBy = table.Column<string>(type: "sysname", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Extension")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						ModifiedBy = table.Column<string>(type: "sysname", nullable: true)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Extension")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Extension")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						MimeType = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Extension")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						PeriodEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Extension")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						PeriodStart = table.Column<DateTime>(type: "datetime2", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Extension")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart")
					},
					constraints: table =>
					{
						table.PrimaryKey("PK_Extension", x => x.Id)
											.Annotation("SqlServer:Clustered", false);
					})
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "Extension")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.CreateTable(
					name: "Document",
					schema: "documents",
					columns: table => new
					{
						Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()")
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Document")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Document")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						CreatedBy = table.Column<string>(type: "sysname", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Document")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						ModifiedBy = table.Column<string>(type: "sysname", nullable: true)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Document")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Document")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						Directory = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Document")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						Flags = table.Column<long>(type: "bigint", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Document")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						CreationTime = table.Column<DateTime>(type: "smalldatetime", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Document")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						LastWriteTime = table.Column<DateTime>(type: "smalldatetime", nullable: true)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Document")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						LastAccessTime = table.Column<DateTime>(type: "smalldatetime", nullable: true)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Document")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						DataId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Document")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						ExtensionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Document")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						PeriodEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Document")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						PeriodStart = table.Column<DateTime>(type: "datetime2", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "Document")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart")
					},
					constraints: table =>
					{
						table.PrimaryKey("PK_Document", x => x.Id)
											.Annotation("SqlServer:Clustered", false);
						table.ForeignKey(
											name: "FK_Document_Data_DataId",
											column: x => x.DataId,
											principalSchema: "documents",
											principalTable: "Data",
											principalColumn: "Id",
											onDelete: ReferentialAction.Cascade);
						table.ForeignKey(
											name: "FK_Document_Extension_ExtensionId",
											column: x => x.ExtensionId,
											principalSchema: "documents",
											principalTable: "Extension",
											principalColumn: "Id",
											onDelete: ReferentialAction.Cascade);
					})
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "Document")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.CreateTable(
					name: "DocumentUser",
					schema: "documents",
					columns: table => new
					{
						Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "DocumentUser")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						CreatedBy = table.Column<string>(type: "sysname", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "DocumentUser")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						ModifiedBy = table.Column<string>(type: "sysname", nullable: true)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "DocumentUser")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						DocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "DocumentUser")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "DocumentUser")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						PeriodEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "DocumentUser")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						PeriodStart = table.Column<DateTime>(type: "datetime2", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "DocumentUser")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart")
					},
					constraints: table =>
					{
						table.PrimaryKey("PK_DocumentUser", x => new { x.DocumentId, x.UserId })
											.Annotation("SqlServer:Clustered", false);
						table.ForeignKey(
											name: "FK_DocumentUser_Document_DocumentId",
											column: x => x.DocumentId,
											principalSchema: "documents",
											principalTable: "Document",
											principalColumn: "Id",
											onDelete: ReferentialAction.Cascade);
						table.ForeignKey(
											name: "FK_DocumentUser_User_UserId",
											column: x => x.UserId,
											principalSchema: "identity",
											principalTable: "User",
											principalColumn: "Id",
											onDelete: ReferentialAction.Cascade);
					})
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "DocumentUser")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.CreateIndex(
					name: "IX_Data_MD5Hash",
					schema: "documents",
					table: "Data",
					column: "MD5Hash",
					unique: true)
					.Annotation("SqlServer:Clustered", false);

			migrationBuilder.CreateIndex(
					name: "IX_Document_DataId",
					schema: "documents",
					table: "Document",
					column: "DataId");

			migrationBuilder.CreateIndex(
					name: "IX_Document_ExtensionId",
					schema: "documents",
					table: "Document",
					column: "ExtensionId");

			migrationBuilder.CreateIndex(
					name: "IX_DocumentUser_UserId",
					schema: "documents",
					table: "DocumentUser",
					column: "UserId");

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
			migrationBuilder.DropTable(
					name: "DocumentUser",
					schema: "documents")
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "DocumentUser")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.DropTable(
					name: "Document",
					schema: "documents")
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "Document")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.DropTable(
					name: "Data",
					schema: "documents")
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "Data")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.DropTable(
					name: "Extension",
					schema: "documents")
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "Extension")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");
		}
	}
}
