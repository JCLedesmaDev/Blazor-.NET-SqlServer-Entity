using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorFullStack.Server.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ComicsEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComicsEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SuperHeroesEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HeroName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ComicId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuperHeroesEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SuperHeroesEntity_ComicsEntity_ComicId",
                        column: x => x.ComicId,
                        principalTable: "ComicsEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ComicsEntity",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Marvel" });

            migrationBuilder.InsertData(
                table: "ComicsEntity",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "DC" });

            migrationBuilder.InsertData(
                table: "SuperHeroesEntity",
                columns: new[] { "Id", "ComicId", "FirstName", "HeroName", "LastName" },
                values: new object[] { 1, 1, "Peter", "Spider-Man", "Parker" });

            migrationBuilder.InsertData(
                table: "SuperHeroesEntity",
                columns: new[] { "Id", "ComicId", "FirstName", "HeroName", "LastName" },
                values: new object[] { 2, 2, "Bruce", "Batman", "Wayne" });

            migrationBuilder.CreateIndex(
                name: "IX_SuperHeroesEntity_ComicId",
                table: "SuperHeroesEntity",
                column: "ComicId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SuperHeroesEntity");

            migrationBuilder.DropTable(
                name: "ComicsEntity");
        }
    }
}
