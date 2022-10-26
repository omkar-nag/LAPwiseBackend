using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LapAPI.Migrations
{
    public partial class AddQuizQuestionCount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QuestionCount",
                table: "Quizzes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuestionCount",
                table: "Quizzes");
        }
    }
}
