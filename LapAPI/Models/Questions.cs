using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LapAPI.Models
{
    public class Questions
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Question { get; set; }

        [Required]
        public string Answer { get; set; }
        [Required]
        public string Options { get; set; } // "optA{delim}optB{delim}optC{delim}optD"

        [Required]
        public char QuestionType { get; set; }

        [ForeignKey("Quizzes")]
        public int QuizId { get; set; }

        public Quizzes quiz { get; set; }
        
    }
}