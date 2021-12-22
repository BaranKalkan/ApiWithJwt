using Microsoft.EntityFrameworkCore.Migrations;

namespace jwtProject.Migrations
{
    public partial class bookchangedwithgutendexinfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Authors",
                table: "AllBooks",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Subjects",
                table: "AllBooks",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "AllBooks",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Authors",
                table: "AllBooks");

            migrationBuilder.DropColumn(
                name: "Subjects",
                table: "AllBooks");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "AllBooks");
        }
    }
}
