using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SchedulingMVCAppReedJ.Migrations
{
    public partial class updatedCourseOffering : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Capacity",
                table: "CourseOfferings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Enrollment",
                table: "CourseOfferings",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Capacity",
                table: "CourseOfferings");

            migrationBuilder.DropColumn(
                name: "Enrollment",
                table: "CourseOfferings");
        }
    }
}
