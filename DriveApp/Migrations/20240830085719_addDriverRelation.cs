using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DriveApp.Migrations
{
    /// <inheritdoc />
    public partial class addDriverRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DriverId",
                table: "TripDetails",
                type: "nvarchar(450)",
                nullable: true);

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
                    { "59b50e28-fcc3-44e0-abb0-52554026f793", null, "Admin", null },
                    { "7a071f5a-d021-419e-910c-20378e64118e", null, "Driver", null },
                    { "afe41448-11c3-44c4-92a9-6a6294d9347f", null, "Traveller", null }
                });
        }
    }
}
