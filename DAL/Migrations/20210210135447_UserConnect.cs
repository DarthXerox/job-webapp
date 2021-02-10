using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class UserConnect : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobApplicationAnswer_JobApplications_JobApplicationId",
                table: "JobApplicationAnswer");

            migrationBuilder.DropForeignKey(
                name: "FK_JobApplicationAnswer_JobOfferQuestion_QuestionId",
                table: "JobApplicationAnswer");

            migrationBuilder.DropForeignKey(
                name: "FK_JobApplications_JobSeekers_ApplicantId",
                table: "JobApplications");

            migrationBuilder.DropForeignKey(
                name: "FK_JobApplications_JobOffers_JobOfferId",
                table: "JobApplications");

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "JobSeekerId",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "JobSeekers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Companies",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobSeekers_UserId",
                table: "JobSeekers",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_UserId",
                table: "Companies",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_Users_UserId",
                table: "Companies",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_JobApplicationAnswer_JobApplications_JobApplicationId",
                table: "JobApplicationAnswer",
                column: "JobApplicationId",
                principalTable: "JobApplications",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_JobApplicationAnswer_JobOfferQuestion_QuestionId",
                table: "JobApplicationAnswer",
                column: "QuestionId",
                principalTable: "JobOfferQuestion",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_JobApplications_JobSeekers_ApplicantId",
                table: "JobApplications",
                column: "ApplicantId",
                principalTable: "JobSeekers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_JobApplications_JobOffers_JobOfferId",
                table: "JobApplications",
                column: "JobOfferId",
                principalTable: "JobOffers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_JobSeekers_Users_UserId",
                table: "JobSeekers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_Users_UserId",
                table: "Companies");

            migrationBuilder.DropForeignKey(
                name: "FK_JobApplicationAnswer_JobApplications_JobApplicationId",
                table: "JobApplicationAnswer");

            migrationBuilder.DropForeignKey(
                name: "FK_JobApplicationAnswer_JobOfferQuestion_QuestionId",
                table: "JobApplicationAnswer");

            migrationBuilder.DropForeignKey(
                name: "FK_JobApplications_JobSeekers_ApplicantId",
                table: "JobApplications");

            migrationBuilder.DropForeignKey(
                name: "FK_JobApplications_JobOffers_JobOfferId",
                table: "JobApplications");

            migrationBuilder.DropForeignKey(
                name: "FK_JobSeekers_Users_UserId",
                table: "JobSeekers");

            migrationBuilder.DropIndex(
                name: "IX_JobSeekers_UserId",
                table: "JobSeekers");

            migrationBuilder.DropIndex(
                name: "IX_Companies_UserId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "JobSeekerId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "JobSeekers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Companies");

            migrationBuilder.AddForeignKey(
                name: "FK_JobApplicationAnswer_JobApplications_JobApplicationId",
                table: "JobApplicationAnswer",
                column: "JobApplicationId",
                principalTable: "JobApplications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_JobApplicationAnswer_JobOfferQuestion_QuestionId",
                table: "JobApplicationAnswer",
                column: "QuestionId",
                principalTable: "JobOfferQuestion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_JobApplications_JobSeekers_ApplicantId",
                table: "JobApplications",
                column: "ApplicantId",
                principalTable: "JobSeekers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_JobApplications_JobOffers_JobOfferId",
                table: "JobApplications",
                column: "JobOfferId",
                principalTable: "JobOffers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
