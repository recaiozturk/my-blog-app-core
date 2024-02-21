using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyBlog.WebUI.Migrations
{
    /// <inheritdoc />
    public partial class ExperienceSteps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExperienceSteps",
                table: "Experiences",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExperienceSteps",
                table: "Experiences");
        }
    }
}
