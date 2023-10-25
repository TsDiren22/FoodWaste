using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodWaste.Infrastructure.Migrations
{
    public partial class CreationUpdatedDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PictureType",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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
                columns: new[] { "PickupDate", "PickupExpiry" },
                values: new object[] { null, new DateTime(2023, 10, 26, 10, 22, 1, 329, DateTimeKind.Local) });

            migrationBuilder.UpdateData(
                table: "Packages",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "PickupDate", "PickupExpiry" },
                values: new object[] { null, new DateTime(2023, 10, 26, 10, 22, 1, 329, DateTimeKind.Local).AddTicks(3) });

            migrationBuilder.UpdateData(
                table: "Packages",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "PickupDate", "PickupExpiry" },
                values: new object[] { null, new DateTime(2023, 10, 26, 10, 22, 1, 329, DateTimeKind.Local).AddTicks(6) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "PictureType",
                value: "image/jpeg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "PictureType",
                value: "image/jpeg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "PictureType",
                value: "image/jpeg");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PictureType",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "Packages",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PickupDate", "PickupExpiry" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Packages",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "PickupDate", "PickupExpiry" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Packages",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "PickupDate", "PickupExpiry" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Packages",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "PickupDate", "PickupExpiry" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Packages",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "PickupDate", "PickupExpiry" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Packages",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "PickupDate", "PickupExpiry" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }
    }
}
