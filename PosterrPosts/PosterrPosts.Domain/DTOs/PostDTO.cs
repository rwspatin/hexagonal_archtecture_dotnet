using PosterrPosts.Domain.Enum;

namespace PosterrPosts.Domain.DTOs
{
    public class PostDTO
    {
        public int Id { get; set; }
        public string PostText { get; set; }
        public DateTime CreateDate { get; set; }
        public EPostType PostType { get; set; }
        public int? PostId { get; set; }
        public UserDTO User { get; set; }
        public IEnumerable<PostDTO> RelatedPosts { get; set; }
    }
}
