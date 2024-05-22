using Microsoft.AspNetCore.Mvc;
using PosterrPosts.Application.Contracts.Services;
using PosterrPosts.Domain.DTOs;

namespace PosterrPosts.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserProfileController : Controller
    {
        private readonly IUserService _userService;
        private readonly IPostService _postService;
        public UserProfileController(
            IUserService userService,
            IPostService postService)
        {
            _userService = userService;
            _postService = postService;
        }

        [HttpGet("{userId}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(UserDataDTO), StatusCodes.Status200OK)]
        public async Task<IActionResult> Index(int userId)
        {
            try
            {
                var user = await _userService.GetUserById(userId);
                if(user == null)
                    return NotFound();

                return Ok(user);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Return posts of user selected
        /// </summary>
        /// <param name="userId">The id of the required user</param>
        /// /// <param name="qtdPosts">Qtd of required posts</param>
        [HttpGet("UserPosts/{userId}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(string), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IEnumerable<PostDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UserPosts(int userId, [FromQuery]int qtdPosts = 5)
        {
            try
            {
                var posts = await _postService.GetAllPostsByUser(userId);
                if (posts == null || posts.Count <= 0)
                    return NoContent();

                return Ok(posts.Take(qtdPosts));
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
