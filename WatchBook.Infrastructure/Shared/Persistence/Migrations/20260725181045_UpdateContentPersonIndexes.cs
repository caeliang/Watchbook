using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WatchBook.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateContentPersonIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ContentPeople_ContentId_Role",
                table: "ContentPeople");

            migrationBuilder.DropIndex(
                name: "IX_ContentPeople_Role",
                table: "ContentPeople");

            migrationBuilder.CreateIndex(
                name: "IX_ContentPeople_ContentId_Role",
                table: "ContentPeople",
                columns: ["ContentId", "Role", "DisplayOrder"]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ContentPeople_ContentId_Role",
                table: "ContentPeople");

            migrationBuilder.CreateIndex(
                name: "IX_ContentPeople_ContentId_Role",
                table: "ContentPeople",
                columns: ["ContentId", "Role"]);

            migrationBuilder.CreateIndex(
                name: "IX_ContentPeople_Role",
                table: "ContentPeople",
                column: "Role");
        }
    }
}
