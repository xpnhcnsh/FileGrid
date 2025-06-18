using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FileGrid.Migrations
{
    /// <inheritdoc />
    public partial class DeleteConditionFile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConditionFile");

            migrationBuilder.AddColumn<Guid>(
                name: "CompletionConditionId",
                table: "Resources",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Resources_CompletionConditionId",
                table: "Resources",
                column: "CompletionConditionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Resources_CompletionCondition_CompletionConditionId",
                table: "Resources",
                column: "CompletionConditionId",
                principalTable: "CompletionCondition",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Resources_CompletionCondition_CompletionConditionId",
                table: "Resources");

            migrationBuilder.DropIndex(
                name: "IX_Resources_CompletionConditionId",
                table: "Resources");

            migrationBuilder.DropColumn(
                name: "CompletionConditionId",
                table: "Resources");

            migrationBuilder.CreateTable(
                name: "ConditionFile",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UploadedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApprovedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CompletionConditionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    UploadedTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConditionFile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConditionFile_CompletionCondition_CompletionConditionId",
                        column: x => x.CompletionConditionId,
                        principalTable: "CompletionCondition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConditionFile_Users_UploadedUserId",
                        column: x => x.UploadedUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConditionFile_CompletionConditionId",
                table: "ConditionFile",
                column: "CompletionConditionId");

            migrationBuilder.CreateIndex(
                name: "IX_ConditionFile_UploadedUserId",
                table: "ConditionFile",
                column: "UploadedUserId");
        }
    }
}
