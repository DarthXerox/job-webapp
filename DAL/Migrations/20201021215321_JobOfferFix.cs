using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class JobOfferFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_JobOffers_Companies_CompanyId1",
                "JobOffers");

            migrationBuilder.DropIndex(
                "IX_JobOffers_CompanyId",
                "JobOffers");

            migrationBuilder.DropIndex(
                "IX_JobOffers_CompanyId1",
                "JobOffers");

            migrationBuilder.DropColumn(
                "CompanyId1",
                "JobOffers");

            migrationBuilder.InsertData(
                "Companies",
                new[] { "Id", "Name" },
                new object[] { 1, "Apple" });

            migrationBuilder.CreateIndex(
                "IX_JobOffers_CompanyId",
                "JobOffers",
                "CompanyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                "IX_JobOffers_CompanyId",
                "JobOffers");

            migrationBuilder.DeleteData(
                "Companies",
                "Id",
                1);

            migrationBuilder.AddColumn<int>(
                "CompanyId1",
                "JobOffers",
                "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                "IX_JobOffers_CompanyId",
                "JobOffers",
                "CompanyId",
                unique: true);

            migrationBuilder.CreateIndex(
                "IX_JobOffers_CompanyId1",
                "JobOffers",
                "CompanyId1");

            migrationBuilder.AddForeignKey(
                "FK_JobOffers_Companies_CompanyId1",
                "JobOffers",
                "CompanyId1",
                "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
