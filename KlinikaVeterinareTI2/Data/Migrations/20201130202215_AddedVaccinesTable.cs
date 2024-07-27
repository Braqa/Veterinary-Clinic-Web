using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KlinikaVeterinareTI2.Data.Migrations
{
    public partial class AddedVaccinesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vaccines",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SerialNo = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    InsertBy = table.Column<string>(nullable: true),
                    InsertDate = table.Column<DateTime>(nullable: false),
                    LUB = table.Column<string>(nullable: true),
                    LUD = table.Column<DateTime>(nullable: false),
                    LUN = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vaccines", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vaccines");
        }
    }
}
