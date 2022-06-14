using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lapGen.Migrations
{
    public partial class InitialCreate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lap_Race_raceID",
                table: "Lap");

            migrationBuilder.DropTable(
                name: "Race");

            migrationBuilder.DropTable(
                name: "Track");

            migrationBuilder.DropIndex(
                name: "IX_Lap_raceID",
                table: "Lap");

            migrationBuilder.DropColumn(
                name: "raceID",
                table: "Lap");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "raceID",
                table: "Lap",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Track",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    active = table.Column<bool>(type: "INTEGER", nullable: false),
                    name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Track", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Race",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    active = table.Column<bool>(type: "INTEGER", nullable: false),
                    name = table.Column<string>(type: "TEXT", nullable: true),
                    recordedRaceStartTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    trackID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Race", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Race_Track_trackID",
                        column: x => x.trackID,
                        principalTable: "Track",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lap_raceID",
                table: "Lap",
                column: "raceID");

            migrationBuilder.CreateIndex(
                name: "IX_Race_trackID",
                table: "Race",
                column: "trackID");

            migrationBuilder.AddForeignKey(
                name: "FK_Lap_Race_raceID",
                table: "Lap",
                column: "raceID",
                principalTable: "Race",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
