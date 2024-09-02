using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DriveApp.Migrations
{
    /// <inheritdoc />
    public partial class UserTripRelationship2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trips_AspNetUsers_UserId1",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "IX_Trips_UserId1",
                table: "Trips");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0d5479a3-5062-463a-ad78-bcdb20acb801");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d1fe2b12-ba92-4c1e-9f5a-4d9663eae83f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fd3cb522-d5ed-4d81-8312-b299b62e96c0");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Trips");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Trips",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2db6de58-e76c-4fc9-8787-53f1712f19a0", null, "User", null },
                    { "9dea55f9-7e9a-4a65-9ac8-4ca4a67f6d0f", null, "Driver", null },
                    { "d11720c0-3e3d-4d83-a88c-10017415e509", null, "Admin", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Trips_UserId",
                table: "Trips",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_AspNetUsers_UserId",
                table: "Trips",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trips_AspNetUsers_UserId",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "IX_Trips_UserId",
                table: "Trips");

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

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Trips",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Trips",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0d5479a3-5062-463a-ad78-bcdb20acb801", null, "Admin", null },
                    { "d1fe2b12-ba92-4c1e-9f5a-4d9663eae83f", null, "User", null },
                    { "fd3cb522-d5ed-4d81-8312-b299b62e96c0", null, "Driver", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Trips_UserId1",
                table: "Trips",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_AspNetUsers_UserId1",
                table: "Trips",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
