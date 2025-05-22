using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CabTap.Data.Migrations
{
    public partial class AddSpatialSupport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");
            
            migrationBuilder.AddColumn<Point>(
                name: "Location",
                table: "Reservations",
                type: "geometry(Point,4326)",
                nullable: false,
                defaultValueSql: "ST_GeomFromText('POINT(0 0)', 4326)"
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "Reservations");
        }
    }
}
