using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApplicationBarosa.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SeedDogTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "Dogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Breed = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SKU = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ListPrice = table.Column<double>(type: "float", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dogs_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dogs_CategoryId",
                table: "Dogs",
                column: "CategoryId");

            migrationBuilder.InsertData(
                table: "Dogs",
                columns: new[] { "Id", "Breed", "Description", "Gender", "ListPrice", "Name", "SKU", "CategoryId" },
                values: new object[,]
                {
                    { 1, "Golden Retriever", "Friendly and intelligent, the Golden Retriever is a popular family dog known for its gentle temperament and loyalty.\r\n\r\nIdeal for families and individuals alike, this breed requires regular exercise and grooming.", 0, 1200.0, "Goldie", "GR9999001", 1 },
                    { 2, "German Shepherd", "Highly intelligent and versatile, the German Shepherd is an excellent working dog and family companion. Known for its courage and loyalty.\r\n\r\nThis breed excels in various roles including police work and search and rescue.", 1, 1400.0, "Coco", "GS777777701", 2 },
                    { 3, "Bulldog", "With its distinctive wrinkled face and muscular build, the Bulldog is known for its friendly and courageous nature.\r\n\r\nA great companion for families, it requires minimal exercise but benefits from regular health check-ups.", 0, 1600.0, "Bully", "BD5555501", 3 },
                    { 4, "Poodle", "Elegant and intelligent, the Poodle comes in various sizes and is known for its hypoallergenic coat. It’s a versatile breed that excels in obedience and agility.\r\n\r\nRequires regular grooming and enjoys being an active family member.", 1, 1300.0, "Pookie", "PD3333333301", 4 },
                    { 5, "Beagle", "The Beagle is a small to medium-sized dog with a keen sense of smell and tracking instinct. It is known for its friendly and curious nature.\r\n\r\nIdeal for families, it needs regular exercise and enjoys outdoor activities.", 0, 950.0, "Beebee", "BG1111111101", 5 },
                    { 6, "Siberian Husky", "Known for its striking appearance and endurance, the Siberian Husky is a strong, energetic, and playful breed. It thrives in colder climates and requires regular exercise and mental stimulation.", 1, 1500.0, "Sibery", "SH000000001", 6 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dogs");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);
        }
    }
}
