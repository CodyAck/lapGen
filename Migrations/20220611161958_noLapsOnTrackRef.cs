using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lapGen.Migrations
{
    public partial class noLapsOnTrackRef : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lap_Track_TrackID",
                table: "Lap");

            migrationBuilder.DropIndex(
                name: "IX_Lap_TrackID",
                table: "Lap");

            migrationBuilder.DropColumn(
                name: "TrackID",
                table: "Lap");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TrackID",
                table: "Lap",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Lap_TrackID",
                table: "Lap",
                column: "TrackID");

            migrationBuilder.AddForeignKey(
                name: "FK_Lap_Track_TrackID",
                table: "Lap",
                column: "TrackID",
                principalTable: "Track",
                principalColumn: "ID");
        }
    }
}
