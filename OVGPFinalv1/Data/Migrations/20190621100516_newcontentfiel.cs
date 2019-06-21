using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OVGPFinalv1.Data.Migrations
{
    public partial class newcontentfiel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ContentFile",
                table: "Content",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "ContentFile",
                table: "Content",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
