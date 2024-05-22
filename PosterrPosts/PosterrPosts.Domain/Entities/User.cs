using System.ComponentModel.DataAnnotations;

namespace PosterrPosts.Domain.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        public DateTime CreationDate { get; set; }

        public virtual IEnumerable<Post> Posts { get; set; }
    }
}
