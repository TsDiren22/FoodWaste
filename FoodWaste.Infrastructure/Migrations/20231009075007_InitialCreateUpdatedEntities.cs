using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodWaste.Infrastructure.Migrations
{
    public partial class InitialCreateUpdatedEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasWarmMeals",
                table: "Cafeterias",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Cafeterias",
                keyColumn: "Id",
                keyValue: 1,
                column: "HasWarmMeals",
                value: true);

            migrationBuilder.UpdateData(
                table: "Cafeterias",
                keyColumn: "Id",
                keyValue: 3,
                column: "HasWarmMeals",
                value: true);

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1,
                column: "Email",
                value: "abc@abc.com");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2,
                column: "Email",
                value: "bcd@bcd.com");

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3,
                column: "Email",
                value: "cde@cde.com");

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Email", "Name" },
                values: new object[] { "ghi@ghi.com", "Diren Öztürk" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "HasWarmMeals",
                table: "Cafeterias");

            migrationBuilder.UpdateData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Email", "Name" },
                values: new object[] { "diren_2001@hotmail.com", "John Doe" });
        }
    }
}
