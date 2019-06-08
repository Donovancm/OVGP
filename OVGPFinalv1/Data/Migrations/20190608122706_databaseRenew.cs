using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OVGPFinalv1.Data.Migrations
{
    public partial class databaseRenew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Adres",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Bedrijf",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContactPersoon",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Plaats",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Postcode",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Adres",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Bedrijf",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ContactPersoon",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Plaats",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Postcode",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    PersonId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Address = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Land = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Postcode = table.Column<string>(nullable: true),
                    Stad = table.Column<string>(nullable: true),
                    TelefoonNummer = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.PersonId);
                });
        }
    }
}
