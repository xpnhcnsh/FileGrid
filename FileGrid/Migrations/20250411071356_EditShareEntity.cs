using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FileGrid.Migrations
{
    /// <inheritdoc />
    public partial class EditShareEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TargetUserIds",
                table: "Shares",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TargetUserIds",
                table: "Shares");
        }
    }
}
