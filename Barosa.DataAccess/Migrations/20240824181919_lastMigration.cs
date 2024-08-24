using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplicationBarosa.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class lastMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApplicationUserId",
                table: "OrderDetails",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ApplicationUserId",
                table: "OrderDetails",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_OrderHeaders_ApplicationUserId",
                table: "OrderDetails",
                column: "ApplicationUserId",
                principalTable: "OrderHeaders",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_OrderHeaders_ApplicationUserId",
                table: "OrderDetails");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetails_ApplicationUserId",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "OrderDetails");
        }
    }
}
