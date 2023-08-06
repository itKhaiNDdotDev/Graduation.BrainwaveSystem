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
                    Delta = table.Column<int>(type: "int", nullable: true),
                    Theta = table.Column<int>(type: "int", nullable: true),
                    Alpha = table.Column<int>(type: "int", nullable: true),
                    LowBeta = table.Column<int>(type: "int", nullable: true),
                    MidBeta = table.Column<int>(type: "int", nullable: true),
                    HighBeta = table.Column<int>(type: "int", nullable: true),
                    Gamma = table.Column<int>(type: "int", nullable: true),
                    UHFGamma = table.Column<int>(type: "int", nullable: true),
                    DeviceDataId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
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
                    DeviceDataId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    PoorQuality = table.Column<int>(type: "int", nullable: true),
                    Attention = table.Column<int>(type: "int", nullable: true),
                    Meditation = table.Column<int>(type: "int", nullable: true),
                    DeviceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
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
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ActiveTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.Id);
                });

            migrationBuilder.CreateTable(
               name: "UserLogins",
               columns: table => new
               {
                   Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                   Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                   Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                   UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                   PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                   PasswordSalt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                   PhotoFileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                   Role = table.Column<int>(type: "int", nullable: true),
                   IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                   CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                   CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                   LastModifiedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                   LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_UserLogins", x => x.Id);
               });

            migrationBuilder.CreateTable(
               name: "Roles",
               columns: table => new
               {
                   Id = table.Column<int>(type: "int", nullable: false),
                   Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_Roles", x => x.Id);
               });

            migrationBuilder.CreateTable(
               name: "UserRoles",
               columns: table => new
               {
                   Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                   RoleId = table.Column<int>(type: "int", nullable: false),
                   UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_UserRoles", x => x.Id);
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

            migrationBuilder.DropTable(
                name: "UserLogins");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "UserRoles");
        }
    }
}
