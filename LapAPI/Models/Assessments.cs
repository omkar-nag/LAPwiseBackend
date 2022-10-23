using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LapAPI.Models
{
    public class Assessments
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string  Title { get; set; }

        [ForeignKey("Quizzes")]
        public int QuizId { get; set; }

        [Required]
        [ForeignKey("Topics")]
        public int TopicId { get; set; }

        public Topics Topics { get; set; }

        public Quizzes Quizzes { get; set; }

    }
}
