using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PCONTB.Panel.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RefactorFileToVideoAndImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Project_ProjectImageFile_ImageId",
                table: "Project");

            migrationBuilder.DropForeignKey(
                name: "FK_Project_ProjectVideoFile_VideoId",
                table: "Project");

            migrationBuilder.DropTable(
                name: "ProjectImageFile");

            migrationBuilder.DropTable(
                name: "ProjectVideoFile");

            migrationBuilder.CreateTable(
                name: "ProjectImage",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Data = table.Column<byte[]>(type: "bytea", nullable: false),
                    FileName = table.Column<string>(type: "text", nullable: false),
                    ContentType = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectImage", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProjectVideo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Data = table.Column<byte[]>(type: "bytea", nullable: false),
                    FileName = table.Column<string>(type: "text", nullable: false),
                    ContentType = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectVideo", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Project_ProjectImage_ImageId",
                table: "Project",
                column: "ImageId",
                principalTable: "ProjectImage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Project_ProjectVideo_VideoId",
                table: "Project",
                column: "VideoId",
                principalTable: "ProjectVideo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Project_ProjectImage_ImageId",
                table: "Project");

            migrationBuilder.DropForeignKey(
                name: "FK_Project_ProjectVideo_VideoId",
                table: "Project");

            migrationBuilder.DropTable(
                name: "ProjectImage");

            migrationBuilder.DropTable(
                name: "ProjectVideo");

            migrationBuilder.CreateTable(
                name: "ProjectImageFile",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ContentType = table.Column<string>(type: "text", nullable: false),
                    Data = table.Column<byte[]>(type: "bytea", nullable: false),
                    FileName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectImageFile", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProjectVideoFile",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ContentType = table.Column<string>(type: "text", nullable: false),
                    Data = table.Column<byte[]>(type: "bytea", nullable: false),
                    FileName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectVideoFile", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Project_ProjectImageFile_ImageId",
                table: "Project",
                column: "ImageId",
                principalTable: "ProjectImageFile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Project_ProjectVideoFile_VideoId",
                table: "Project",
                column: "VideoId",
                principalTable: "ProjectVideoFile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
