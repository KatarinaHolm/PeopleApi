using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PeopleApi.Migrations
{
    /// <inheritdoc />
    public partial class FixForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Interests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNr = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonsInterests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    InterestId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonsInterests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonsInterests_Interests_InterestId",
                        column: x => x.InterestId,
                        principalTable: "Interests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonsInterests_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Links",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersonInterestId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Links", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Links_PersonsInterests_PersonInterestId",
                        column: x => x.PersonInterestId,
                        principalTable: "PersonsInterests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Interests",
                columns: new[] { "Id", "Description", "Title" },
                values: new object[,]
                {
                    { 1, "Körsång är en form av vokalmusik där en grupp människor sjunger tillsammans, oftast under ledning av en dirigent. Det är en kollektiv konstform som bygger på samspel, gemenskap och harmonisering av röster.", "Körsång" },
                    { 2, "Träning är fysisk aktivitet med mål att förbättra styrka, kondition, rörlighet eller färdigheter genom regelbunden ansträngning, där kroppen anpassar sig genom att bygga upp muskler, stärka skelettet och förbättra hjärnans funktioner för att hantera ökad stress. Det involverar planering med specifika mål, val av övningar (som knäböj, bänkpress), rätt intensitet och återhämtning, ofta rekommenderat minst två gånger i veckan för styrka, med variation och progression för bästa resultat. ", "Träning" },
                    { 3, "Programmering är konsten att skriva instruktioner (kod) i ett programmeringsspråk som en dator eller annan apparatur kan förstå för att utföra specifika uppgifter, lösa problem, skapa appar, webbplatser eller automatisera processer; det är i grunden logiskt tänkande och att bryta ner en uppgift i små, exakta steg (en algoritm) som sedan översätts till maskinkod. Processen inkluderar analys, planering, kodning, testning och felsökning. ", "Programmering" },
                    { 4, "Matlagning är konsten, vetenskapen och hantverket att använda värme för att omvandla livsmedelsråvaror till färdig mat.Syftet är att göra maten mer välsmakande, lättsmält, näringsrik eller säker att äta. Det innefattar en rad tekniker, från grundläggande förberedelser till avancerade metoder som kräver precision.", "Matlagning" }
                });

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "Id", "FirstName", "LastName", "PhoneNr" },
                values: new object[,]
                {
                    { 1, "Mohammed", "Malik", "072-789 23 41" },
                    { 2, "Sara", "Wennergren", "076-346 44 81" },
                    { 3, "Lydia", "Abou", "073-339 15 47" }
                });

            migrationBuilder.InsertData(
                table: "PersonsInterests",
                columns: new[] { "Id", "InterestId", "PersonId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 1 },
                    { 3, 3, 2 },
                    { 4, 1, 2 },
                    { 5, 4, 3 },
                    { 6, 3, 3 }
                });

            migrationBuilder.InsertData(
                table: "Links",
                columns: new[] { "Id", "PersonInterestId", "Url" },
                values: new object[,]
                {
                    { 1, 1, "https://www.svenskakyrkan.se/sofia/sofia-nova" },
                    { 2, 1, "https://www.sverigeskorforbund.se/" },
                    { 3, 2, "https://www.sats.se/" },
                    { 4, 3, "https://chasacademy.se/" },
                    { 5, 4, "https://hjkk.se/" },
                    { 6, 5, "https://www.koket.se/" },
                    { 7, 6, "https://csharpskolan.se/" },
                    { 8, 6, "https://learn.microsoft.com/en-us/dotnet/csharp/" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Links_PersonInterestId",
                table: "Links",
                column: "PersonInterestId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonsInterests_InterestId",
                table: "PersonsInterests",
                column: "InterestId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonsInterests_PersonId",
                table: "PersonsInterests",
                column: "PersonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Links");

            migrationBuilder.DropTable(
                name: "PersonsInterests");

            migrationBuilder.DropTable(
                name: "Interests");

            migrationBuilder.DropTable(
                name: "Persons");
        }
    }
}
