using PosterrPosts.Domain.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PosterrPosts.Domain.Entities
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(777, ErrorMessage= "PostText must be 777 characters or less")]
        public string PostText { get; set; }
        public DateTime CreateDate { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        [ForeignKey("Post")]
        public int? PostId { get; set; }
        public EPostType PostType { get; set; }
        public virtual User User { get; set; }
        public virtual IEnumerable<Post> SubPosts { get; set; }
    }
}
