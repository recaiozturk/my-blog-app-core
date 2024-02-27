using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyBlog.WebUI.Migrations
{
    /// <inheritdoc />
    public partial class PortfolioImageIsCover : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCoverImage",
                table: "ProjectImages",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCoverImage",
                table: "ProjectImages");
        }
    }
}
