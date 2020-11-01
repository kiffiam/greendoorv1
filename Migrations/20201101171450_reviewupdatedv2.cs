using Microsoft.EntityFrameworkCore.Migrations;

namespace GreenDoorV1.Migrations
{
    public partial class reviewupdatedv2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "Reviews",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Reviews");
        }
    }
}
