using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AplikacjaLabyData.Migrations
{
    /// <inheritdoc />
    public partial class OwnerAndAdressAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "books",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Owners",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Surname = table.Column<string>(type: "TEXT", nullable: true),
                    Address_City = table.Column<string>(type: "TEXT", nullable: true),
                    Address_Street = table.Column<string>(type: "TEXT", nullable: true),
                    Address_PostalCode = table.Column<string>(type: "TEXT", nullable: true),
                    Address_Region = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Owners", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Owners",
                columns: new[] { "Id", "Address_City", "Address_PostalCode", "Address_Region", "Address_Street", "Name", "Surname" },
                values: new object[,]
                {
                    { 1, "Kraków", "31-150", "małopolskie", "Św. Filipa 17", "Pawel", "Bielecki" },
                    { 2, "Kraków", "31-150", "małopolskie", "Krowoderska 45/6", "Jacek", "Obrzut" }
                });

            migrationBuilder.UpdateData(
                table: "books",
                keyColumn: "Id",
                keyValue: 1,
                column: "OwnerId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "books",
                keyColumn: "Id",
                keyValue: 2,
                column: "OwnerId",
                value: 2);

            migrationBuilder.CreateIndex(
                name: "IX_books_OwnerId",
                table: "books",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_books_Owners_OwnerId",
                table: "books",
                column: "OwnerId",
                principalTable: "Owners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_books_Owners_OwnerId",
                table: "books");

            migrationBuilder.DropTable(
                name: "Owners");

            migrationBuilder.DropIndex(
                name: "IX_books_OwnerId",
                table: "books");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "books");
        }
    }
}
