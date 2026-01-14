using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoListApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenameColmnINTASKS : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ExpectedEndData",
                table: "Tasks",
                newName: "ExpectedEndDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ExpectedEndDate",
                table: "Tasks",
                newName: "ExpectedEndData");
        }
    }
}
