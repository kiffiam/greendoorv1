using Microsoft.EntityFrameworkCore.Migrations;

namespace GreenDoorV1.Migrations
{
    public partial class reviewsupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "RoomId",
                table: "Reviews",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "Reviews");
        }
    }
}
