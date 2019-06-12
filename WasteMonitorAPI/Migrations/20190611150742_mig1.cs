using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WasteMonitorAPI.Migrations
{
    public partial class mig1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WasteData",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateTime = table.Column<DateTime>(nullable: false),
                    Weight = table.Column<double>(nullable: false),
                    FillingLevel = table.Column<double>(nullable: false),
                    wasEmptied = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WasteData", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "WasteData",
                columns: new[] { "Id", "DateTime", "FillingLevel", "Weight", "wasEmptied" },
                values: new object[] { 1, new DateTime(2019, 6, 11, 17, 7, 41, 814, DateTimeKind.Local).AddTicks(8933), 0.5, 20.0, false });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WasteData");
        }
    }
}
