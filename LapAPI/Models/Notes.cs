using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LapAPI.Models
{
    public class Notes
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [ForeignKey("Users")]
        public int UserId { get; set; }

        public Users? Users { get; set; }
    }
}
