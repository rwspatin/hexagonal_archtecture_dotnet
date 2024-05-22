using Microsoft.AspNetCore.Mvc;
using PosterrPosts.Application.Contracts.Services;
using PosterrPosts.Domain.DTOs;

namespace PosterrPosts.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : Controller
    {
        private readonly IPostService _postService;
        public HomeController(
            IPostService postService)
        {
            _postService = postService;
        }

        /// <summary>
        /// Start page
        /// </summary>
        /// <returns>Return the 10 latest posts</returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IEnumerable<PostDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Index()
        {
            try
            {
                var posts = await _postService.GetAllPosts();
                if (posts == null || posts.Count <= 0)
                    return NoContent();
                
                return Ok(posts.Take(10).ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Request to get posts from all users
        /// </summary>
        /// <param name="qtdPosts">Limit of posts</param>
        /// <param name="from">Start posts</param>
        /// <param name="to">Date limit of posts</param>
        /// <returns>Default return 10 posts</returns>
        [HttpGet("GeneralPosts")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IEnumerable<PostDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GeneralPosts([FromQuery] int qtdPosts = 10, DateTime? from = null, DateTime? to = null)
        {
            try
            {
                var posts = await _postService.GetAllPosts(from, to);
                if (posts == null || posts.Count <= 0)
                    return NoContent();

                return Ok(posts.Take(qtdPosts).ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Request to get posts from all users
        /// </summary>
        /// <param name="qtdPosts">Limit of posts</param>
        /// <param name="from">Start posts</param>
        /// <param name="to">Date limit of posts</param>
        /// <returns>Default return 10 posts</returns>
        [HttpGet("GeneralPosts/{userId}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(string), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IEnumerable<PostDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UserPosts(int userId, [FromQuery] int qtdPosts = 10, DateTime? from = null, DateTime? to = null)
        {
            try
            {
                var posts = await _postService.GetAllPostsByUser(userId, from, to);
                if (posts == null || posts.Count <= 0)
                    return NoContent();

                return Ok(posts.Take(qtdPosts).ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Add a new post
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddPost(CreatePostDTO post)
        {
            try
            {
                if (post == null)
                    return BadRequest("Post deve ser preenchido");
                else if (post.UserId <= 0)
                    return BadRequest("Necessario preencher user id do post");

                var (sucess, error) = await _postService.AddPost(post);
                if (!sucess)
                    return BadRequest(error);

                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Response a existe post
        /// </summary>
        /// <param name="postId">Id of post of react</param>
        [HttpPost("Repost/{postId}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddPost(int postId, CreatePostDTO post)
        {
            try
            {
                if(postId <= 0)
                    return BadRequest("Id do Post relacionado deve ser preenchido");
                else if (post == null)
                    return BadRequest("Post deve ser preenchido");
                else if (post.UserId <= 0)
                    return BadRequest("Necessario preencher user id do post");

                var (sucess, error) = await _postService.ReactToPost(post, postId, Domain.Enum.EPostType.REPOSTE);
                if (!sucess)
                    return BadRequest(error);

                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Quote a existe post
        /// </summary>
        /// <param name="postId">Id of post of react</param>
        [HttpPost("Quote/{postId}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> QuotePost(int postId, CreatePostDTO post)
        {
            try
            {
                if (postId <= 0)
                    return BadRequest("Id do Post relacionado deve ser preenchido");
                else if (post == null)
                    return BadRequest("Post deve ser preenchido");
                else if (post.UserId <= 0)
                    return BadRequest("Necessario preencher user id do post");

                var (sucess, error) = await _postService.ReactToPost(post, postId, Domain.Enum.EPostType.QUOTE);
                if (!sucess)
                    return BadRequest(error);

                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

