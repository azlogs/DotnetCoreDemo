using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogEngine.DataModels.Migrations
{
    /// <inheritdoc />
    public partial class AddRolesAndUserRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "role",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    is_delete = table.Column<bool>(type: "INTEGER", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(datetime('now'))"),
                    updated_date = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(datetime('now'))"),
                    created_user_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    updated_user_id = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "user_role",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    user_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    role_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    is_delete = table.Column<bool>(type: "INTEGER", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(datetime('now'))"),
                    updated_date = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(datetime('now'))"),
                    created_user_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    updated_user_id = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_role", x => x.Id);
                    table.ForeignKey(
                        name: "FK_user_role_role",
                        column: x => x.role_id,
                        principalTable: "role",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_user_role_user",
                        column: x => x.user_id,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_user_role_role_id",
                table: "user_role",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_role_user_id",
                table: "user_role",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user_role");

            migrationBuilder.DropTable(
                name: "role");
        }
    }
}
