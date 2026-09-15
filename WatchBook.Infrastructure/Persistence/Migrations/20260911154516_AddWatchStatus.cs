using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WatchBook.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddWatchStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WatchStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ContentId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WatchStatuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WatchStatuses_Contents_ContentId",
                        column: x => x.ContentId,
                        principalTable: "Contents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WatchStatuses_ContentId",
                table: "WatchStatuses",
                column: "ContentId");

            migrationBuilder.CreateIndex(
                name: "IX_WatchStatuses_UserId_ContentId",
                table: "WatchStatuses",
                columns: new[] { "UserId", "ContentId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WatchStatuses");
        }
    }
}
