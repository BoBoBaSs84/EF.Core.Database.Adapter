using System;

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Persistence.Migrations
{
	/// <inheritdoc />
	public partial class TransactionUpdate : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterColumn<string>(
					name: "Purpose",
					schema: "Finance",
					table: "Transaction",
					type: "nvarchar(4000)",
					maxLength: 4000,
					nullable: true,
					oldClrType: typeof(string),
					oldType: "nvarchar(4000)",
					oldMaxLength: 4000)
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "Transaction")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart")
					.OldAnnotation("SqlServer:IsTemporal", true)
					.OldAnnotation("SqlServer:TemporalHistoryTableName", "Transaction")
					.OldAnnotation("SqlServer:TemporalHistoryTableSchema", "History")
					.OldAnnotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.OldAnnotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.AlterColumn<string>(
					name: "MandateReference",
					schema: "Finance",
					table: "Transaction",
					type: "nvarchar(50)",
					maxLength: 50,
					nullable: true,
					oldClrType: typeof(string),
					oldType: "nvarchar(50)",
					oldMaxLength: 50)
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "Transaction")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart")
					.OldAnnotation("SqlServer:IsTemporal", true)
					.OldAnnotation("SqlServer:TemporalHistoryTableName", "Transaction")
					.OldAnnotation("SqlServer:TemporalHistoryTableSchema", "History")
					.OldAnnotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.OldAnnotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.AlterColumn<string>(
					name: "CustomerReference",
					schema: "Finance",
					table: "Transaction",
					type: "nvarchar(50)",
					maxLength: 50,
					nullable: true,
					oldClrType: typeof(string),
					oldType: "nvarchar(50)",
					oldMaxLength: 50)
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "Transaction")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart")
					.OldAnnotation("SqlServer:IsTemporal", true)
					.OldAnnotation("SqlServer:TemporalHistoryTableName", "Transaction")
					.OldAnnotation("SqlServer:TemporalHistoryTableSchema", "History")
					.OldAnnotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.OldAnnotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.AlterColumn<string>(
					name: "CreditorId",
					schema: "Finance",
					table: "Transaction",
					type: "nvarchar(25)",
					maxLength: 25,
					nullable: true,
					oldClrType: typeof(string),
					oldType: "nvarchar(25)",
					oldMaxLength: 25)
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "Transaction")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart")
					.OldAnnotation("SqlServer:IsTemporal", true)
					.OldAnnotation("SqlServer:TemporalHistoryTableName", "Transaction")
					.OldAnnotation("SqlServer:TemporalHistoryTableSchema", "History")
					.OldAnnotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.OldAnnotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterColumn<string>(
					name: "Purpose",
					schema: "Finance",
					table: "Transaction",
					type: "nvarchar(4000)",
					maxLength: 4000,
					nullable: false,
					defaultValue: "",
					oldClrType: typeof(string),
					oldType: "nvarchar(4000)",
					oldMaxLength: 4000,
					oldNullable: true)
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "Transaction")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart")
					.OldAnnotation("SqlServer:IsTemporal", true)
					.OldAnnotation("SqlServer:TemporalHistoryTableName", "Transaction")
					.OldAnnotation("SqlServer:TemporalHistoryTableSchema", "History")
					.OldAnnotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.OldAnnotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.AlterColumn<string>(
					name: "MandateReference",
					schema: "Finance",
					table: "Transaction",
					type: "nvarchar(50)",
					maxLength: 50,
					nullable: false,
					defaultValue: "",
					oldClrType: typeof(string),
					oldType: "nvarchar(50)",
					oldMaxLength: 50,
					oldNullable: true)
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "Transaction")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart")
					.OldAnnotation("SqlServer:IsTemporal", true)
					.OldAnnotation("SqlServer:TemporalHistoryTableName", "Transaction")
					.OldAnnotation("SqlServer:TemporalHistoryTableSchema", "History")
					.OldAnnotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.OldAnnotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.AlterColumn<string>(
					name: "CustomerReference",
					schema: "Finance",
					table: "Transaction",
					type: "nvarchar(50)",
					maxLength: 50,
					nullable: false,
					defaultValue: "",
					oldClrType: typeof(string),
					oldType: "nvarchar(50)",
					oldMaxLength: 50,
					oldNullable: true)
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "Transaction")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart")
					.OldAnnotation("SqlServer:IsTemporal", true)
					.OldAnnotation("SqlServer:TemporalHistoryTableName", "Transaction")
					.OldAnnotation("SqlServer:TemporalHistoryTableSchema", "History")
					.OldAnnotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.OldAnnotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");

			migrationBuilder.AlterColumn<string>(
					name: "CreditorId",
					schema: "Finance",
					table: "Transaction",
					type: "nvarchar(25)",
					maxLength: 25,
					nullable: false,
					defaultValue: "",
					oldClrType: typeof(string),
					oldType: "nvarchar(25)",
					oldMaxLength: 25,
					oldNullable: true)
					.Annotation("SqlServer:IsTemporal", true)
					.Annotation("SqlServer:TemporalHistoryTableName", "Transaction")
					.Annotation("SqlServer:TemporalHistoryTableSchema", "History")
					.Annotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.Annotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart")
					.OldAnnotation("SqlServer:IsTemporal", true)
					.OldAnnotation("SqlServer:TemporalHistoryTableName", "Transaction")
					.OldAnnotation("SqlServer:TemporalHistoryTableSchema", "History")
					.OldAnnotation("SqlServer:TemporalPeriodEndColumnName", "PeriodEnd")
					.OldAnnotation("SqlServer:TemporalPeriodStartColumnName", "PeriodStart");
		}
	}
}
