using System;

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Persistence.Migrations
{
	/// <inheritdoc />
	public partial class DataRework : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
					name: "FK_Data_Document_DocumentId",
					schema: "documents",
					table: "Data");

			migrationBuilder.DropIndex(
					name: "IX_Document_MD5Hash",
					schema: "documents",
					table: "Document");

			migrationBuilder.DropIndex(
					name: "IX_Data_DocumentId",
					schema: "documents",
					table: "Data");

			migrationBuilder.DropColumn(
					name: "Length",
					schema: "documents",
					table: "Document")
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "Document")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.DropColumn(
					name: "MD5Hash",
					schema: "documents",
					table: "Document")
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "Document")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.DropColumn(
					name: "DocumentId",
					schema: "documents",
					table: "Data")
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "Data")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.RenameColumn(
					name: "RawData",
					schema: "documents",
					table: "Data",
					newName: "Content")
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "Data")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.AddColumn<string>(
					name: "MimeType",
					schema: "documents",
					table: "Extension",
					type: "nvarchar(256)",
					maxLength: 256,
					nullable: true)
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "Extension")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.AddColumn<long>(
					name: "Length",
					schema: "documents",
					table: "Data",
					type: "bigint",
					nullable: false,
					defaultValue: 0L)
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "Data")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.AddColumn<byte[]>(
					name: "MD5Hash",
					schema: "documents",
					table: "Data",
					type: "binary(16)",
					nullable: false,
					defaultValue: new byte[0])
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "Data")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.CreateTable(
					name: "DocumentData",
					schema: "documents",
					columns: table => new
					{
						Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()")
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "DocumentData")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "DocumentData")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						CreatedBy = table.Column<string>(type: "sysname", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "DocumentData")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						ModifiedBy = table.Column<string>(type: "sysname", nullable: true)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "DocumentData")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						DocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "DocumentData")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						DataId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "DocumentData")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						PeriodEnd = table.Column<DateTime>(type: "datetime2", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "DocumentData")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart"),
						PeriodStart = table.Column<DateTime>(type: "datetime2", nullable: false)
									.Annotation("SqlServer:IsTemporal", true)
									.Annotation("SqlServer:TemporalHistoryTableName", "DocumentData")
									.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
									.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
									.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart")
					},
					constraints: table =>
					{
						table.PrimaryKey("PK_DocumentData", x => x.Id)
											.Annotation("SqlServer:Clustered", false);
						table.ForeignKey(
											name: "FK_DocumentData_Data_DataId",
											column: x => x.DataId,
											principalSchema: "documents",
											principalTable: "Data",
											principalColumn: "Id",
											onDelete: ReferentialAction.Cascade);
						table.ForeignKey(
											name: "FK_DocumentData_Document_DocumentId",
											column: x => x.DocumentId,
											principalSchema: "documents",
											principalTable: "Document",
											principalColumn: "Id",
											onDelete: ReferentialAction.Cascade);
					})
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "DocumentData")
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
					name: "IX_DocumentData_DataId",
					schema: "documents",
					table: "DocumentData",
					column: "DataId");

			migrationBuilder.CreateIndex(
					name: "IX_DocumentData_DocumentId",
					schema: "documents",
					table: "DocumentData",
					column: "DocumentId");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
					name: "DocumentData",
					schema: "documents")
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "DocumentData")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.DropIndex(
					name: "IX_Data_MD5Hash",
					schema: "documents",
					table: "Data");

			migrationBuilder.DropColumn(
					name: "MimeType",
					schema: "documents",
					table: "Extension")
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "Extension")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.DropColumn(
					name: "Length",
					schema: "documents",
					table: "Data")
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "Data")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.DropColumn(
					name: "MD5Hash",
					schema: "documents",
					table: "Data")
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "Data")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.RenameColumn(
					name: "Content",
					schema: "documents",
					table: "Data",
					newName: "RawData")
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "Data")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.AddColumn<long>(
					name: "Length",
					schema: "documents",
					table: "Document",
					type: "bigint",
					nullable: false,
					defaultValue: 0L)
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "Document")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.AddColumn<byte[]>(
					name: "MD5Hash",
					schema: "documents",
					table: "Document",
					type: "binary(16)",
					nullable: false,
					defaultValue: new byte[0])
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "Document")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.AddColumn<Guid>(
					name: "DocumentId",
					schema: "documents",
					table: "Data",
					type: "uniqueidentifier",
					nullable: false,
					defaultValue: new Guid("00000000-0000-0000-0000-000000000000"))
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "Data")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "history")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.CreateIndex(
					name: "IX_Document_MD5Hash",
					schema: "documents",
					table: "Document",
					column: "MD5Hash",
					unique: true)
					.Annotation("SqlServer:Clustered", false);

			migrationBuilder.CreateIndex(
					name: "IX_Data_DocumentId",
					schema: "documents",
					table: "Data",
					column: "DocumentId",
					unique: true);

			migrationBuilder.AddForeignKey(
					name: "FK_Data_Document_DocumentId",
					schema: "documents",
					table: "Data",
					column: "DocumentId",
					principalSchema: "documents",
					principalTable: "Document",
					principalColumn: "Id",
					onDelete: ReferentialAction.Cascade);
		}
	}
}
