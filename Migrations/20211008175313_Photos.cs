using Microsoft.EntityFrameworkCore.Migrations;

namespace NewProject.Migrations
{
    public partial class Photos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfilePicture",
                table: "User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfilePicture",
                table: "Song",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfilePicture",
                table: "Playlist",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfilePicture",
                table: "User");

            migrationBuilder.DropColumn(
                name: "ProfilePicture",
                table: "Song");

            migrationBuilder.DropColumn(
                name: "ProfilePicture",
                table: "Playlist");
        }
    }
}
