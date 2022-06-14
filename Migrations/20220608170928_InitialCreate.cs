using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lapGen.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Car",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    number = table.Column<int>(type: "INTEGER", nullable: false),
                    name = table.Column<string>(type: "TEXT", nullable: true),
                    length = table.Column<string>(type: "TEXT", nullable: true),
                    height = table.Column<string>(type: "TEXT", nullable: true),
                    width = table.Column<string>(type: "TEXT", nullable: true),
                    weight = table.Column<string>(type: "TEXT", nullable: true),
                    wheelbase = table.Column<string>(type: "TEXT", nullable: true),
                    hp_maxMin = table.Column<int>(type: "INTEGER", nullable: false),
                    hp_maxMax = table.Column<int>(type: "INTEGER", nullable: false),
                    transmission = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Car", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Driver",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", nullable: true),
                    rank = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Driver", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Track",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Track", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Lap",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    recordedTrackID = table.Column<int>(type: "INTEGER", nullable: true),
                    recordedDriverID = table.Column<int>(type: "INTEGER", nullable: true),
                    raceCarID = table.Column<int>(type: "INTEGER", nullable: true),
                    recordedRaceStartTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    recordedLapTime = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lap", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Lap_Car_raceCarID",
                        column: x => x.raceCarID,
                        principalTable: "Car",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Lap_Driver_recordedDriverID",
                        column: x => x.recordedDriverID,
                        principalTable: "Driver",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Lap_Track_recordedTrackID",
                        column: x => x.recordedTrackID,
                        principalTable: "Track",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lap_raceCarID",
                table: "Lap",
                column: "raceCarID");

            migrationBuilder.CreateIndex(
                name: "IX_Lap_recordedDriverID",
                table: "Lap",
                column: "recordedDriverID");

            migrationBuilder.CreateIndex(
                name: "IX_Lap_recordedTrackID",
                table: "Lap",
                column: "recordedTrackID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Lap");

            migrationBuilder.DropTable(
                name: "Car");

            migrationBuilder.DropTable(
                name: "Driver");

            migrationBuilder.DropTable(
                name: "Track");
        }
    }
}
