using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CabTap.Data.Migrations
{
    public partial class ReservationDistance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Distance",
                table: "Reservations",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Distance",
                table: "Reservations");
        }
    }
}
