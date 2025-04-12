using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniversityHelper.CommunityService.Data.Provider.MsSql.Ef.Migrations
{
    /// <inheritdoc />
    public partial class RemoveLishniePolya : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Communities");

            migrationBuilder.DropColumn(
                name: "PointId",
                table: "Communities");

            migrationBuilder.DropColumn(
                name: "Text",
                table: "Communities");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AuthorId",
                table: "Communities",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PointId",
                table: "Communities",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "Communities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
