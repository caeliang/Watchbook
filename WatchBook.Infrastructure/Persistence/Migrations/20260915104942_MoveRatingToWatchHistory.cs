using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WatchBook.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class MoveRatingToWatchHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.AddColumn<int>(
                name: "EpisodeId",
                table: "WatchHistories",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Rating",
                table: "WatchHistories",
                type: "decimal(2,1)",
                precision: 2,
                scale: 1,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WatchHistories_EpisodeId",
                table: "WatchHistories",
                column: "EpisodeId");

            migrationBuilder.AddForeignKey(
                name: "FK_WatchHistories_Episodes_EpisodeId",
                table: "WatchHistories",
                column: "EpisodeId",
                principalTable: "Episodes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WatchHistories_Episodes_EpisodeId",
                table: "WatchHistories");

            migrationBuilder.DropIndex(
                name: "IX_WatchHistories_EpisodeId",
                table: "WatchHistories");

            migrationBuilder.DropColumn(
                name: "EpisodeId",
                table: "WatchHistories");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "WatchHistories");

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContentId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Score = table.Column<decimal>(type: "decimal(2,1)", precision: 2, scale: 1, nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ratings_Contents_ContentId",
                        column: x => x.ContentId,
                        principalTable: "Contents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_ContentId",
                table: "Ratings",
                column: "ContentId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_UserId_ContentId",
                table: "Ratings",
                columns: new[] { "UserId", "ContentId" },
                unique: true);
        }
    }
}
