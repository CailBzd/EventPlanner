using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eventplanner_server.Migrations
{
    /// <inheritdoc />
    public partial class AddImageEvent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Events",
                type: "bytea",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Events");
        }
    }
}
