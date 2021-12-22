using Microsoft.EntityFrameworkCore.Migrations;

namespace jwtProject.Migrations
{
    public partial class Bookchanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "URL",
                table: "AllUserBooks");

            migrationBuilder.DropColumn(
                name: "URL",
                table: "AllFavbooks");

            migrationBuilder.DropColumn(
                name: "Author",
                table: "AllBooks");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "AllBooks");

            migrationBuilder.DropColumn(
                name: "TotalPage",
                table: "AllBooks");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "AllBooks",
                newName: "URL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "URL",
                table: "AllBooks",
                newName: "Title");

            migrationBuilder.AddColumn<string>(
                name: "URL",
                table: "AllUserBooks",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "URL",
                table: "AllFavbooks",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "AllBooks",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "AllBooks",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TotalPage",
                table: "AllBooks",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
