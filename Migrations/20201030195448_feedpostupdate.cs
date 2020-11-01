using Microsoft.EntityFrameworkCore.Migrations;

namespace GreenDoorV1.Migrations
{
    public partial class feedpostupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Rooms_RoomId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_RoomId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "Reviews");

            migrationBuilder.AddColumn<string>(
                name: "RoomName",
                table: "Reviews",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "FeedPosts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoomName",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "FeedPosts");

            migrationBuilder.AddColumn<long>(
                name: "RoomId",
                table: "Reviews",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_RoomId",
                table: "Reviews",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Rooms_RoomId",
                table: "Reviews",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
