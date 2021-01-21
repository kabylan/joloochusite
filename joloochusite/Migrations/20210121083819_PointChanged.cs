using Microsoft.EntityFrameworkCore.Migrations;

namespace joloochusite.Migrations
{
    public partial class PointChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Points_Districts_DistrictId",
                table: "Points");

            migrationBuilder.DropForeignKey(
                name: "FK_Points_Villages_VillageId",
                table: "Points");

            migrationBuilder.DropIndex(
                name: "IX_Points_DistrictId",
                table: "Points");

            migrationBuilder.DropIndex(
                name: "IX_Points_VillageId",
                table: "Points");

            migrationBuilder.DropColumn(
                name: "DistrictId",
                table: "Points");

            migrationBuilder.DropColumn(
                name: "VillageId",
                table: "Points");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Points",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Points");

            migrationBuilder.AddColumn<int>(
                name: "DistrictId",
                table: "Points",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VillageId",
                table: "Points",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Points_DistrictId",
                table: "Points",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_Points_VillageId",
                table: "Points",
                column: "VillageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Points_Districts_DistrictId",
                table: "Points",
                column: "DistrictId",
                principalTable: "Districts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Points_Villages_VillageId",
                table: "Points",
                column: "VillageId",
                principalTable: "Villages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
