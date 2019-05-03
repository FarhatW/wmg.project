using Microsoft.EntityFrameworkCore.Migrations;

namespace wmg.DataAccess.Migrations
{
    public partial class projectType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Credit",
                table: "ProjectTypes",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Credit",
                table: "ProjectTypes");
        }
    }
}
