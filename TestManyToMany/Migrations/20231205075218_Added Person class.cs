using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestManyToMany.Migrations
{
    /// <inheritdoc />
    public partial class AddedPersonclass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PersonEntityPersonId",
                table: "Books",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    PersonId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.PersonId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_PersonEntityPersonId",
                table: "Books",
                column: "PersonEntityPersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Person_PersonEntityPersonId",
                table: "Books",
                column: "PersonEntityPersonId",
                principalTable: "Person",
                principalColumn: "PersonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Person_PersonEntityPersonId",
                table: "Books");

            migrationBuilder.DropTable(
                name: "Person");

            migrationBuilder.DropIndex(
                name: "IX_Books_PersonEntityPersonId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "PersonEntityPersonId",
                table: "Books");
        }
    }
}
