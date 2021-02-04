using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class User : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "JobSeekers",
                keyColumn: "Id",
                keyValue: 1);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Apple" });

            migrationBuilder.InsertData(
                table: "JobSeekers",
                columns: new[] { "Id", "Email", "Name", "Skills", "Surname" },
                values: new object[] { 1, "uco@mail.muni.cz", "John", "C#;Python", "Wick" });
        }
    }
}
