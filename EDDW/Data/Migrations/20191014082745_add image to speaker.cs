using Microsoft.EntityFrameworkCore.Migrations;

namespace EDDW.Data.Migrations
{
    public partial class addimagetospeaker : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageURL",
                table: "Speaker",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageURL",
                table: "Speaker");
        }
    }
}
