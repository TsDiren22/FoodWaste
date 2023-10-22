using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodWaste.Infrastructure.Migrations
{
    public partial class CreateWithNullableStudentInPackage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Packages_Students_StudentId",
                table: "Packages");

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "Packages",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PickupDate",
                table: "Packages",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.InsertData(
                table: "Packages",
                columns: new[] { "Id", "CafeteriaId", "ContainsAdultProducts", "MealType", "Name", "PickupDate", "PickupExpiry", "Price", "StudentId" },
                values: new object[] { 4, 1, true, "Cold", "Test1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 10.0m, null });

            migrationBuilder.InsertData(
                table: "Packages",
                columns: new[] { "Id", "CafeteriaId", "ContainsAdultProducts", "MealType", "Name", "PickupDate", "PickupExpiry", "Price", "StudentId" },
                values: new object[] { 5, 1, true, "Cold", "Test2", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 10.0m, null });

            migrationBuilder.InsertData(
                table: "Packages",
                columns: new[] { "Id", "CafeteriaId", "ContainsAdultProducts", "MealType", "Name", "PickupDate", "PickupExpiry", "Price", "StudentId" },
                values: new object[] { 6, 1, true, "Cold", "Test3", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 10.0m, null });

            migrationBuilder.AddForeignKey(
                name: "FK_Packages_Students_StudentId",
                table: "Packages",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Packages_Students_StudentId",
                table: "Packages");

            migrationBuilder.DeleteData(
                table: "Packages",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Packages",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Packages",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "Packages",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "PickupDate",
                table: "Packages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Packages_Students_StudentId",
                table: "Packages",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
