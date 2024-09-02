using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DriveApp.Migrations
{
    /// <inheritdoc />
    public partial class AddPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2db6de58-e76c-4fc9-8787-53f1712f19a0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9dea55f9-7e9a-4a65-9ac8-4ca4a67f6d0f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d11720c0-3e3d-4d83-a88c-10017415e509");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Trips",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "435994a8-aa68-46aa-a8a9-361130047d9d", null, "Admin", null },
                    { "4773dd37-8aff-4ba2-b57f-3f3f0f6081bf", null, "Driver", null },
                    { "93dacee6-b3b3-4430-ab98-2e2c3782fe89", null, "User", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "435994a8-aa68-46aa-a8a9-361130047d9d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4773dd37-8aff-4ba2-b57f-3f3f0f6081bf");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "93dacee6-b3b3-4430-ab98-2e2c3782fe89");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Trips");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2db6de58-e76c-4fc9-8787-53f1712f19a0", null, "User", null },
                    { "9dea55f9-7e9a-4a65-9ac8-4ca4a67f6d0f", null, "Driver", null },
                    { "d11720c0-3e3d-4d83-a88c-10017415e509", null, "Admin", null }
                });
        }
    }
}
