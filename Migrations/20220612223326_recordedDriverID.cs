using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lapGen.Migrations
{
    public partial class recordedDriverID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lap_Driver_recordedDriverID",
                table: "Lap");

            migrationBuilder.AlterColumn<int>(
                name: "recordedDriverID",
                table: "Lap",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Lap_Driver_recordedDriverID",
                table: "Lap",
                column: "recordedDriverID",
                principalTable: "Driver",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lap_Driver_recordedDriverID",
                table: "Lap");

            migrationBuilder.AlterColumn<int>(
                name: "recordedDriverID",
                table: "Lap",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Lap_Driver_recordedDriverID",
                table: "Lap",
                column: "recordedDriverID",
                principalTable: "Driver",
                principalColumn: "ID");
        }
    }
}
