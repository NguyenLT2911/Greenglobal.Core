using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Greenglobal.Core.Migrations
{
    /// <inheritdoc />
    public partial class Add_Entity_UserRoleDepts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserRoleDepts",
                schema: "auth",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    DepartmentId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsMain = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoleDepts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRoleDepts_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalSchema: "auth",
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoleDepts_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "auth",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoleDepts_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "auth",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserRoleDepts_DepartmentId",
                schema: "auth",
                table: "UserRoleDepts",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoleDepts_RoleId",
                schema: "auth",
                table: "UserRoleDepts",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoleDepts_UserId",
                schema: "auth",
                table: "UserRoleDepts",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserRoleDepts",
                schema: "auth");
        }
    }
}
