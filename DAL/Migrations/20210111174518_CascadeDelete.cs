using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class CascadeDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobOfferQuestion_JobOffers_JobOfferId",
                table: "JobOfferQuestion");

            migrationBuilder.DropForeignKey(
                name: "FK_JobOffers_Companies_CompanyId",
                table: "JobOffers");

            migrationBuilder.AddForeignKey(
                name: "FK_JobOfferQuestion_JobOffers_JobOfferId",
                table: "JobOfferQuestion",
                column: "JobOfferId",
                principalTable: "JobOffers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobOffers_Companies_CompanyId",
                table: "JobOffers",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobOfferQuestion_JobOffers_JobOfferId",
                table: "JobOfferQuestion");

            migrationBuilder.DropForeignKey(
                name: "FK_JobOffers_Companies_CompanyId",
                table: "JobOffers");

            migrationBuilder.AddForeignKey(
                name: "FK_JobOfferQuestion_JobOffers_JobOfferId",
                table: "JobOfferQuestion",
                column: "JobOfferId",
                principalTable: "JobOffers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_JobOffers_Companies_CompanyId",
                table: "JobOffers",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
