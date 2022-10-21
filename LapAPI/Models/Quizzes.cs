using System.ComponentModel.DataAnnotations;

namespace LapAPI.Models
{
    public class Quizzes
    {

        public Quizzes()
        {
            this.questions = new HashSet<Questions>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public ICollection<Questions> questions { get; set; }
    }
}
