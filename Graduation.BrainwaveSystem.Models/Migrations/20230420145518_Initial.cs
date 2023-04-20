using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Graduation.BrainwaveSystem.Models.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Data8BandsEEGs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Delta = table.Column<int>(type: "int", nullable: false),
                    Theta = table.Column<int>(type: "int", nullable: false),
                    Alpha = table.Column<int>(type: "int", nullable: false),
                    LowBeta = table.Column<int>(type: "int", nullable: false),
                    MidBeta = table.Column<int>(type: "int", nullable: false),
                    HighBeta = table.Column<int>(type: "int", nullable: false),
                    Gamma = table.Column<int>(type: "int", nullable: false),
                    UHFGamma = table.Column<int>(type: "int", nullable: false),
                    DeviceDataId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Data8BandsEEGs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DataRawEEGs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false),
                    DeviceDataId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataRawEEGs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeviceDatas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PoorQuality = table.Column<int>(type: "int", nullable: false),
                    Attention = table.Column<int>(type: "int", nullable: false),
                    Meditation = table.Column<int>(type: "int", nullable: false),
                    DeviceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceDatas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ActiveTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Data8BandsEEGs");

            migrationBuilder.DropTable(
                name: "DataRawEEGs");

            migrationBuilder.DropTable(
                name: "DeviceDatas");

            migrationBuilder.DropTable(
                name: "Devices");
        }
    }
}
