using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddVehicleFeaturesRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleFeatures_Features_FeaturesId",
                table: "VehicleFeatures");

            migrationBuilder.DropForeignKey(
                name: "FK_VehicleFeatures_Vehicles_VehiclesId",
                table: "VehicleFeatures");

            migrationBuilder.DropForeignKey(
                name: "FK_VehicleView_Vehicles_VehicleId",
                table: "VehicleView");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VehicleView",
                table: "VehicleView");

            migrationBuilder.RenameTable(
                name: "VehicleView",
                newName: "VehicleViews");

            migrationBuilder.RenameColumn(
                name: "VehiclesId",
                table: "VehicleFeatures",
                newName: "FeatureId");

            migrationBuilder.RenameColumn(
                name: "FeaturesId",
                table: "VehicleFeatures",
                newName: "VehicleId");

            migrationBuilder.RenameIndex(
                name: "IX_VehicleFeatures_VehiclesId",
                table: "VehicleFeatures",
                newName: "IX_VehicleFeatures_FeatureId");

            migrationBuilder.RenameColumn(
                name: "IdSpecifications",
                table: "Specifications",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_VehicleView_VehicleId",
                table: "VehicleViews",
                newName: "IX_VehicleViews_VehicleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VehicleViews",
                table: "VehicleViews",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleFeatures_Features_FeatureId",
                table: "VehicleFeatures",
                column: "FeatureId",
                principalTable: "Features",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleFeatures_Vehicles_VehicleId",
                table: "VehicleFeatures",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleViews_Vehicles_VehicleId",
                table: "VehicleViews",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleFeatures_Features_FeatureId",
                table: "VehicleFeatures");

            migrationBuilder.DropForeignKey(
                name: "FK_VehicleFeatures_Vehicles_VehicleId",
                table: "VehicleFeatures");

            migrationBuilder.DropForeignKey(
                name: "FK_VehicleViews_Vehicles_VehicleId",
                table: "VehicleViews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VehicleViews",
                table: "VehicleViews");

            migrationBuilder.RenameTable(
                name: "VehicleViews",
                newName: "VehicleView");

            migrationBuilder.RenameColumn(
                name: "FeatureId",
                table: "VehicleFeatures",
                newName: "VehiclesId");

            migrationBuilder.RenameColumn(
                name: "VehicleId",
                table: "VehicleFeatures",
                newName: "FeaturesId");

            migrationBuilder.RenameIndex(
                name: "IX_VehicleFeatures_FeatureId",
                table: "VehicleFeatures",
                newName: "IX_VehicleFeatures_VehiclesId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Specifications",
                newName: "IdSpecifications");

            migrationBuilder.RenameIndex(
                name: "IX_VehicleViews_VehicleId",
                table: "VehicleView",
                newName: "IX_VehicleView_VehicleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VehicleView",
                table: "VehicleView",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleFeatures_Features_FeaturesId",
                table: "VehicleFeatures",
                column: "FeaturesId",
                principalTable: "Features",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleFeatures_Vehicles_VehiclesId",
                table: "VehicleFeatures",
                column: "VehiclesId",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleView_Vehicles_VehicleId",
                table: "VehicleView",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
