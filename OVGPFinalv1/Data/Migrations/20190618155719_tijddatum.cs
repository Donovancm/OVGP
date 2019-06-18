using Microsoft.EntityFrameworkCore.Migrations;

namespace OVGPFinalv1.Data.Migrations
{
    public partial class tijddatum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartDatum",
                table: "Agenda",
                newName: "Tijd");

            migrationBuilder.RenameColumn(
                name: "EindDatum",
                table: "Agenda",
                newName: "Datum");

            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "Chat",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PersonName",
                table: "Chat",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Tijd",
                table: "Agenda",
                newName: "StartDatum");

            migrationBuilder.RenameColumn(
                name: "Datum",
                table: "Agenda",
                newName: "EindDatum");

            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "Chat",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "PersonName",
                table: "Chat",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
