using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApplicationBarosa.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeOfBreed = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Dogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Breed = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SKU = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ListPrice = table.Column<double>(type: "float", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dogs_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "TypeOfBreed" },
                values: new object[,]
                {
                    { 9, "Hunting Dogs" },
                    { 10, "Companion Dogs" },
                    { 11, "Kid-Friendly Dogs" }
                });

            migrationBuilder.InsertData(
                table: "Dogs",
                columns: new[] { "Id", "Breed", "CategoryId", "Description", "Gender", "ImageUrl", "ListPrice", "SKU" },
                values: new object[,]
                {
                    { 1, "Golden Retriever", 11, "Friendly and intelligent, the Golden Retriever is a popular family dog known for its gentle temperament and loyalty.", 0, "", 1200.0, "GR9999001" },
                    { 2, "German Shepherd", 9, "Highly intelligent and versatile, the German Shepherd is an excellent working dog and family companion.", 1, "", 1400.0, "GS777777701" },
                    { 3, "Bulldog", 10, "With its distinctive wrinkled face and muscular build, the Bulldog is known for its friendly and courageous nature.", 0, "", 1600.0, "BD5555501" },
                    { 4, "Poodle", 10, "Elegant and intelligent, the Poodle comes in various sizes and is known for its hypoallergenic coat.", 1, "", 1300.0, "PD3333333301" },
                    { 5, "Beagle", 9, "The Beagle is known for its keen sense of smell and tracking instinct, making it an excellent hunting dog.", 0, "", 950.0, "BG1111111101" },
                    { 6, "Siberian Husky", 11, "Known for its striking appearance and endurance, the Siberian Husky is a strong, energetic, and playful breed.", 1, "", 1500.0, "SH000000001" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dogs_CategoryId",
                table: "Dogs",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dogs");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
