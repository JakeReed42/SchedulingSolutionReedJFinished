using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SchedulingMVCAppReedJ.Migrations
{
    public partial class madeInstructorNullableInCourseOffering : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseOfferings_Instructors_InstructorID",
                table: "CourseOfferings");

            migrationBuilder.AlterColumn<int>(
                name: "InstructorID",
                table: "CourseOfferings",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_CourseOfferings_Instructors_InstructorID",
                table: "CourseOfferings",
                column: "InstructorID",
                principalTable: "Instructors",
                principalColumn: "InstructorID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseOfferings_Instructors_InstructorID",
                table: "CourseOfferings");

            migrationBuilder.AlterColumn<int>(
                name: "InstructorID",
                table: "CourseOfferings",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseOfferings_Instructors_InstructorID",
                table: "CourseOfferings",
                column: "InstructorID",
                principalTable: "Instructors",
                principalColumn: "InstructorID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
