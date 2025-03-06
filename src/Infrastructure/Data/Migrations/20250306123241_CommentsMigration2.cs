using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class CommentsMigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Users_ClientId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Vehicles_VehicleId",
                table: "Reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews");

            migrationBuilder.RenameTable(
                name: "Reviews",
                newName: "Comments");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_VehicleId",
                table: "Comments",
                newName: "IX_Comments_VehicleId");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_ClientId",
                table: "Comments",
                newName: "IX_Comments_ClientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comments",
                table: "Comments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Users_ClientId",
                table: "Comments",
                column: "ClientId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Vehicles_VehicleId",
                table: "Comments",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Users_ClientId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Vehicles_VehicleId",
                table: "Comments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comments",
                table: "Comments");

            migrationBuilder.RenameTable(
                name: "Comments",
                newName: "Reviews");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_VehicleId",
                table: "Reviews",
                newName: "IX_Reviews_VehicleId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_ClientId",
                table: "Reviews",
                newName: "IX_Reviews_ClientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Users_ClientId",
                table: "Reviews",
                column: "ClientId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Vehicles_VehicleId",
                table: "Reviews",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
