using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.Migrations
{
    public partial class AddPropertiesToPatron : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BookId",
                table: "Copies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CopyId",
                table: "Checkouts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PatronId",
                table: "Checkouts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CopyId",
                table: "BookPatrons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Copies_BookId",
                table: "Copies",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_BookPatrons_CopyId",
                table: "BookPatrons",
                column: "CopyId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookPatrons_Copies_CopyId",
                table: "BookPatrons",
                column: "CopyId",
                principalTable: "Copies",
                principalColumn: "CopyId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Copies_Books_BookId",
                table: "Copies",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "BookId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookPatrons_Copies_CopyId",
                table: "BookPatrons");

            migrationBuilder.DropForeignKey(
                name: "FK_Copies_Books_BookId",
                table: "Copies");

            migrationBuilder.DropIndex(
                name: "IX_Copies_BookId",
                table: "Copies");

            migrationBuilder.DropIndex(
                name: "IX_BookPatrons_CopyId",
                table: "BookPatrons");

            migrationBuilder.DropColumn(
                name: "BookId",
                table: "Copies");

            migrationBuilder.DropColumn(
                name: "CopyId",
                table: "Checkouts");

            migrationBuilder.DropColumn(
                name: "PatronId",
                table: "Checkouts");

            migrationBuilder.DropColumn(
                name: "CopyId",
                table: "BookPatrons");
        }
    }
}
