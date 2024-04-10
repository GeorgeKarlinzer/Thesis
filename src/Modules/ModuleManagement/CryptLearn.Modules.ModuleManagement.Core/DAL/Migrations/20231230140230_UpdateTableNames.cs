using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CryptLearn.Modules.ModuleManagement.Core.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTableNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Template_Languages_LanguageName",
                table: "Template");

            migrationBuilder.DropForeignKey(
                name: "FK_Template_Modules_ModuleId",
                table: "Template");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Template",
                table: "Template");

            migrationBuilder.RenameTable(
                name: "Template",
                newName: "Templates");

            migrationBuilder.RenameIndex(
                name: "IX_Template_ModuleId",
                table: "Templates",
                newName: "IX_Templates_ModuleId");

            migrationBuilder.RenameIndex(
                name: "IX_Template_LanguageName",
                table: "Templates",
                newName: "IX_Templates_LanguageName");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Templates",
                table: "Templates",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Templates_Languages_LanguageName",
                table: "Templates",
                column: "LanguageName",
                principalTable: "Languages",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Templates_Modules_ModuleId",
                table: "Templates",
                column: "ModuleId",
                principalTable: "Modules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Templates_Languages_LanguageName",
                table: "Templates");

            migrationBuilder.DropForeignKey(
                name: "FK_Templates_Modules_ModuleId",
                table: "Templates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Templates",
                table: "Templates");

            migrationBuilder.RenameTable(
                name: "Templates",
                newName: "Template");

            migrationBuilder.RenameIndex(
                name: "IX_Templates_ModuleId",
                table: "Template",
                newName: "IX_Template_ModuleId");

            migrationBuilder.RenameIndex(
                name: "IX_Templates_LanguageName",
                table: "Template",
                newName: "IX_Template_LanguageName");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Template",
                table: "Template",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Template_Languages_LanguageName",
                table: "Template",
                column: "LanguageName",
                principalTable: "Languages",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Template_Modules_ModuleId",
                table: "Template",
                column: "ModuleId",
                principalTable: "Modules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
