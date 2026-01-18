using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MenuMaster.Migrations
{
    /// <inheritdoc />
    public partial class UpdateLocationToAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Location",
                table: "Restaurants",
                newName: "Address");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Restaurants",
                newName: "Location");
        }
    }
}
