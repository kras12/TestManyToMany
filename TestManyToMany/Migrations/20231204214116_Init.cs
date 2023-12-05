using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestManyToMany.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    BookEntityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.BookEntityId);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryEntityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryEntityId);
                });

            migrationBuilder.CreateTable(
                name: "BookEntityCategoryEntity",
                columns: table => new
                {
                    BooksBookEntityId = table.Column<int>(type: "int", nullable: false),
                    CategoriesCategoryEntityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookEntityCategoryEntity", x => new { x.BooksBookEntityId, x.CategoriesCategoryEntityId });
                    table.ForeignKey(
                        name: "FK_BookEntityCategoryEntity_Books_BooksBookEntityId",
                        column: x => x.BooksBookEntityId,
                        principalTable: "Books",
                        principalColumn: "BookEntityId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookEntityCategoryEntity_Categories_CategoriesCategoryEntityId",
                        column: x => x.CategoriesCategoryEntityId,
                        principalTable: "Categories",
                        principalColumn: "CategoryEntityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookEntityCategoryEntity_CategoriesCategoryEntityId",
                table: "BookEntityCategoryEntity",
                column: "CategoriesCategoryEntityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookEntityCategoryEntity");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
