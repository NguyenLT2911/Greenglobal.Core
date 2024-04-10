using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Greenglobal.Core.Migrations
{
    /// <inheritdoc />
    public partial class Add_Entity_Application_And_Update_Entities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserRoleDepts",
                schema: "auth");

            migrationBuilder.AddColumn<bool>(
                name: "AllowLogin",
                schema: "auth",
                table: "Users",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ShortName",
                schema: "auth",
                table: "Units",
                type: "character varying(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "ApplicationId",
                schema: "auth",
                table: "Roles",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "ShortName",
                schema: "auth",
                table: "Departments",
                type: "character varying(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Applications",
                schema: "auth",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ShortName = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    IconPath = table.Column<string>(type: "text", nullable: true),
                    SortOrder = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserTitleDepts",
                schema: "auth",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    TitleId = table.Column<Guid>(type: "uuid", nullable: false),
                    DepartmentId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsMain = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTitleDepts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTitleDepts_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalSchema: "auth",
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserTitleDepts_Titles_TitleId",
                        column: x => x.TitleId,
                        principalSchema: "auth",
                        principalTable: "Titles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserTitleDepts_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "auth",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Roles_ApplicationId",
                schema: "auth",
                table: "Roles",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTitleDepts_DepartmentId",
                schema: "auth",
                table: "UserTitleDepts",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTitleDepts_TitleId",
                schema: "auth",
                table: "UserTitleDepts",
                column: "TitleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTitleDepts_UserId",
                schema: "auth",
                table: "UserTitleDepts",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_Applications_ApplicationId",
                schema: "auth",
                table: "Roles",
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
                name: "FK_Roles_Applications_ApplicationId",
                schema: "auth",
                table: "Roles");

            migrationBuilder.DropTable(
                name: "Applications",
                schema: "auth");

            migrationBuilder.DropTable(
                name: "UserTitleDepts",
                schema: "auth");

            migrationBuilder.DropIndex(
                name: "IX_Roles_ApplicationId",
                schema: "auth",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "AllowLogin",
                schema: "auth",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ShortName",
                schema: "auth",
                table: "Units");

            migrationBuilder.DropColumn(
                name: "ApplicationId",
                schema: "auth",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "ShortName",
                schema: "auth",
                table: "Departments");

            migrationBuilder.CreateTable(
                name: "UserRoleDepts",
                schema: "auth",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DepartmentId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
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
    }
}
