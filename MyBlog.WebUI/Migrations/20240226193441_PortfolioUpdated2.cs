using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyBlog.WebUI.Migrations
{
    /// <inheritdoc />
    public partial class PortfolioUpdated2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectImage_Portfolios_PortfolioId",
                table: "ProjectImage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectImage",
                table: "ProjectImage");

            migrationBuilder.RenameTable(
                name: "ProjectImage",
                newName: "ProjectImages");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectImage_PortfolioId",
                table: "ProjectImages",
                newName: "IX_ProjectImages_PortfolioId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectImages",
                table: "ProjectImages",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectImages_Portfolios_PortfolioId",
                table: "ProjectImages",
                column: "PortfolioId",
                principalTable: "Portfolios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectImages_Portfolios_PortfolioId",
                table: "ProjectImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectImages",
                table: "ProjectImages");

            migrationBuilder.RenameTable(
                name: "ProjectImages",
                newName: "ProjectImage");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectImages_PortfolioId",
                table: "ProjectImage",
                newName: "IX_ProjectImage_PortfolioId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectImage",
                table: "ProjectImage",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectImage_Portfolios_PortfolioId",
                table: "ProjectImage",
                column: "PortfolioId",
                principalTable: "Portfolios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
