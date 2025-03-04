using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class ImagesListMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VehicleId1",
                table: "Images",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_VehicleId1",
                table: "Images",
                column: "VehicleId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Vehicles_VehicleId1",
                table: "Images",
                column: "VehicleId1",
                principalTable: "Vehicles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Vehicles_VehicleId1",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_VehicleId1",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "VehicleId1",
                table: "Images");
        }
    }
}
