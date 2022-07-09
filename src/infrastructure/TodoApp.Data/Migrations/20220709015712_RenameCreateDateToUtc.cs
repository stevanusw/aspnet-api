using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodoApp.Data.Migrations
{
    public partial class RenameCreateDateToUtc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastUpdateDate",
                schema: "dbo",
                table: "Todos",
                newName: "LastUpdateDateUtc");

            migrationBuilder.RenameColumn(
                name: "CreateDate",
                schema: "dbo",
                table: "Todos",
                newName: "CreateDateUtc");

            migrationBuilder.RenameColumn(
                name: "LastUpdateDate",
                schema: "dbo",
                table: "Tasks",
                newName: "LastUpdateDateUtc");

            migrationBuilder.RenameColumn(
                name: "CreateDate",
                schema: "dbo",
                table: "Tasks",
                newName: "CreateDateUtc");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastUpdateDateUtc",
                schema: "dbo",
                table: "Todos",
                newName: "LastUpdateDate");

            migrationBuilder.RenameColumn(
                name: "CreateDateUtc",
                schema: "dbo",
                table: "Todos",
                newName: "CreateDate");

            migrationBuilder.RenameColumn(
                name: "LastUpdateDateUtc",
                schema: "dbo",
                table: "Tasks",
                newName: "LastUpdateDate");

            migrationBuilder.RenameColumn(
                name: "CreateDateUtc",
                schema: "dbo",
                table: "Tasks",
                newName: "CreateDate");
        }
    }
}
