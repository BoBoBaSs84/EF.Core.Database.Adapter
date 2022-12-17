using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Database.Adapter.Infrastructure.Migrations.MasterData
{
    /// <inheritdoc />
    public partial class EnDayTypeUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "private",
                table: "EnDayType",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 0, "Is an authorised prolonged absence from work, for any reason authorised by the workplace.", "Absence" },
                    { 1, "Business travel is travel undertaken for work or business purposes, as opposed to other types of travel, such as for leisure purposes.", "Buisness trip" },
                    { 2, "In the case of a suspension, the employee is permanently or temporarily released from his or her contractual work duties.", "Suspension" },
                    { 3, "A holiday is a day set aside by custom or by law on which normal activities, especially business or work including school, are suspended or reduced.", "Holiday" },
                    { 4, "The place of work is usually in the employee's own home, and in the case of mobile work also in third locations.", "Mobile working" },
                    { 5, "Is either the plan to leave of absence from a regular job or an instance of leisure travel away from home.", "Planned vacation" },
                    { 6, "A business day means any day except any Saturday, any Sunday, or any day which is a legal holiday.", "Business day" },
                    { 7, "Short-time work in the employment relationship means the temporary reduction of regular working hours in a company due to a significant loss of work.", "Short time work" },
                    { 8, "The employee can no longer perform his or her most recently performed work tasks due to illness or can only do so at the risk of aggravating the illness", "Sickness" },
                    { 9, "Is either a leave of absence from a regular job or an instance of leisure travel away from home.", "Vacation" },
                    { 10, "With the vacation block, employers prohibit their employees from taking vacation during a certain period of time.", "Vacation block" },
                    { 11, "Generally refers to the period between the end of a usual work week and the beginning of the new work week.", "Weekend" },
                    { 12, "Weekend work means working on days that are usually non-working days.", "Weekend workday" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "private",
                table: "EnDayType",
                keyColumn: "Id",
                keyValue: 0);

            migrationBuilder.DeleteData(
                schema: "private",
                table: "EnDayType",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "private",
                table: "EnDayType",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "private",
                table: "EnDayType",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "private",
                table: "EnDayType",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                schema: "private",
                table: "EnDayType",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                schema: "private",
                table: "EnDayType",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                schema: "private",
                table: "EnDayType",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                schema: "private",
                table: "EnDayType",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                schema: "private",
                table: "EnDayType",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                schema: "private",
                table: "EnDayType",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                schema: "private",
                table: "EnDayType",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                schema: "private",
                table: "EnDayType",
                keyColumn: "Id",
                keyValue: 12);
        }
    }
}
