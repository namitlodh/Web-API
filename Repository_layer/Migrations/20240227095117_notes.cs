using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository_layer.Migrations
{
    public partial class notes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_users_Id",
                table: "Notes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Notes",
                table: "Notes");

            migrationBuilder.RenameTable(
                name: "Notes",
                newName: "NoteEntity");

            migrationBuilder.RenameIndex(
                name: "IX_Notes_Id",
                table: "NoteEntity",
                newName: "IX_NoteEntity_Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NoteEntity",
                table: "NoteEntity",
                column: "NotesId");

            migrationBuilder.AddForeignKey(
                name: "FK_NoteEntity_users_Id",
                table: "NoteEntity",
                column: "Id",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NoteEntity_users_Id",
                table: "NoteEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NoteEntity",
                table: "NoteEntity");

            migrationBuilder.RenameTable(
                name: "NoteEntity",
                newName: "Notes");

            migrationBuilder.RenameIndex(
                name: "IX_NoteEntity_Id",
                table: "Notes",
                newName: "IX_Notes_Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Notes",
                table: "Notes",
                column: "NotesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_users_Id",
                table: "Notes",
                column: "Id",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
