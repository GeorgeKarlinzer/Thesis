using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CryptLearn.Modules.ModuleSolving.Core.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTableNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Solutions_Language_LanguageName",
                table: "Solutions");

            migrationBuilder.DropForeignKey(
                name: "FK_Test_Language_LanguageName",
                table: "Test");

            migrationBuilder.DropForeignKey(
                name: "FK_Test_Modules_ModuleId",
                table: "Test");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Test",
                table: "Test");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Language",
                table: "Language");

            migrationBuilder.RenameTable(
                name: "Test",
                newName: "Tests");

            migrationBuilder.RenameTable(
                name: "Language",
                newName: "Languages");

            migrationBuilder.RenameIndex(
                name: "IX_Test_ModuleId",
                table: "Tests",
                newName: "IX_Tests_ModuleId");

            migrationBuilder.RenameIndex(
                name: "IX_Test_LanguageName",
                table: "Tests",
                newName: "IX_Tests_LanguageName");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tests",
                table: "Tests",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Languages",
                table: "Languages",
                column: "Name");

            migrationBuilder.AddForeignKey(
                name: "FK_Solutions_Languages_LanguageName",
                table: "Solutions",
                column: "LanguageName",
                principalTable: "Languages",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tests_Languages_LanguageName",
                table: "Tests",
                column: "LanguageName",
                principalTable: "Languages",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tests_Modules_ModuleId",
                table: "Tests",
                column: "ModuleId",
                principalTable: "Modules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Solutions_Languages_LanguageName",
                table: "Solutions");

            migrationBuilder.DropForeignKey(
                name: "FK_Tests_Languages_LanguageName",
                table: "Tests");

            migrationBuilder.DropForeignKey(
                name: "FK_Tests_Modules_ModuleId",
                table: "Tests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tests",
                table: "Tests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Languages",
                table: "Languages");

            migrationBuilder.RenameTable(
                name: "Tests",
                newName: "Test");

            migrationBuilder.RenameTable(
                name: "Languages",
                newName: "Language");

            migrationBuilder.RenameIndex(
                name: "IX_Tests_ModuleId",
                table: "Test",
                newName: "IX_Test_ModuleId");

            migrationBuilder.RenameIndex(
                name: "IX_Tests_LanguageName",
                table: "Test",
                newName: "IX_Test_LanguageName");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Test",
                table: "Test",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Language",
                table: "Language",
                column: "Name");

            migrationBuilder.AddForeignKey(
                name: "FK_Solutions_Language_LanguageName",
                table: "Solutions",
                column: "LanguageName",
                principalTable: "Language",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Test_Language_LanguageName",
                table: "Test",
                column: "LanguageName",
                principalTable: "Language",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Test_Modules_ModuleId",
                table: "Test",
                column: "ModuleId",
                principalTable: "Modules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
