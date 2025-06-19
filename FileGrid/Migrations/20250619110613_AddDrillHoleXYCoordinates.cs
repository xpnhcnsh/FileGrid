using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FileGrid.Migrations
{
    /// <inheritdoc />
    public partial class AddDrillHoleXYCoordinates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "DrillHoles",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "CoordinateSystem",
                table: "DrillHoles",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "坐标系类型");

            migrationBuilder.AddColumn<float>(
                name: "DesignedCollarElevation",
                table: "DrillHoles",
                type: "real",
                nullable: true,
                comment: "设计孔口标高");

            migrationBuilder.AddColumn<float>(
                name: "DesignedCollarX",
                table: "DrillHoles",
                type: "real",
                nullable: true,
                comment: "设计孔口坐标东");

            migrationBuilder.AddColumn<float>(
                name: "DesignedCollarY",
                table: "DrillHoles",
                type: "real",
                nullable: true,
                comment: "设计孔口坐标北");

            migrationBuilder.AddColumn<float>(
                name: "MeasuredCollarElevation",
                table: "DrillHoles",
                type: "real",
                nullable: true,
                comment: "实际孔口标高");

            migrationBuilder.AddColumn<float>(
                name: "MeasuredCollarX",
                table: "DrillHoles",
                type: "real",
                nullable: true,
                comment: "实际孔口坐标东");

            migrationBuilder.AddColumn<float>(
                name: "MeasuredCollarY",
                table: "DrillHoles",
                type: "real",
                nullable: true,
                comment: "实际孔口坐标北");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoordinateSystem",
                table: "DrillHoles");

            migrationBuilder.DropColumn(
                name: "DesignedCollarElevation",
                table: "DrillHoles");

            migrationBuilder.DropColumn(
                name: "DesignedCollarX",
                table: "DrillHoles");

            migrationBuilder.DropColumn(
                name: "DesignedCollarY",
                table: "DrillHoles");

            migrationBuilder.DropColumn(
                name: "MeasuredCollarElevation",
                table: "DrillHoles");

            migrationBuilder.DropColumn(
                name: "MeasuredCollarX",
                table: "DrillHoles");

            migrationBuilder.DropColumn(
                name: "MeasuredCollarY",
                table: "DrillHoles");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "DrillHoles",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);
        }
    }
}
