using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SchedulingMVCAppReedJ.Migrations
{
    public partial class AddedCourseOfferingChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CoursesOfferingsChanges",
                columns: table => new
                {
                    CourseOfferingChangeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CRN = table.Column<string>(nullable: true),
                    ChangeType = table.Column<string>(nullable: true),
                    ChangerRole = table.Column<string>(nullable: true),
                    CourseID = table.Column<int>(nullable: false),
                    DateAndTimeOfChange = table.Column<DateTime>(nullable: false),
                    Days = table.Column<string>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: false),
                    EndTime = table.Column<DateTime>(nullable: false),
                    InstructorID = table.Column<int>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    StartTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoursesOfferingsChanges", x => x.CourseOfferingChangeID);
                    table.ForeignKey(
                        name: "FK_CoursesOfferingsChanges_Courses_CourseID",
                        column: x => x.CourseID,
                        principalTable: "Courses",
                        principalColumn: "CourseID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CoursesOfferingsChanges_Instructors_InstructorID",
                        column: x => x.InstructorID,
                        principalTable: "Instructors",
                        principalColumn: "InstructorID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CoursesOfferingsChanges_CourseID",
                table: "CoursesOfferingsChanges",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_CoursesOfferingsChanges_InstructorID",
                table: "CoursesOfferingsChanges",
                column: "InstructorID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoursesOfferingsChanges");
        }
    }
}
