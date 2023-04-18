using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_Identity.Migrations
{
    /// <inheritdoc />
    public partial class Addclassleveltousser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SelectedSubject",
                table: "Students",
                newName: "ClassLevel");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ClassLevel",
                table: "Students",
                newName: "SelectedSubject");
        }
    }
}
