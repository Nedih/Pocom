using Microsoft.AspNetCore.Mvc;
using Pocom.BLL.Interfaces;
using Pocom.BLL.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Pocom.BLL.Models;
using Microsoft.AspNetCore.Identity;

namespace Pocom.Api.Controllers;

[Authorize]
[Route("api/v1/[controller]")]
[ApiController]
public class PostsController : ControllerBase
{
    private readonly IPostService _service;

    public PostsController(IPostService service)
    {
        _service = service;
    }

    [AllowAnonymous]
    [HttpPost("query")]
    public IEnumerable<PostDTO> Index([FromBody] RequestViewModel vm)
    {
        return _service.GetAsync(vm);
    }
    [AllowAnonymous]
    [HttpGet]
    public IEnumerable<PostDTO> Index()
    {
        return _service.GetAsync(new RequestViewModel());
    }

    [HttpDelete]
    public void Delete(Guid id)
    {
        var post = _service.FirstOrDefaultAsync(x => x.Id == id);
        if (post.Author == User.Identity.Name)
            _service.DeleteAsync(id);
    }

    [HttpPatch("edittext")]
    public void EditText([FromForm] Guid postId, [FromForm] Guid authorId, [FromForm] string text)
    {
        _service.UpdateTextAsync(postId, authorId, text);
    }
    [HttpGet("ownposts")]
    public IEnumerable<PostDTO> GetOwnPosts()
    {
        return _service.GetAsync(x => x.Author.Email == User.Identity.Name).ToList();
    }
    [AllowAnonymous]
    [HttpGet("{id}")]
    public PostDTO GetPost(Guid id)
    {
        return _service.FirstOrDefaultAsync(x => x.Id == id);
    }
    [AllowAnonymous]
    [HttpGet("comments/{id}")]
    public IEnumerable<PostDTO> GetComments(Guid id)
    {
        return _service.GetAsync(x => x.ParentPostId == id);
    }
    [HttpGet("byemail")]
    public IEnumerable<PostDTO> GetByEmail([FromBody] string email)
    {
        return _service.GetAsync(x => x.Author.Email == email).ToList();
    }

    [HttpPost]
    public async Task<IdentityResult> CreatePost([FromBody] PostDTO postModel)
    {
        return await _service.CreateAsync(User?.Identity?.Name, postModel);
    }

    [HttpGet("user-reactions")]
    public async Task<IEnumerable<PostDTO>> GetUserReactionsPostsAsync()
    {
        return await _service.GetUserReactionsPostsAsync(User?.Identity?.Name);
    }
}