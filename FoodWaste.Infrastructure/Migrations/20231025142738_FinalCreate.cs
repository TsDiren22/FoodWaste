using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodWaste.Infrastructure.Migrations
{
    public partial class FinalCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Packages",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PickupDate", "PickupExpiry" },
                values: new object[] { new DateTime(2023, 10, 26, 16, 27, 37, 615, DateTimeKind.Local).AddTicks(250), new DateTime(2023, 10, 27, 16, 27, 37, 615, DateTimeKind.Local).AddTicks(294) });

            migrationBuilder.UpdateData(
                table: "Packages",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "PickupDate", "PickupExpiry" },
                values: new object[] { new DateTime(2023, 10, 26, 16, 27, 37, 615, DateTimeKind.Local).AddTicks(306), new DateTime(2023, 10, 27, 16, 27, 37, 615, DateTimeKind.Local).AddTicks(313) });

            migrationBuilder.UpdateData(
                table: "Packages",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "PickupDate", "PickupExpiry" },
                values: new object[] { new DateTime(2023, 10, 26, 16, 27, 37, 615, DateTimeKind.Local).AddTicks(324), new DateTime(2023, 10, 27, 16, 27, 37, 615, DateTimeKind.Local).AddTicks(331) });

            migrationBuilder.UpdateData(
                table: "Packages",
                keyColumn: "Id",
                keyValue: 4,
                column: "PickupExpiry",
                value: new DateTime(2023, 10, 27, 16, 27, 37, 615, DateTimeKind.Local).AddTicks(340));

            migrationBuilder.UpdateData(
                table: "Packages",
                keyColumn: "Id",
                keyValue: 5,
                column: "PickupExpiry",
                value: new DateTime(2023, 10, 27, 16, 27, 37, 615, DateTimeKind.Local).AddTicks(349));

            migrationBuilder.UpdateData(
                table: "Packages",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CafeteriaId", "MealType", "PickupExpiry" },
                values: new object[] { 2, "Warm", new DateTime(2023, 10, 27, 16, 27, 37, 615, DateTimeKind.Local).AddTicks(358) });

            migrationBuilder.InsertData(
                table: "Packages",
                columns: new[] { "Id", "CafeteriaId", "ContainsAdultProducts", "MealType", "Name", "PickupDate", "PickupExpiry", "Price", "StudentId" },
                values: new object[,]
                {
                    { 7, 3, true, "Warm", "Test4", null, new DateTime(2023, 10, 27, 16, 27, 37, 615, DateTimeKind.Local).AddTicks(367), 10.0m, null },
                    { 8, 1, true, "Test", "Test5", null, new DateTime(2023, 10, 27, 16, 27, 37, 615, DateTimeKind.Local).AddTicks(375), 10.0m, null },
                    { 9, 2, true, "Something", "Test6", null, new DateTime(2023, 10, 27, 16, 27, 37, 615, DateTimeKind.Local).AddTicks(385), 10.0m, null },
                    { 10, 2, true, "Test", "Test7", null, new DateTime(2023, 10, 27, 16, 27, 37, 615, DateTimeKind.Local).AddTicks(396), 10.0m, null },
                    { 11, 3, true, "Testtttt", "Test8", null, new DateTime(2023, 10, 27, 16, 27, 37, 615, DateTimeKind.Local).AddTicks(405), 10.0m, null },
                    { 12, 3, true, "TESTTTTT", "Test9", null, new DateTime(2023, 10, 27, 16, 27, 37, 615, DateTimeKind.Local).AddTicks(414), 10.0m, null },
                    { 13, 1, true, "warm", "Test10", null, new DateTime(2023, 10, 27, 16, 27, 37, 615, DateTimeKind.Local).AddTicks(422), 10.0m, null },
                    { 14, 3, true, "cold", "Test11", null, new DateTime(2023, 10, 27, 16, 27, 37, 615, DateTimeKind.Local).AddTicks(431), 10.0m, null },
                    { 15, 1, true, "COLD", "Test12", null, new DateTime(2023, 10, 27, 16, 27, 37, 615, DateTimeKind.Local).AddTicks(440), 10.0m, null },
                    { 16, 2, true, "WARM", "Test13", null, new DateTime(2023, 10, 27, 16, 27, 37, 615, DateTimeKind.Local).AddTicks(448), 10.0m, null },
                    { 17, 2, true, "SUPER WARM", "Test14", null, new DateTime(2023, 10, 27, 16, 27, 37, 615, DateTimeKind.Local).AddTicks(457), 10.0m, null },
                    { 18, 3, true, "SOME", "Test15", null, new DateTime(2023, 10, 27, 16, 27, 37, 615, DateTimeKind.Local).AddTicks(466), 10.0m, null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Packages",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Packages",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Packages",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Packages",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Packages",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Packages",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Packages",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Packages",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Packages",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Packages",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Packages",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Packages",
                keyColumn: "Id",
                keyValue: 18);

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
                columns: new[] { "CafeteriaId", "MealType", "PickupExpiry" },
                values: new object[] { 1, "Cold", new DateTime(2023, 10, 26, 11, 26, 8, 314, DateTimeKind.Local).AddTicks(4678) });
        }
    }
}
