using Microsoft.EntityFrameworkCore.Migrations;

namespace jwtProject.Migrations
{
    public partial class foreignkeysadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserBook_AspNetUsers_ApiUserId",
                table: "UserBook");

            migrationBuilder.DropForeignKey(
                name: "FK_UserBook_AspNetUsers_ApiUserId1",
                table: "UserBook");

            migrationBuilder.RenameColumn(
                name: "ApiUserId1",
                table: "UserBook",
                newName: "UsersFavBook");

            migrationBuilder.RenameColumn(
                name: "ApiUserId",
                table: "UserBook",
                newName: "UsersBook");

            migrationBuilder.RenameIndex(
                name: "IX_UserBook_ApiUserId1",
                table: "UserBook",
                newName: "IX_UserBook_UsersFavBook");

            migrationBuilder.RenameIndex(
                name: "IX_UserBook_ApiUserId",
                table: "UserBook",
                newName: "IX_UserBook_UsersBook");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserBook_AspNetUsers_UsersBook",
                table: "UserBook");

            migrationBuilder.DropForeignKey(
                name: "FK_UserBook_AspNetUsers_UsersFavBook",
                table: "UserBook");

            migrationBuilder.RenameColumn(
                name: "UsersFavBook",
                table: "UserBook",
                newName: "ApiUserId1");

            migrationBuilder.RenameColumn(
                name: "UsersBook",
                table: "UserBook",
                newName: "ApiUserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserBook_UsersFavBook",
                table: "UserBook",
                newName: "IX_UserBook_ApiUserId1");

            migrationBuilder.RenameIndex(
                name: "IX_UserBook_UsersBook",
                table: "UserBook",
                newName: "IX_UserBook_ApiUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserBook_AspNetUsers_ApiUserId",
                table: "UserBook",
                column: "ApiUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserBook_AspNetUsers_ApiUserId1",
                table: "UserBook",
                column: "ApiUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
