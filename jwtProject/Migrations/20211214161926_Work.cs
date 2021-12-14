using Microsoft.EntityFrameworkCore.Migrations;

namespace jwtProject.Migrations
{
    public partial class Work : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserBook_AllBooks_bookId",
                table: "UserBook");

            migrationBuilder.DropForeignKey(
                name: "FK_UserBook_AspNetUsers_UsersBook",
                table: "UserBook");

            migrationBuilder.DropForeignKey(
                name: "FK_UserBook_AspNetUsers_UsersFavBook",
                table: "UserBook");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserBook",
                table: "UserBook");

            migrationBuilder.RenameTable(
                name: "UserBook",
                newName: "AllUserBooks");

            migrationBuilder.RenameIndex(
                name: "IX_UserBook_UsersFavBook",
                table: "AllUserBooks",
                newName: "IX_AllUserBooks_UsersFavBook");

            migrationBuilder.RenameIndex(
                name: "IX_UserBook_UsersBook",
                table: "AllUserBooks",
                newName: "IX_AllUserBooks_UsersBook");

            migrationBuilder.RenameIndex(
                name: "IX_UserBook_bookId",
                table: "AllUserBooks",
                newName: "IX_AllUserBooks_bookId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AllUserBooks",
                table: "AllUserBooks",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AllUserBooks_AllBooks_bookId",
                table: "AllUserBooks",
                column: "bookId",
                principalTable: "AllBooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AllUserBooks_AspNetUsers_UsersBook",
                table: "AllUserBooks",
                column: "UsersBook",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AllUserBooks_AspNetUsers_UsersFavBook",
                table: "AllUserBooks",
                column: "UsersFavBook",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AllUserBooks_AllBooks_bookId",
                table: "AllUserBooks");

            migrationBuilder.DropForeignKey(
                name: "FK_AllUserBooks_AspNetUsers_UsersBook",
                table: "AllUserBooks");

            migrationBuilder.DropForeignKey(
                name: "FK_AllUserBooks_AspNetUsers_UsersFavBook",
                table: "AllUserBooks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AllUserBooks",
                table: "AllUserBooks");

            migrationBuilder.RenameTable(
                name: "AllUserBooks",
                newName: "UserBook");

            migrationBuilder.RenameIndex(
                name: "IX_AllUserBooks_UsersFavBook",
                table: "UserBook",
                newName: "IX_UserBook_UsersFavBook");

            migrationBuilder.RenameIndex(
                name: "IX_AllUserBooks_UsersBook",
                table: "UserBook",
                newName: "IX_UserBook_UsersBook");

            migrationBuilder.RenameIndex(
                name: "IX_AllUserBooks_bookId",
                table: "UserBook",
                newName: "IX_UserBook_bookId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserBook",
                table: "UserBook",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserBook_AllBooks_bookId",
                table: "UserBook",
                column: "bookId",
                principalTable: "AllBooks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserBook_AspNetUsers_UsersBook",
                table: "UserBook",
                column: "UsersBook",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserBook_AspNetUsers_UsersFavBook",
                table: "UserBook",
                column: "UsersFavBook",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
