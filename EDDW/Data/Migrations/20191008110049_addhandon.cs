using Microsoft.EntityFrameworkCore.Migrations;

namespace EDDW.Data.Migrations
{
    public partial class addhandon : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Programme",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Programme");
        }
    }
}
