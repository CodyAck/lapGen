using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lapGen.Migrations
{
    public partial class carToDiver : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "driverID",
                table: "Car",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Car_Driver_driverID",
                table: "Car");

            migrationBuilder.DropIndex(
                name: "IX_Car_driverID",
                table: "Car");

            migrationBuilder.DropColumn(
                name: "driverID",
                table: "Car");
        }
    }
}
