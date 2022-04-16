using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodoApp.Data.Migrations
{
    public partial class CreateEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Todos",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Todos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TodoId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSUTCDATETIME()"),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_Todos_TodoId",
                        column: x => x.TodoId,
                        principalSchema: "dbo",
                        principalTable: "Todos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Todos",
                columns: new[] { "Id", "IsCompleted", "Name" },
                values: new object[] { 1, false, "Learn Javascript" });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Todos",
                columns: new[] { "Id", "IsCompleted", "Name" },
                values: new object[] { 2, false, "Learn ASP.NET" });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Tasks",
                columns: new[] { "Id", "Name", "TodoId" },
                values: new object[,]
                {
                    { 1, "Learn Ecmascript", 1 },
                    { 2, "Learn Typescript", 1 },
                    { 3, "Learn C#", 2 },
                    { 4, "Learn SQL", 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_TodoId",
                schema: "dbo",
                table: "Tasks",
                column: "TodoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasks",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Todos",
                schema: "dbo");
        }
    }
}
