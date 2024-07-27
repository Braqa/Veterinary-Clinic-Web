using Microsoft.EntityFrameworkCore.Migrations;

namespace KlinikaVeterinareTI2.Data.Migrations
{
    public partial class AddedOwnersFieldToVisitsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Visits",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SerialNo",
                table: "Vaccines",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Visits_OwnerId",
                table: "Visits",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Visits_AspNetUsers_OwnerId",
                table: "Visits",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Visits_AspNetUsers_OwnerId",
                table: "Visits");

            migrationBuilder.DropIndex(
                name: "IX_Visits_OwnerId",
                table: "Visits");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Visits");

            migrationBuilder.AlterColumn<string>(
                name: "SerialNo",
                table: "Vaccines",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50);
        }
    }
}
