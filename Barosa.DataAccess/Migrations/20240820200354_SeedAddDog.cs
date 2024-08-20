using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplicationBarosa.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SeedAddDog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Dogs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Hunting Dogs");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Companion Dogs");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Kid-Friendly Dogs");

            migrationBuilder.UpdateData(
                table: "Dogs",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CategoryId", "Description" },
                values: new object[] { 3, "Friendly and intelligent, the Golden Retriever is a popular family dog known for its gentle temperament and loyalty." });

            migrationBuilder.UpdateData(
                table: "Dogs",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CategoryId", "Description" },
                values: new object[] { 1, "Highly intelligent and versatile, the German Shepherd is an excellent working dog and family companion." });

            migrationBuilder.UpdateData(
                table: "Dogs",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CategoryId", "Description" },
                values: new object[] { 2, "With its distinctive wrinkled face and muscular build, the Bulldog is known for its friendly and courageous nature." });

            migrationBuilder.UpdateData(
                table: "Dogs",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CategoryId", "Description" },
                values: new object[] { 2, "Elegant and intelligent, the Poodle comes in various sizes and is known for its hypoallergenic coat." });

            migrationBuilder.UpdateData(
                table: "Dogs",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CategoryId", "Description" },
                values: new object[] { 1, "The Beagle is known for its keen sense of smell and tracking instinct, making it an excellent hunting dog." });

            migrationBuilder.UpdateData(
                table: "Dogs",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CategoryId", "Description" },
                values: new object[] { 3, "Known for its striking appearance and endurance, the Siberian Husky is a strong, energetic, and playful breed." });

            migrationBuilder.CreateIndex(
                name: "IX_Dogs_CategoryId",
                table: "Dogs",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dogs_Categories_CategoryId",
                table: "Dogs",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dogs_Categories_CategoryId",
                table: "Dogs");

            migrationBuilder.DropIndex(
                name: "IX_Dogs_CategoryId",
                table: "Dogs");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Dogs");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Havanese");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Husky");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Golden retreiver");

            migrationBuilder.UpdateData(
                table: "Dogs",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "Friendly and intelligent, the Golden Retriever is a popular family dog known for its gentle temperament and loyalty.\r\n\r\nIdeal for families and individuals alike, this breed requires regular exercise and grooming.");

            migrationBuilder.UpdateData(
                table: "Dogs",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "Highly intelligent and versatile, the German Shepherd is an excellent working dog and family companion. Known for its courage and loyalty.\r\n\r\nThis breed excels in various roles including police work and search and rescue.");

            migrationBuilder.UpdateData(
                table: "Dogs",
                keyColumn: "Id",
                keyValue: 3,
                column: "Description",
                value: "With its distinctive wrinkled face and muscular build, the Bulldog is known for its friendly and courageous nature.\r\n\r\nA great companion for families, it requires minimal exercise but benefits from regular health check-ups.");

            migrationBuilder.UpdateData(
                table: "Dogs",
                keyColumn: "Id",
                keyValue: 4,
                column: "Description",
                value: "Elegant and intelligent, the Poodle comes in various sizes and is known for its hypoallergenic coat. It’s a versatile breed that excels in obedience and agility.\r\n\r\nRequires regular grooming and enjoys being an active family member.");

            migrationBuilder.UpdateData(
                table: "Dogs",
                keyColumn: "Id",
                keyValue: 5,
                column: "Description",
                value: "The Beagle is a small to medium-sized dog with a keen sense of smell and tracking instinct. It is known for its friendly and curious nature.\r\n\r\nIdeal for families, it needs regular exercise and enjoys outdoor activities.");

            migrationBuilder.UpdateData(
                table: "Dogs",
                keyColumn: "Id",
                keyValue: 6,
                column: "Description",
                value: "Known for its striking appearance and endurance, the Siberian Husky is a strong, energetic, and playful breed. It thrives in colder climates and requires regular exercise and mental stimulation.");
        }
    }
}
