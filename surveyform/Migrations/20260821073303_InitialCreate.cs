using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace surveyform.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SurveyResponses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Satisfaction = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EaseOfUse = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    QualityRating = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Recommendation = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Likes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Improvements = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    UseAgain = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    AdditionalComments = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    SubmittedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyResponses", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SurveyResponses");
        }
    }
}
