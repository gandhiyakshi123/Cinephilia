using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cinephilia.Data.Migrations
{
    public partial class RemoveOneColumun : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UniqueId",
                table: "BrowseBies");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UniqueId",
                table: "BrowseBies",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
