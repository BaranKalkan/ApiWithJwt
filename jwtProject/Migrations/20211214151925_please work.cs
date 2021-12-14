using Microsoft.EntityFrameworkCore.Migrations;

namespace jwtProject.Migrations
{
    public partial class pleasework : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "userid",
                table: "UserBook",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "userid",
                table: "UserBook");
        }
    }
}
