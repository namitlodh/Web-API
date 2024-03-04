using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository_layer.Migrations
{
    public partial class Collaboration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Collaborations",
                columns: table => new
                {
                    CollabId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CollanEmail = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    IsTrash = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false),
                    NotesId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collaborations", x => x.CollabId);
                    table.ForeignKey(
                        name: "FK_Collaborations_users_Id",
                        column: x => x.Id,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Collaborations_Notes_NotesId",
                        column: x => x.NotesId,
                        principalTable: "Notes",
                        principalColumn: "NotesId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Collaborations_Id",
                table: "Collaborations",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Collaborations_NotesId",
                table: "Collaborations",
                column: "NotesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Collaborations");
        }
    }
}
