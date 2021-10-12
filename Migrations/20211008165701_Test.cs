using Microsoft.EntityFrameworkCore.Migrations;

namespace NewProject.Migrations
{
    public partial class Test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SongPlaylist_Song_SongId",
                table: "SongPlaylist");

            migrationBuilder.AlterColumn<int>(
                name: "SongId",
                table: "SongPlaylist",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfilePicture",
                table: "Album",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SongPlaylist_Song_SongId",
                table: "SongPlaylist",
                column: "SongId",
                principalTable: "Song",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SongPlaylist_Song_SongId",
                table: "SongPlaylist");

            migrationBuilder.DropColumn(
                name: "ProfilePicture",
                table: "Album");

            migrationBuilder.AlterColumn<int>(
                name: "SongId",
                table: "SongPlaylist",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_SongPlaylist_Song_SongId",
                table: "SongPlaylist",
                column: "SongId",
                principalTable: "Song",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
