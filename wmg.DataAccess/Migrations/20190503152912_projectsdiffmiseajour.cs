using Microsoft.EntityFrameworkCore.Migrations;

namespace wmg.DataAccess.Migrations
{
    public partial class projectsdiffmiseajour : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LbDifficulty",
                table: "ProjectTypes",
                newName: "LbProject");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LbProject",
                table: "ProjectTypes",
                newName: "LbDifficulty");
        }
    }
}
