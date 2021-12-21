using Microsoft.EntityFrameworkCore.Migrations;

namespace jwtProject.Migrations
{
    public partial class FavBooks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "URL",
                table: "AllUserBooks",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AllFavbooks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    userid = table.Column<string>(type: "TEXT", nullable: true),
                    bookId = table.Column<int>(type: "INTEGER", nullable: true),
                    URL = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllFavbooks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AllFavbooks_AllBooks_bookId",
                        column: x => x.bookId,
                        principalTable: "AllBooks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AllFavbooks_bookId",
                table: "AllFavbooks",
                column: "bookId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AllFavbooks");

            migrationBuilder.DropColumn(
                name: "URL",
                table: "AllUserBooks");
        }
    }
}
