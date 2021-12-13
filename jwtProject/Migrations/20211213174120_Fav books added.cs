using Microsoft.EntityFrameworkCore.Migrations;

namespace jwtProject.Migrations
{
    public partial class Favbooksadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApiUserId1",
                table: "UserBook",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserBook_ApiUserId1",
                table: "UserBook",
                column: "ApiUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_UserBook_AspNetUsers_ApiUserId1",
                table: "UserBook",
                column: "ApiUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserBook_AspNetUsers_ApiUserId1",
                table: "UserBook");

            migrationBuilder.DropIndex(
                name: "IX_UserBook_ApiUserId1",
                table: "UserBook");

            migrationBuilder.DropColumn(
                name: "ApiUserId1",
                table: "UserBook");
        }
    }
}
