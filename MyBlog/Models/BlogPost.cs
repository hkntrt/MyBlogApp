using System.ComponentModel.DataAnnotations;

namespace MyBlog.Models
{
    public class BlogPost
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Title { get; set; }
        public string? Image { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        [Required]
        public string? Article { get; set; }

        public override string ToString()
        {
            return $"title: {Title}, article: {Article}";
        }

    }
}
