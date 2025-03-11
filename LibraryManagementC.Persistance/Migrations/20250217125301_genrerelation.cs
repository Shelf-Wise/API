using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManagementC.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class genrerelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Genre_Books_BookId",
                table: "Genre");

            migrationBuilder.DropIndex(
                name: "IX_Genre_BookId",
                table: "Genre");

            migrationBuilder.DropColumn(
                name: "BookId",
                table: "Genre");

            migrationBuilder.CreateTable(
                name: "BookGenre",
                columns: table => new
                {
                    BooksId = table.Column<Guid>(type: "uuid", nullable: false),
                    GenresId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookGenre", x => new { x.BooksId, x.GenresId });
                    table.ForeignKey(
                        name: "FK_BookGenre_Books_BooksId",
                        column: x => x.BooksId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookGenre_Genre_GenresId",
                        column: x => x.GenresId,
                        principalTable: "Genre",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookGenre_GenresId",
                table: "BookGenre",
                column: "GenresId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookGenre");

            migrationBuilder.AddColumn<Guid>(
                name: "BookId",
                table: "Genre",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Genre_BookId",
                table: "Genre",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_Genre_Books_BookId",
                table: "Genre",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id");
        }
    }
}
