using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplicationBarosa.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class streeTnotDaddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StreedAddress",
                table: "AspNetUsers",
                newName: "StreetAddress");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StreetAddress",
                table: "AspNetUsers",
                newName: "StreedAddress");
        }
    }
}
