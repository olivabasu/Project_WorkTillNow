using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DrugAPI.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Drug_Location",
                columns: table => new
                {
                    Drug_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Available_Stock = table.Column<int>(type: "int", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drug_Location", x => x.Drug_Id);
                });

            migrationBuilder.CreateTable(
                name: "Drug_Details",
                columns: table => new
                {
                    DrugId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Manufacturer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManufacturedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    cost = table.Column<double>(type: "float", nullable: false),
                    UnitPackage = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<double>(type: "float", nullable: false),
                    drugLocationDrug_Id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drug_Details", x => x.DrugId);
                    table.ForeignKey(
                        name: "FK_Drug_Details_Drug_Location_drugLocationDrug_Id",
                        column: x => x.drugLocationDrug_Id,
                        principalTable: "Drug_Location",
                        principalColumn: "Drug_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Drug_Details_drugLocationDrug_Id",
                table: "Drug_Details",
                column: "drugLocationDrug_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Drug_Details");

            migrationBuilder.DropTable(
                name: "Drug_Location");
        }
    }
}
