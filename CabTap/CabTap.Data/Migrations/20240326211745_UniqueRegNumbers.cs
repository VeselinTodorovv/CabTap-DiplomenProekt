using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CabTap.Data.Migrations
{
    public partial class UniqueRegNumbers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RegNumber",
                table: "Taxis",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Taxis_RegNumber",
                table: "Taxis",
                column: "RegNumber",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Taxis_RegNumber",
                table: "Taxis");

            migrationBuilder.AlterColumn<string>(
                name: "RegNumber",
                table: "Taxis",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
