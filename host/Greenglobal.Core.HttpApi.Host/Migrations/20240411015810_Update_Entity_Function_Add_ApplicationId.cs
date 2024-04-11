using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Greenglobal.Core.Migrations
{
    /// <inheritdoc />
    public partial class Update_Entity_Function_Add_ApplicationId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ApplicationId",
                schema: "auth",
                table: "Functions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Functions_ApplicationId",
                schema: "auth",
                table: "Functions",
                column: "ApplicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Functions_Applications_ApplicationId",
                schema: "auth",
                table: "Functions",
                column: "ApplicationId",
                principalSchema: "auth",
                principalTable: "Applications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Functions_Applications_ApplicationId",
                schema: "auth",
                table: "Functions");

            migrationBuilder.DropIndex(
                name: "IX_Functions_ApplicationId",
                schema: "auth",
                table: "Functions");

            migrationBuilder.DropColumn(
                name: "ApplicationId",
                schema: "auth",
                table: "Functions");
        }
    }
}
