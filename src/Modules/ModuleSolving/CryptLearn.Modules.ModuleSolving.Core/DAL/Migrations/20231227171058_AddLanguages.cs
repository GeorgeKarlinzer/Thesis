using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CryptLearn.Modules.ModuleSolving.Core.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddLanguages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Test",
                table: "Test");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Solutions",
                table: "Solutions");

            migrationBuilder.DropColumn(
                name: "Language",
                table: "Test");

            migrationBuilder.DropColumn(
                name: "Language",
                table: "Solutions");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Test",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "LanguageName",
                table: "Test",
                type: "nvarchar(256)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Solutions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "LanguageName",
                table: "Solutions",
                type: "nvarchar(256)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Test",
                table: "Test",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Solutions",
                table: "Solutions",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Language",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Language", x => x.Name);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Test_LanguageName",
                table: "Test",
                column: "LanguageName");

            migrationBuilder.CreateIndex(
                name: "IX_Test_ModuleId",
                table: "Test",
                column: "ModuleId");

            migrationBuilder.CreateIndex(
                name: "IX_Solutions_LanguageName",
                table: "Solutions",
                column: "LanguageName");

            migrationBuilder.CreateIndex(
                name: "IX_Solutions_ModuleId",
                table: "Solutions",
                column: "ModuleId");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Solutions_Language_LanguageName",
                table: "Solutions");

            migrationBuilder.DropForeignKey(
                name: "FK_Test_Language_LanguageName",
                table: "Test");

            migrationBuilder.DropTable(
                name: "Language");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Test",
                table: "Test");

            migrationBuilder.DropIndex(
                name: "IX_Test_LanguageName",
                table: "Test");

            migrationBuilder.DropIndex(
                name: "IX_Test_ModuleId",
                table: "Test");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Solutions",
                table: "Solutions");

            migrationBuilder.DropIndex(
                name: "IX_Solutions_LanguageName",
                table: "Solutions");

            migrationBuilder.DropIndex(
                name: "IX_Solutions_ModuleId",
                table: "Solutions");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Test");

            migrationBuilder.DropColumn(
                name: "LanguageName",
                table: "Test");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Solutions");

            migrationBuilder.DropColumn(
                name: "LanguageName",
                table: "Solutions");

            migrationBuilder.AddColumn<string>(
                name: "Language",
                table: "Test",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Language",
                table: "Solutions",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Test",
                table: "Test",
                columns: new[] { "ModuleId", "Language" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Solutions",
                table: "Solutions",
                columns: new[] { "ModuleId", "UserId", "Language" });
        }
    }
}
