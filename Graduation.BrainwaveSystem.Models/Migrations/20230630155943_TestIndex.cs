using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Graduation.BrainwaveSystem.Models.Migrations
{
    public partial class TestIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_DataRawEEGs_DeviceDataId",
                table: "DataRawEEGs",
                column: "DeviceDataId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DataRawEEGs_DeviceDataId",
                table: "DataRawEEGs");
        }
    }
}
