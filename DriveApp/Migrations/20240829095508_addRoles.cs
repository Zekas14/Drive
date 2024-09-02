using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DriveApp.Migrations
{
    /// <inheritdoc />
    public partial class addRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0e802c68-9095-4b56-bc2b-b418280666fa", null, "Traveller", null },
                    { "807fc415-add9-43fb-9b89-6b550d05bd31", null, "Admin", null },
                    { "85d6e7ca-add2-44d3-96cc-36914baf7893", null, "Driver", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0e802c68-9095-4b56-bc2b-b418280666fa");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "807fc415-add9-43fb-9b89-6b550d05bd31");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "85d6e7ca-add2-44d3-96cc-36914baf7893");

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
    }
}
