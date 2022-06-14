using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lapGen.Migrations
{
    public partial class dbRebuild : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Car_Driver_currentDriverID",
                table: "Car");

            migrationBuilder.DropIndex(
                name: "IX_Car_currentDriverID",
                table: "Car");

            migrationBuilder.DropColumn(
                name: "currentDriverID",
                table: "Car");

            migrationBuilder.AddColumn<int>(
                name: "currentDriver",
                table: "Car",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "currentDriver",
                table: "Car");

            migrationBuilder.AddColumn<int>(
                name: "currentDriverID",
                table: "Car",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Car_currentDriverID",
                table: "Car",
                column: "currentDriverID");

            migrationBuilder.AddForeignKey(
                name: "FK_Car_Driver_currentDriverID",
                table: "Car",
                column: "currentDriverID",
                principalTable: "Driver",
                principalColumn: "ID");
        }
    }
}
