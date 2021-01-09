using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class RelationshipFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_JobApplications_ApplicantId",
                table: "JobApplications");

            migrationBuilder.DropIndex(
                name: "IX_JobApplications_JobOfferId",
                table: "JobApplications");

            migrationBuilder.DropIndex(
                name: "IX_JobApplicationAnswer_QuestionId",
                table: "JobApplicationAnswer");

            migrationBuilder.CreateIndex(
                name: "IX_JobApplications_ApplicantId",
                table: "JobApplications",
                column: "ApplicantId");

            migrationBuilder.CreateIndex(
                name: "IX_JobApplications_JobOfferId",
                table: "JobApplications",
                column: "JobOfferId");

            migrationBuilder.CreateIndex(
                name: "IX_JobApplicationAnswer_QuestionId",
                table: "JobApplicationAnswer",
                column: "QuestionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_JobApplications_ApplicantId",
                table: "JobApplications");

            migrationBuilder.DropIndex(
                name: "IX_JobApplications_JobOfferId",
                table: "JobApplications");

            migrationBuilder.DropIndex(
                name: "IX_JobApplicationAnswer_QuestionId",
                table: "JobApplicationAnswer");

            migrationBuilder.CreateIndex(
                name: "IX_JobApplications_ApplicantId",
                table: "JobApplications",
                column: "ApplicantId",
                unique: true,
                filter: "[ApplicantId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_JobApplications_JobOfferId",
                table: "JobApplications",
                column: "JobOfferId",
                unique: true,
                filter: "[JobOfferId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_JobApplicationAnswer_QuestionId",
                table: "JobApplicationAnswer",
                column: "QuestionId",
                unique: true,
                filter: "[QuestionId] IS NOT NULL");
        }
    }
}
