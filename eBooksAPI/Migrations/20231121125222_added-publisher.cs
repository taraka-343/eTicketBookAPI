using Microsoft.EntityFrameworkCore.Migrations;

namespace eBooksAPI.Migrations
{
    public partial class addedpublisher : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "publisherId",
                table: "books",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Publishers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    publisherName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publishers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_books_publisherId",
                table: "books",
                column: "publisherId");

            migrationBuilder.AddForeignKey(
                name: "FK_books_Publishers_publisherId",
                table: "books",
                column: "publisherId",
                principalTable: "Publishers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_books_Publishers_publisherId",
                table: "books");

            migrationBuilder.DropTable(
                name: "Publishers");

            migrationBuilder.DropIndex(
                name: "IX_books_publisherId",
                table: "books");

            migrationBuilder.DropColumn(
                name: "publisherId",
                table: "books");
        }
    }
}
