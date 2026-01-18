using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MenuMaster.Migrations
{
    /// <inheritdoc />
    public partial class tokenChangeToCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PasswordResetTokenUsedAt",
                table: "Users",
                newName: "PasswordResetCodeUsedAt");

            migrationBuilder.RenameColumn(
                name: "PasswordResetTokenHash",
                table: "Users",
                newName: "PasswordResetCodeHash");

            migrationBuilder.RenameColumn(
                name: "PasswordResetTokenExpiresAt",
                table: "Users",
                newName: "PasswordResetCodeExpiresAt");

            migrationBuilder.RenameColumn(
                name: "PasswordResetTokenUsedAt",
                table: "Restaurants",
                newName: "PasswordResetCodeUsedAt");

            migrationBuilder.RenameColumn(
                name: "PasswordResetTokenHash",
                table: "Restaurants",
                newName: "PasswordResetCodeHash");

            migrationBuilder.RenameColumn(
                name: "PasswordResetTokenExpiresAt",
                table: "Restaurants",
                newName: "PasswordResetCodeExpiresAt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PasswordResetCodeUsedAt",
                table: "Users",
                newName: "PasswordResetTokenUsedAt");

            migrationBuilder.RenameColumn(
                name: "PasswordResetCodeHash",
                table: "Users",
                newName: "PasswordResetTokenHash");

            migrationBuilder.RenameColumn(
                name: "PasswordResetCodeExpiresAt",
                table: "Users",
                newName: "PasswordResetTokenExpiresAt");

            migrationBuilder.RenameColumn(
                name: "PasswordResetCodeUsedAt",
                table: "Restaurants",
                newName: "PasswordResetTokenUsedAt");

            migrationBuilder.RenameColumn(
                name: "PasswordResetCodeHash",
                table: "Restaurants",
                newName: "PasswordResetTokenHash");

            migrationBuilder.RenameColumn(
                name: "PasswordResetCodeExpiresAt",
                table: "Restaurants",
                newName: "PasswordResetTokenExpiresAt");
        }
    }
}
