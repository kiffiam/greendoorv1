using Microsoft.EntityFrameworkCore.Migrations;

namespace GreenDoorV1.Migrations
{
    public partial class roles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8fd21843-3538-4a06-a0f9-3e1dc4e25278", "e6e758e8-36d4-4180-91a9-b5b69f00f583", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "003c7bf0-5e02-4fa9-930a-611469db3e58", "9b270245-55e2-4f5a-9d82-822874c63d00", "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "003c7bf0-5e02-4fa9-930a-611469db3e58");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8fd21843-3538-4a06-a0f9-3e1dc4e25278");
        }
    }
}
