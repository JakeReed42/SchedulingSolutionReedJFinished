using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SchedulingMVCAppReedJ.Migrations
{
    public partial class addedTeachingQualification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TeachingQualifications",
                columns: table => new
                {
                    TeachingQualificationID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CourseID = table.Column<int>(nullable: false),
                    InstructorID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeachingQualifications", x => x.TeachingQualificationID);
                    table.ForeignKey(
                        name: "FK_TeachingQualifications_Courses_CourseID",
                        column: x => x.CourseID,
                        principalTable: "Courses",
                        principalColumn: "CourseID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TeachingQualifications_Instructors_InstructorID",
                        column: x => x.InstructorID,
                        principalTable: "Instructors",
                        principalColumn: "InstructorID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeachingQualifications_CourseID",
                table: "TeachingQualifications",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_TeachingQualifications_InstructorID",
                table: "TeachingQualifications",
                column: "InstructorID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeachingQualifications");
        }
    }
}
