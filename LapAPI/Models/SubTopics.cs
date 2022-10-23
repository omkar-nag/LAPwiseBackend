using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LapAPI.Models
{
    public class SubTopics
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Content { get; set; }

        [ForeignKey("Topics")]
        public int TopicsId { get; set; }

        public Topics Topics { get; set; }
    }
}
