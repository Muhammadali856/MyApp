using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyApp.Server.Migrations
{
    /// <inheritdoc />
    public partial class SeedVillaTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Dachas",
                columns: new[] { "Id", "Amenity", "CreatedDate", "Details", "ImageURL", "IsAvailable", "Name", "Rate", "Sqft", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, "Wi-Fi, Pool, Sauna", new DateTime(2025, 7, 14, 20, 29, 52, 3, DateTimeKind.Local).AddTicks(7624), "A beautiful villa with a sunset view.", "", "No", "Sunset Villa", 7.5, 2000, new DateTime(2025, 7, 14, 20, 29, 52, 3, DateTimeKind.Local).AddTicks(7635) },
                    { 2, "Wi-Fi, Fireplace, Hiking Trails", new DateTime(2025, 7, 14, 20, 29, 52, 3, DateTimeKind.Local).AddTicks(7637), "Cozy retreat in the mountains.", "", "Yes", "Mountain Retreat", 6.2000000000000002, 1800, new DateTime(2025, 7, 14, 20, 29, 52, 3, DateTimeKind.Local).AddTicks(7638) },
                    { 3, "Wi-Fi, Boat Dock, BBQ", new DateTime(2025, 7, 14, 20, 29, 52, 3, DateTimeKind.Local).AddTicks(7639), "Relaxing house by the lake.", "", "Yes", "Lake House", 8.0, 2200, new DateTime(2025, 7, 14, 20, 29, 52, 3, DateTimeKind.Local).AddTicks(7640) },
                    { 4, "Fireplace, Hiking Trails, Pet Friendly", new DateTime(2025, 7, 14, 20, 29, 52, 3, DateTimeKind.Local).AddTicks(7641), "Secluded cabin in the forest.", "", "No", "Forest Cabin", 4.5, 1200, new DateTime(2025, 7, 14, 20, 29, 52, 3, DateTimeKind.Local).AddTicks(7641) },
                    { 5, "Wi-Fi, Rooftop Access, Gym", new DateTime(2025, 7, 14, 20, 29, 52, 3, DateTimeKind.Local).AddTicks(7643), "Modern loft in the city center.", "", "Yes", "City Loft", 9.0, 950, new DateTime(2025, 7, 14, 20, 29, 52, 3, DateTimeKind.Local).AddTicks(7643) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Dachas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Dachas",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Dachas",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Dachas",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Dachas",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
