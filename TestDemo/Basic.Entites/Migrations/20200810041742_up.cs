using Microsoft.EntityFrameworkCore.Migrations;

namespace Basic.Entites.Migrations
{
    public partial class up : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CreationId",
                table: "Users",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "DeleteId",
                table: "Users",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "ModificationId",
                table: "Users",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DeleteId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ModificationId",
                table: "Users");
        }
    }
}
