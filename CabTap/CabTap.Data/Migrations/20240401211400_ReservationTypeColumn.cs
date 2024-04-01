using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CabTap.Data.Migrations
{
    public partial class ReservationTypeColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReservationType",
                table: "Reservations",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReservationType",
                table: "Reservations");
        }
    }
}
