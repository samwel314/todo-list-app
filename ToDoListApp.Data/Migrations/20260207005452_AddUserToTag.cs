using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ToDoListApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUserToTag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Notes",
                keyColumn: "NoteId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Notes",
                keyColumn: "NoteId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Notes",
                keyColumn: "NoteId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Notes",
                keyColumn: "NoteId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "TagId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "TagId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "TagId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "TaskId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "TaskId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "TaskId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Tasks",
                keyColumn: "TaskId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "TagId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "TagId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "TagId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "TagId",
                keyValue: 5);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Tags",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_UserId",
                table: "Tags",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_AspNetUsers_UserId",
                table: "Tags",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_AspNetUsers_UserId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_UserId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Tags");

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "TagId", "TagName" },
                values: new object[,]
                {
                    { 1, "Work" },
                    { 2, "Personal" },
                    { 3, "Study" },
                    { 4, "Urgent" },
                    { 5, "Home" },
                    { 6, "Shopping" },
                    { 7, "Health" }
                });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "TaskId", "CompletedAT", "CreatedAT", "Description", "ExpectedEndDate", "IsCompleted", "Status", "TagId", "Title" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Understand DbContext, Migrations, and LINQ", new DateTime(2025, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 0, 2, "Learn EF Core" },
                    { 2, null, new DateTime(2024, 12, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Create CRUD endpoints with pagination and filtering", new DateTime(2025, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 1, 3, "Build ToDo API" },
                    { 3, new DateTime(2024, 12, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Test TaskService methods using xUnit", new DateTime(2024, 12, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 2, 4, "Write Unit Tests" },
                    { 4, null, new DateTime(2024, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Make generic repository work with Task and Note", new DateTime(2024, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 3, 5, "Refactor Repository" }
                });

            migrationBuilder.InsertData(
                table: "Notes",
                columns: new[] { "NoteId", "CreatedOrUpdatedAt", "Progress_Note", "TaskId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 1, 2, 10, 0, 0, 0, DateTimeKind.Unspecified), "Started reading EF Core documentation", 1 },
                    { 2, new DateTime(2025, 1, 3, 12, 0, 0, 0, DateTimeKind.Unspecified), "Implemented GetAll with pagination", 2 },
                    { 3, new DateTime(2024, 12, 28, 18, 0, 0, 0, DateTimeKind.Unspecified), "All unit tests passed successfully", 3 },
                    { 4, new DateTime(2024, 12, 21, 9, 0, 0, 0, DateTimeKind.Unspecified), "Refactoring stopped because deadline missed", 4 }
                });
        }
    }
}
