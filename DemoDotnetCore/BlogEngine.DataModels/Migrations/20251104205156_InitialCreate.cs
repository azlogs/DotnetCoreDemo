using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogEngine.DataModels.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    email = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    username = table.Column<string>(type: "TEXT", unicode: false, maxLength: 50, nullable: false),
                    password = table.Column<string>(type: "TEXT", unicode: false, maxLength: 500, nullable: false),
                    fullname = table.Column<string>(type: "TEXT", unicode: false, maxLength: 100, nullable: false),
                    is_delete = table.Column<bool>(type: "INTEGER", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(sysdatetime())"),
                    updated_date = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(sysdatetime())"),
                    created_user_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    updated_user_id = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "post",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    title = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    slug_title = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    post_content = table.Column<string>(type: "ntext", nullable: false),
                    author = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    tags = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    is_delete = table.Column<bool>(type: "INTEGER", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(sysdatetime())"),
                    updated_date = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(sysdatetime())"),
                    created_user_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    updated_user_id = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_post", x => x.Id);
                    table.ForeignKey(
                        name: "FK_post_User",
                        column: x => x.created_user_id,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "comment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    parrent_comment_id = table.Column<Guid>(type: "TEXT", nullable: true),
                    user_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    comment_content = table.Column<string>(type: "TEXT", nullable: false),
                    post_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    is_delete = table.Column<bool>(type: "INTEGER", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(sysdatetime())"),
                    updated_date = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(sysdatetime())"),
                    created_user_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    updated_user_id = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_comment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_comment_User",
                        column: x => x.user_id,
                        principalTable: "User",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_comment_post",
                        column: x => x.post_id,
                        principalTable: "post",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_comment_post_id",
                table: "comment",
                column: "post_id");

            migrationBuilder.CreateIndex(
                name: "IX_comment_user_id",
                table: "comment",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_post_created_user_id",
                table: "post",
                column: "created_user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "comment");

            migrationBuilder.DropTable(
                name: "post");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
