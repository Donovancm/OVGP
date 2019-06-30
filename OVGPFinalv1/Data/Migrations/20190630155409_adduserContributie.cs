using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OVGPFinalv1.Data.Migrations
{
    public partial class adduserContributie : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "BedragTeVoldoen",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "BetaalBedrag",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Betaaldatum",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ContributieBetaald",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "KvKnummer",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "VorigeBetaalDatum",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BedragTeVoldoen",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "BetaalBedrag",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Betaaldatum",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ContributieBetaald",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "KvKnummer",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "VorigeBetaalDatum",
                table: "AspNetUsers");
        }
    }
}
