

using Microsoft.EntityFrameworkCore.Migrations;

namespace GreenDoorV1.Migrations
{
    public partial class deletedUserFromSet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoomName",
                table: "Reviews");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RoomName",
                table: "Reviews",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
