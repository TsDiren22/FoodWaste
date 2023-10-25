using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodWaste.Infrastructure.Migrations
{
    public partial class NullablePictureFile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Packages",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PickupDate", "PickupExpiry" },
                values: new object[] { new DateTime(2023, 10, 25, 11, 26, 8, 314, DateTimeKind.Local).AddTicks(4586), new DateTime(2023, 10, 26, 11, 26, 8, 314, DateTimeKind.Local).AddTicks(4640) });

            migrationBuilder.UpdateData(
                table: "Packages",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "PickupDate", "PickupExpiry" },
                values: new object[] { new DateTime(2023, 10, 25, 11, 26, 8, 314, DateTimeKind.Local).AddTicks(4649), new DateTime(2023, 10, 26, 11, 26, 8, 314, DateTimeKind.Local).AddTicks(4653) });

            migrationBuilder.UpdateData(
                table: "Packages",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "PickupDate", "PickupExpiry" },
                values: new object[] { new DateTime(2023, 10, 25, 11, 26, 8, 314, DateTimeKind.Local).AddTicks(4659), new DateTime(2023, 10, 26, 11, 26, 8, 314, DateTimeKind.Local).AddTicks(4663) });

            migrationBuilder.UpdateData(
                table: "Packages",
                keyColumn: "Id",
                keyValue: 4,
                column: "PickupExpiry",
                value: new DateTime(2023, 10, 26, 11, 26, 8, 314, DateTimeKind.Local).AddTicks(4668));

            migrationBuilder.UpdateData(
                table: "Packages",
                keyColumn: "Id",
                keyValue: 5,
                column: "PickupExpiry",
                value: new DateTime(2023, 10, 26, 11, 26, 8, 314, DateTimeKind.Local).AddTicks(4673));

            migrationBuilder.UpdateData(
                table: "Packages",
                keyColumn: "Id",
                keyValue: 6,
                column: "PickupExpiry",
                value: new DateTime(2023, 10, 26, 11, 26, 8, 314, DateTimeKind.Local).AddTicks(4678));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Packages",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PickupDate", "PickupExpiry" },
                values: new object[] { new DateTime(2023, 10, 25, 10, 22, 1, 328, DateTimeKind.Local).AddTicks(9935), new DateTime(2023, 10, 26, 10, 22, 1, 328, DateTimeKind.Local).AddTicks(9979) });

            migrationBuilder.UpdateData(
                table: "Packages",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "PickupDate", "PickupExpiry" },
                values: new object[] { new DateTime(2023, 10, 25, 10, 22, 1, 328, DateTimeKind.Local).AddTicks(9988), new DateTime(2023, 10, 26, 10, 22, 1, 328, DateTimeKind.Local).AddTicks(9990) });

            migrationBuilder.UpdateData(
                table: "Packages",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "PickupDate", "PickupExpiry" },
                values: new object[] { new DateTime(2023, 10, 25, 10, 22, 1, 328, DateTimeKind.Local).AddTicks(9994), new DateTime(2023, 10, 26, 10, 22, 1, 328, DateTimeKind.Local).AddTicks(9996) });

            migrationBuilder.UpdateData(
                table: "Packages",
                keyColumn: "Id",
                keyValue: 4,
                column: "PickupExpiry",
                value: new DateTime(2023, 10, 26, 10, 22, 1, 329, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Packages",
                keyColumn: "Id",
                keyValue: 5,
                column: "PickupExpiry",
                value: new DateTime(2023, 10, 26, 10, 22, 1, 329, DateTimeKind.Local).AddTicks(3));

            migrationBuilder.UpdateData(
                table: "Packages",
                keyColumn: "Id",
                keyValue: 6,
                column: "PickupExpiry",
                value: new DateTime(2023, 10, 26, 10, 22, 1, 329, DateTimeKind.Local).AddTicks(6));
        }
    }
}
