using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Set_Backend.Migrations
{
    /// <inheritdoc />
    public partial class Games : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PlayerId = table.Column<int>(type: "integer", nullable: false),
                    Hints = table.Column<int>(type: "integer", nullable: false),
                    Fails = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FinishedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Games_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Colour = table.Column<int>(type: "integer", nullable: false),
                    Shape = table.Column<int>(type: "integer", nullable: false),
                    Filling = table.Column<int>(type: "integer", nullable: false),
                    Number = table.Column<int>(type: "integer", nullable: false),
                    GameId = table.Column<int>(type: "integer", nullable: true),
                    GameId1 = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cards_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cards_Games_GameId1",
                        column: x => x.GameId1,
                        principalTable: "Games",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FoundSets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GameId = table.Column<int>(type: "integer", nullable: false),
                    FoundAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Card1Id = table.Column<int>(type: "integer", nullable: false),
                    Card2Id = table.Column<int>(type: "integer", nullable: false),
                    Card3Id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoundSets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FoundSets_Cards_Card1Id",
                        column: x => x.Card1Id,
                        principalTable: "Cards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FoundSets_Cards_Card2Id",
                        column: x => x.Card2Id,
                        principalTable: "Cards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FoundSets_Cards_Card3Id",
                        column: x => x.Card3Id,
                        principalTable: "Cards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FoundSets_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Cards",
                columns: new[] { "Id", "Colour", "Filling", "GameId", "GameId1", "Number", "Shape" },
                values: new object[,]
                {
                    { 1, 0, 0, null, null, 1, 0 },
                    { 2, 0, 0, null, null, 2, 0 },
                    { 3, 0, 0, null, null, 3, 0 },
                    { 4, 0, 1, null, null, 1, 0 },
                    { 5, 0, 1, null, null, 2, 0 },
                    { 6, 0, 1, null, null, 3, 0 },
                    { 7, 0, 2, null, null, 1, 0 },
                    { 8, 0, 2, null, null, 2, 0 },
                    { 9, 0, 2, null, null, 3, 0 },
                    { 10, 0, 0, null, null, 1, 1 },
                    { 11, 0, 0, null, null, 2, 1 },
                    { 12, 0, 0, null, null, 3, 1 },
                    { 13, 0, 1, null, null, 1, 1 },
                    { 14, 0, 1, null, null, 2, 1 },
                    { 15, 0, 1, null, null, 3, 1 },
                    { 16, 0, 2, null, null, 1, 1 },
                    { 17, 0, 2, null, null, 2, 1 },
                    { 18, 0, 2, null, null, 3, 1 },
                    { 19, 0, 0, null, null, 1, 2 },
                    { 20, 0, 0, null, null, 2, 2 },
                    { 21, 0, 0, null, null, 3, 2 },
                    { 22, 0, 1, null, null, 1, 2 },
                    { 23, 0, 1, null, null, 2, 2 },
                    { 24, 0, 1, null, null, 3, 2 },
                    { 25, 0, 2, null, null, 1, 2 },
                    { 26, 0, 2, null, null, 2, 2 },
                    { 27, 0, 2, null, null, 3, 2 },
                    { 28, 1, 0, null, null, 1, 0 },
                    { 29, 1, 0, null, null, 2, 0 },
                    { 30, 1, 0, null, null, 3, 0 },
                    { 31, 1, 1, null, null, 1, 0 },
                    { 32, 1, 1, null, null, 2, 0 },
                    { 33, 1, 1, null, null, 3, 0 },
                    { 34, 1, 2, null, null, 1, 0 },
                    { 35, 1, 2, null, null, 2, 0 },
                    { 36, 1, 2, null, null, 3, 0 },
                    { 37, 1, 0, null, null, 1, 1 },
                    { 38, 1, 0, null, null, 2, 1 },
                    { 39, 1, 0, null, null, 3, 1 },
                    { 40, 1, 1, null, null, 1, 1 },
                    { 41, 1, 1, null, null, 2, 1 },
                    { 42, 1, 1, null, null, 3, 1 },
                    { 43, 1, 2, null, null, 1, 1 },
                    { 44, 1, 2, null, null, 2, 1 },
                    { 45, 1, 2, null, null, 3, 1 },
                    { 46, 1, 0, null, null, 1, 2 },
                    { 47, 1, 0, null, null, 2, 2 },
                    { 48, 1, 0, null, null, 3, 2 },
                    { 49, 1, 1, null, null, 1, 2 },
                    { 50, 1, 1, null, null, 2, 2 },
                    { 51, 1, 1, null, null, 3, 2 },
                    { 52, 1, 2, null, null, 1, 2 },
                    { 53, 1, 2, null, null, 2, 2 },
                    { 54, 1, 2, null, null, 3, 2 },
                    { 55, 2, 0, null, null, 1, 0 },
                    { 56, 2, 0, null, null, 2, 0 },
                    { 57, 2, 0, null, null, 3, 0 },
                    { 58, 2, 1, null, null, 1, 0 },
                    { 59, 2, 1, null, null, 2, 0 },
                    { 60, 2, 1, null, null, 3, 0 },
                    { 61, 2, 2, null, null, 1, 0 },
                    { 62, 2, 2, null, null, 2, 0 },
                    { 63, 2, 2, null, null, 3, 0 },
                    { 64, 2, 0, null, null, 1, 1 },
                    { 65, 2, 0, null, null, 2, 1 },
                    { 66, 2, 0, null, null, 3, 1 },
                    { 67, 2, 1, null, null, 1, 1 },
                    { 68, 2, 1, null, null, 2, 1 },
                    { 69, 2, 1, null, null, 3, 1 },
                    { 70, 2, 2, null, null, 1, 1 },
                    { 71, 2, 2, null, null, 2, 1 },
                    { 72, 2, 2, null, null, 3, 1 },
                    { 73, 2, 0, null, null, 1, 2 },
                    { 74, 2, 0, null, null, 2, 2 },
                    { 75, 2, 0, null, null, 3, 2 },
                    { 76, 2, 1, null, null, 1, 2 },
                    { 77, 2, 1, null, null, 2, 2 },
                    { 78, 2, 1, null, null, 3, 2 },
                    { 79, 2, 2, null, null, 1, 2 },
                    { 80, 2, 2, null, null, 2, 2 },
                    { 81, 2, 2, null, null, 3, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cards_GameId",
                table: "Cards",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Cards_GameId1",
                table: "Cards",
                column: "GameId1");

            migrationBuilder.CreateIndex(
                name: "IX_FoundSets_Card1Id",
                table: "FoundSets",
                column: "Card1Id");

            migrationBuilder.CreateIndex(
                name: "IX_FoundSets_Card2Id",
                table: "FoundSets",
                column: "Card2Id");

            migrationBuilder.CreateIndex(
                name: "IX_FoundSets_Card3Id",
                table: "FoundSets",
                column: "Card3Id");

            migrationBuilder.CreateIndex(
                name: "IX_FoundSets_GameId",
                table: "FoundSets",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_PlayerId",
                table: "Games",
                column: "PlayerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FoundSets");

            migrationBuilder.DropTable(
                name: "Cards");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Players");
        }
    }
}
