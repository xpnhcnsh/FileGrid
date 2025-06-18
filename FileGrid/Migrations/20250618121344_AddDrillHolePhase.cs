using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FileGrid.Migrations
{
    /// <inheritdoc />
    public partial class AddDrillHolePhase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DrillHoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    ParentDrillHoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    StartedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrillHoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DrillHoles_DrillHoles_ParentDrillHoleId",
                        column: x => x.ParentDrillHoleId,
                        principalTable: "DrillHoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DrillHoles_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DrillHoleOutsource",
                columns: table => new
                {
                    DrillHoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrillHoleOutsource", x => new { x.DrillHoleId, x.CompanyId });
                    table.ForeignKey(
                        name: "FK_DrillHoleOutsource_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DrillHoleOutsource_DrillHoles_DrillHoleId",
                        column: x => x.DrillHoleId,
                        principalTable: "DrillHoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Phases",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DrillHoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParentPhaseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    PhaseType = table.Column<int>(type: "int", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    CompletedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StartedTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Phases_DrillHoles_DrillHoleId",
                        column: x => x.DrillHoleId,
                        principalTable: "DrillHoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Phases_Phases_ParentPhaseId",
                        column: x => x.ParentPhaseId,
                        principalTable: "Phases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompletionCondition",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PhaseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConditionType = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsMet = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompletionCondition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompletionCondition_Phases_PhaseId",
                        column: x => x.PhaseId,
                        principalTable: "Phases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhaseParameter",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PhaseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhaseParameter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhaseParameter_Phases_PhaseId",
                        column: x => x.PhaseId,
                        principalTable: "Phases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConditionFile",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompletionConditionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    ApprovedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UploadedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UploadedUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                name: "IX_CompletionCondition_PhaseId",
                table: "CompletionCondition",
                column: "PhaseId");

            migrationBuilder.CreateIndex(
                name: "IX_ConditionFile_CompletionConditionId",
                table: "ConditionFile",
                column: "CompletionConditionId");

            migrationBuilder.CreateIndex(
                name: "IX_ConditionFile_UploadedUserId",
                table: "ConditionFile",
                column: "UploadedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_DrillHoleOutsource_CompanyId",
                table: "DrillHoleOutsource",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_DrillHoles_ParentDrillHoleId",
                table: "DrillHoles",
                column: "ParentDrillHoleId");

            migrationBuilder.CreateIndex(
                name: "IX_DrillHoles_ProjectId",
                table: "DrillHoles",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_PhaseParameter_PhaseId",
                table: "PhaseParameter",
                column: "PhaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Phases_DrillHoleId",
                table: "Phases",
                column: "DrillHoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Phases_ParentPhaseId",
                table: "Phases",
                column: "ParentPhaseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConditionFile");

            migrationBuilder.DropTable(
                name: "DrillHoleOutsource");

            migrationBuilder.DropTable(
                name: "PhaseParameter");

            migrationBuilder.DropTable(
                name: "CompletionCondition");

            migrationBuilder.DropTable(
                name: "Phases");

            migrationBuilder.DropTable(
                name: "DrillHoles");
        }
    }
}
