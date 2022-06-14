using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lapGen.Migrations
{
    public partial class restructureTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Car_Driver_driverID",
                table: "Car");

            migrationBuilder.DropForeignKey(
                name: "FK_Lap_Car_raceCarID",
                table: "Lap");

            migrationBuilder.DropForeignKey(
                name: "FK_Lap_Track_recordedTrackID",
                table: "Lap");

            migrationBuilder.DropIndex(
                name: "IX_Lap_raceCarID",
                table: "Lap");

            migrationBuilder.DropIndex(
                name: "IX_Car_driverID",
                table: "Car");

            migrationBuilder.DropColumn(
                name: "raceCarID",
                table: "Lap");

            migrationBuilder.DropColumn(
                name: "recordedRaceStartTime",
                table: "Lap");

            migrationBuilder.DropColumn(
                name: "driverID",
                table: "Car");

            migrationBuilder.RenameColumn(
                name: "recordedTrackID",
                table: "Lap",
                newName: "TrackID");

            migrationBuilder.RenameIndex(
                name: "IX_Lap_recordedTrackID",
                table: "Lap",
                newName: "IX_Lap_TrackID");

            migrationBuilder.AlterColumn<string>(
                name: "recordedLapTime",
                table: "Lap",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TEXT");

            migrationBuilder.AddColumn<int>(
                name: "carID",
                table: "Lap",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "raceID",
                table: "Lap",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CarID",
                table: "Driver",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Race",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
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
                name: "IX_Lap_carID",
                table: "Lap",
                column: "carID");

            migrationBuilder.CreateIndex(
                name: "IX_Lap_raceID",
                table: "Lap",
                column: "raceID");

            migrationBuilder.CreateIndex(
                name: "IX_Driver_CarID",
                table: "Driver",
                column: "CarID");

            migrationBuilder.CreateIndex(
                name: "IX_Race_trackID",
                table: "Race",
                column: "trackID");

            migrationBuilder.AddForeignKey(
                name: "FK_Driver_Car_CarID",
                table: "Driver",
                column: "CarID",
                principalTable: "Car",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Lap_Car_carID",
                table: "Lap",
                column: "carID",
                principalTable: "Car",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lap_Race_raceID",
                table: "Lap",
                column: "raceID",
                principalTable: "Race",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lap_Track_TrackID",
                table: "Lap",
                column: "TrackID",
                principalTable: "Track",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Driver_Car_CarID",
                table: "Driver");

            migrationBuilder.DropForeignKey(
                name: "FK_Lap_Car_carID",
                table: "Lap");

            migrationBuilder.DropForeignKey(
                name: "FK_Lap_Race_raceID",
                table: "Lap");

            migrationBuilder.DropForeignKey(
                name: "FK_Lap_Track_TrackID",
                table: "Lap");

            migrationBuilder.DropTable(
                name: "Race");

            migrationBuilder.DropIndex(
                name: "IX_Lap_carID",
                table: "Lap");

            migrationBuilder.DropIndex(
                name: "IX_Lap_raceID",
                table: "Lap");

            migrationBuilder.DropIndex(
                name: "IX_Driver_CarID",
                table: "Driver");

            migrationBuilder.DropColumn(
                name: "carID",
                table: "Lap");

            migrationBuilder.DropColumn(
                name: "raceID",
                table: "Lap");

            migrationBuilder.DropColumn(
                name: "CarID",
                table: "Driver");

            migrationBuilder.RenameColumn(
                name: "TrackID",
                table: "Lap",
                newName: "recordedTrackID");

            migrationBuilder.RenameIndex(
                name: "IX_Lap_TrackID",
                table: "Lap",
                newName: "IX_Lap_recordedTrackID");

            migrationBuilder.AlterColumn<DateTime>(
                name: "recordedLapTime",
                table: "Lap",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "raceCarID",
                table: "Lap",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "recordedRaceStartTime",
                table: "Lap",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "driverID",
                table: "Car",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Lap_raceCarID",
                table: "Lap",
                column: "raceCarID");

            migrationBuilder.CreateIndex(
                name: "IX_Car_driverID",
                table: "Car",
                column: "driverID");

            migrationBuilder.AddForeignKey(
                name: "FK_Car_Driver_driverID",
                table: "Car",
                column: "driverID",
                principalTable: "Driver",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lap_Car_raceCarID",
                table: "Lap",
                column: "raceCarID",
                principalTable: "Car",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Lap_Track_recordedTrackID",
                table: "Lap",
                column: "recordedTrackID",
                principalTable: "Track",
                principalColumn: "ID");
        }
    }
}
