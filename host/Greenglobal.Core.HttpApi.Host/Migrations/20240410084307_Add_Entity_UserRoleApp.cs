using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Greenglobal.Core.Migrations
{
    /// <inheritdoc />
    public partial class Add_Entity_UserRoleApp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserRoleApps",
                schema: "auth",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    ApplicationId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsMain = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoleApps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRoleApps_Applications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalSchema: "auth",
                        principalTable: "Applications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoleApps_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "auth",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoleApps_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "auth",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserRoleApps_ApplicationId",
                schema: "auth",
                table: "UserRoleApps",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoleApps_RoleId",
                schema: "auth",
                table: "UserRoleApps",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoleApps_UserId",
                schema: "auth",
                table: "UserRoleApps",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserRoleApps",
                schema: "auth");
        }
    }
}
