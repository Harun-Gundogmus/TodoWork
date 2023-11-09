using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TodoWorkServer.Migrations
{
    /// <inheritdoc />
    public partial class mg1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Todos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Work = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Todos", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Todos",
                columns: new[] { "Id", "IsCompleted", "Work" },
                values: new object[,]
                {
                    { 1, false, "Get up" },
                    { 2, false, "Work lesson" },
                    { 3, false, "Go home" },
                    { 4, false, "Take a shower" },
                    { 5, false, "Get sleep" },
                    { 6, true, "Check your e-mail" },
                    { 7, true, "Brush teeth" },
                    { 8, true, "Get to work" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Todos_Work",
                table: "Todos",
                column: "Work",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Todos");
        }
    }
}
