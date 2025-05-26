using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace CabTap.Data.Migrations
{
    public partial class LocationsData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
            name: "public");

            migrationBuilder.AddColumn<Point>(
                name: "DestinationPoint",
                table: "Reservations",
                type: "geometry (Point, 4326)",
                nullable: false,
                defaultValueSql: "ST_GeomFromText('POINT(0 0)', 4326)");

            migrationBuilder.AddColumn<Point>(
                name: "OriginPoint",
                schema: "public",
                table: "Reservations",
                type: "geometry (Point, 4326)",
                nullable: false,
                defaultValueSql: "ST_GeomFromText('POINT(0 0)', 4326)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DestinationPoint",
                schema: "public",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "OriginPoint",
                schema: "public",
                table: "Reservations");
        }
    }
}
