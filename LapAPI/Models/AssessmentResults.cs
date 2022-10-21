using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LapAPI.Models
{
    public class AssessmentResults
    {
        [Key]
        public int Id { get; set; }


        [Required]
        public int Score { get; set; }

        [Required]
        [ForeignKey("Assessments")]
        public int AssessmentId { get; set; }

        [ForeignKey("Users")]
        public int UserId { get; set; }


        public Users user { get; set; }

        public Assessments assessment { get; set; }

    }
}
