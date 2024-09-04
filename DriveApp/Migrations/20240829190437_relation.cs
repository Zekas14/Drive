using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DriveApp.Migrations
{
    /// <inheritdoc />
    public partial class Relation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "21220ca1-4568-491b-a04a-9e090fa83657");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7a9bc4d1-5e84-4434-b5e9-46f95c5fb541");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9d15de73-ba90-4785-b597-61ec6e8a5fc9");

            migrationBuilder.AddColumn<string>(
                name: "DirverId",
                table: "TripDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DriverId",
                table: "TripDetails",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "26fe0681-3a5c-46d4-b695-15fcad7ef5b9", null, "Traveller", null },
                    { "8eac2b0c-9bb6-44bc-8773-23b5b0aff511", null, "Driver", null },
                    { "cfb1daae-1b00-4ce4-9bc1-ea61b183e0e4", null, "Admin", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TripDetails_DriverId",
                table: "TripDetails",
                column: "DriverId");

            migrationBuilder.AddForeignKey(
                name: "FK_TripDetails_Drivers_DriverId",
                table: "TripDetails",
                column: "DriverId",
                principalTable: "Drivers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TripDetails_Drivers_DriverId",
                table: "TripDetails");

            migrationBuilder.DropIndex(
                name: "IX_TripDetails_DriverId",
                table: "TripDetails");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "26fe0681-3a5c-46d4-b695-15fcad7ef5b9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8eac2b0c-9bb6-44bc-8773-23b5b0aff511");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cfb1daae-1b00-4ce4-9bc1-ea61b183e0e4");

            migrationBuilder.DropColumn(
                name: "DirverId",
                table: "TripDetails");

            migrationBuilder.DropColumn(
                name: "DriverId",
                table: "TripDetails");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "21220ca1-4568-491b-a04a-9e090fa83657", null, "Admin", null },
                    { "7a9bc4d1-5e84-4434-b5e9-46f95c5fb541", null, "Traveller", null },
                    { "9d15de73-ba90-4785-b597-61ec6e8a5fc9", null, "Driver", null }
                });
        }
    }
}
