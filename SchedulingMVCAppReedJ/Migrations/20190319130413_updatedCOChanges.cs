using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SchedulingMVCAppReedJ.Migrations
{
    public partial class updatedCOChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ChangerName",
                table: "CoursesOfferingsChanges",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChangerName",
                table: "CoursesOfferingsChanges");
        }
    }
}
