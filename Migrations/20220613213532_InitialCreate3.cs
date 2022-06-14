using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lapGen.Migrations
{
    public partial class InitialCreate3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lap_Car_carID",
                table: "Lap");

            migrationBuilder.DropForeignKey(
                name: "FK_Lap_Driver_recordedDriverID",
                table: "Lap");

            migrationBuilder.DropIndex(
                name: "IX_Lap_carID",
                table: "Lap");

            migrationBuilder.DropIndex(
                name: "IX_Lap_recordedDriverID",
                table: "Lap");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Lap_carID",
                table: "Lap",
                column: "carID");

            migrationBuilder.CreateIndex(
                name: "IX_Lap_recordedDriverID",
                table: "Lap",
                column: "recordedDriverID");

            migrationBuilder.AddForeignKey(
                name: "FK_Lap_Car_carID",
                table: "Lap",
                column: "carID",
                principalTable: "Car",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lap_Driver_recordedDriverID",
                table: "Lap",
                column: "recordedDriverID",
                principalTable: "Driver",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
