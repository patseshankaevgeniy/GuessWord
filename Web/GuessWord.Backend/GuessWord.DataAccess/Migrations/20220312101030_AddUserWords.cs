using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GuessWord.DataAccess.Migrations
{
    public partial class AddUserWords : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Translators",
                schema: "dbo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersWords",
                schema: "dbo",
                table: "UsersWords");

            migrationBuilder.DropColumn(
                name: "New",
                schema: "dbo",
                table: "Words");

            migrationBuilder.RenameTable(
                name: "UsersWords",
                schema: "dbo",
                newName: "UserWords",
                newSchema: "dbo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserWords",
                schema: "dbo",
                table: "UserWords",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "WordTranslations",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WordId = table.Column<int>(type: "int", nullable: false),
                    WordTranslationId = table.Column<int>(type: "int", nullable: false),
                    TranslationId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WordTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WordTranslations_Words_TranslationId",
                        column: x => x.TranslationId,
                        principalSchema: "dbo",
                        principalTable: "Words",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WordTranslations_Words_WordId",
                        column: x => x.WordId,
                        principalSchema: "dbo",
                        principalTable: "Words",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserWords_UserId",
                schema: "dbo",
                table: "UserWords",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserWords_WordId",
                schema: "dbo",
                table: "UserWords",
                column: "WordId");

            migrationBuilder.CreateIndex(
                name: "IX_WordTranslations_TranslationId",
                schema: "dbo",
                table: "WordTranslations",
                column: "TranslationId");

            migrationBuilder.CreateIndex(
                name: "IX_WordTranslations_WordId",
                schema: "dbo",
                table: "WordTranslations",
                column: "WordId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserWords_Users_UserId",
                schema: "dbo",
                table: "UserWords",
                column: "UserId",
                principalSchema: "dbo",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserWords_Words_WordId",
                schema: "dbo",
                table: "UserWords",
                column: "WordId",
                principalSchema: "dbo",
                principalTable: "Words",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserWords_Users_UserId",
                schema: "dbo",
                table: "UserWords");

            migrationBuilder.DropForeignKey(
                name: "FK_UserWords_Words_WordId",
                schema: "dbo",
                table: "UserWords");

            migrationBuilder.DropTable(
                name: "WordTranslations",
                schema: "dbo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserWords",
                schema: "dbo",
                table: "UserWords");

            migrationBuilder.DropIndex(
                name: "IX_UserWords_UserId",
                schema: "dbo",
                table: "UserWords");

            migrationBuilder.DropIndex(
                name: "IX_UserWords_WordId",
                schema: "dbo",
                table: "UserWords");

            migrationBuilder.RenameTable(
                name: "UserWords",
                schema: "dbo",
                newName: "UsersWords",
                newSchema: "dbo");

            migrationBuilder.AddColumn<string>(
                name: "New",
                schema: "dbo",
                table: "Words",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersWords",
                schema: "dbo",
                table: "UsersWords",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Translators",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WordId = table.Column<int>(type: "int", nullable: false),
                    WordTranslationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Translators", x => x.Id);
                });
        }
    }
}
