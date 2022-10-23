using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LapAPI.Models
{
    public class Topics
    {

        public Topics()
        {
            this.SubTopics = new HashSet<SubTopics>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Content { get; set; }

        public ICollection<SubTopics> SubTopics { get; set; }
    }
}
