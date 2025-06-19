using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FileGrid.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDrillHolePrecision : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "MeasuredCollarY",
                table: "DrillHoles",
                type: "float",
                nullable: true,
                comment: "实际孔口坐标北",
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true,
                oldComment: "实际孔口坐标北");

            migrationBuilder.AlterColumn<double>(
                name: "MeasuredCollarX",
                table: "DrillHoles",
                type: "float",
                nullable: true,
                comment: "实际孔口坐标东",
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true,
                oldComment: "实际孔口坐标东");

            migrationBuilder.AlterColumn<double>(
                name: "MeasuredCollarElevation",
                table: "DrillHoles",
                type: "float",
                nullable: true,
                comment: "实际孔口标高",
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true,
                oldComment: "实际孔口标高");

            migrationBuilder.AlterColumn<double>(
                name: "DesignedCollarY",
                table: "DrillHoles",
                type: "float",
                nullable: true,
                comment: "设计孔口坐标北",
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true,
                oldComment: "设计孔口坐标北");

            migrationBuilder.AlterColumn<double>(
                name: "DesignedCollarX",
                table: "DrillHoles",
                type: "float",
                nullable: true,
                comment: "设计孔口坐标东",
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true,
                oldComment: "设计孔口坐标东");

            migrationBuilder.AlterColumn<double>(
                name: "DesignedCollarElevation",
                table: "DrillHoles",
                type: "float",
                nullable: true,
                comment: "设计孔口标高",
                oldClrType: typeof(float),
                oldType: "real",
                oldNullable: true,
                oldComment: "设计孔口标高");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "MeasuredCollarY",
                table: "DrillHoles",
                type: "real",
                nullable: true,
                comment: "实际孔口坐标北",
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true,
                oldComment: "实际孔口坐标北");

            migrationBuilder.AlterColumn<float>(
                name: "MeasuredCollarX",
                table: "DrillHoles",
                type: "real",
                nullable: true,
                comment: "实际孔口坐标东",
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true,
                oldComment: "实际孔口坐标东");

            migrationBuilder.AlterColumn<float>(
                name: "MeasuredCollarElevation",
                table: "DrillHoles",
                type: "real",
                nullable: true,
                comment: "实际孔口标高",
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true,
                oldComment: "实际孔口标高");

            migrationBuilder.AlterColumn<float>(
                name: "DesignedCollarY",
                table: "DrillHoles",
                type: "real",
                nullable: true,
                comment: "设计孔口坐标北",
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true,
                oldComment: "设计孔口坐标北");

            migrationBuilder.AlterColumn<float>(
                name: "DesignedCollarX",
                table: "DrillHoles",
                type: "real",
                nullable: true,
                comment: "设计孔口坐标东",
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true,
                oldComment: "设计孔口坐标东");

            migrationBuilder.AlterColumn<float>(
                name: "DesignedCollarElevation",
                table: "DrillHoles",
                type: "real",
                nullable: true,
                comment: "设计孔口标高",
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true,
                oldComment: "设计孔口标高");
        }
    }
}
