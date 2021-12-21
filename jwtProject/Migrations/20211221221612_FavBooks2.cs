using Microsoft.EntityFrameworkCore.Migrations;

namespace jwtProject.Migrations
{
    public partial class FavBooks2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AllUserBooks_AspNetUsers_UsersFavBook",
                table: "AllUserBooks");

            migrationBuilder.DropIndex(
                name: "IX_AllUserBooks_UsersFavBook",
                table: "AllUserBooks");

            migrationBuilder.DropColumn(
                name: "UsersFavBook",
                table: "AllUserBooks");

            migrationBuilder.AddColumn<string>(
                name: "UsersFavBook",
                table: "AllFavbooks",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AllFavbooks_UsersFavBook",
                table: "AllFavbooks",
                column: "UsersFavBook");

            migrationBuilder.AddForeignKey(
                name: "FK_AllFavbooks_AspNetUsers_UsersFavBook",
                table: "AllFavbooks",
                column: "UsersFavBook",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AllFavbooks_AspNetUsers_UsersFavBook",
                table: "AllFavbooks");

            migrationBuilder.DropIndex(
                name: "IX_AllFavbooks_UsersFavBook",
                table: "AllFavbooks");

            migrationBuilder.DropColumn(
                name: "UsersFavBook",
                table: "AllFavbooks");

            migrationBuilder.AddColumn<string>(
                name: "UsersFavBook",
                table: "AllUserBooks",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AllUserBooks_UsersFavBook",
                table: "AllUserBooks",
                column: "UsersFavBook");

            migrationBuilder.AddForeignKey(
                name: "FK_AllUserBooks_AspNetUsers_UsersFavBook",
                table: "AllUserBooks",
                column: "UsersFavBook",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
