using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Graduation.BrainwaveSystem.Models.Migrations
{
    public partial class AddIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Devices",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Devices",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_Description",
                table: "Devices",
                column: "Description");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_Name",
                table: "Devices",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_UserId",
                table: "Devices",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceDatas_CreatedTime",
                table: "DeviceDatas",
                column: "CreatedTime");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceDatas_DeviceId",
                table: "DeviceDatas",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_DataRawEEGs_CreatedTime",
                table: "DataRawEEGs",
                column: "CreatedTime");

            migrationBuilder.CreateIndex(
                name: "IX_Data8BandsEEGs_CreatedTime",
                table: "Data8BandsEEGs",
                column: "CreatedTime");

            migrationBuilder.CreateIndex(
                name: "IX_Data8BandsEEGs_DeviceDataId",
                table: "Data8BandsEEGs",
                column: "DeviceDataId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Devices_Description",
                table: "Devices");

            migrationBuilder.DropIndex(
                name: "IX_Devices_Name",
                table: "Devices");

            migrationBuilder.DropIndex(
                name: "IX_Devices_UserId",
                table: "Devices");

            migrationBuilder.DropIndex(
                name: "IX_DeviceDatas_CreatedTime",
                table: "DeviceDatas");

            migrationBuilder.DropIndex(
                name: "IX_DeviceDatas_DeviceId",
                table: "DeviceDatas");

            migrationBuilder.DropIndex(
                name: "IX_DataRawEEGs_CreatedTime",
                table: "DataRawEEGs");

            migrationBuilder.DropIndex(
                name: "IX_Data8BandsEEGs_CreatedTime",
                table: "Data8BandsEEGs");

            migrationBuilder.DropIndex(
                name: "IX_Data8BandsEEGs_DeviceDataId",
                table: "Data8BandsEEGs");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Devices",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Devices",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
