using Microsoft.EntityFrameworkCore.Migrations;

namespace OVGPFinalv1.Data.Migrations
{
    public partial class chatimportantvw : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ChatImportant",
                table: "Chat",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChatImportant",
                table: "Chat");
        }
    }
}
