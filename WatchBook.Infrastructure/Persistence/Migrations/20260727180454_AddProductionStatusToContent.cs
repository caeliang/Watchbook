using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WatchBook.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddProductionStatusToContent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductionStatus",
                table: "Contents",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductionStatus",
                table: "Contents");
        }
    }
}
