using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManagementC.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class MemberImagEURL : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Members",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Members");
        }
    }
}
