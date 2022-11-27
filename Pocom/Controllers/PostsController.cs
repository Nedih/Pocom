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
        return _service.Get(vm);
    }
    [AllowAnonymous]
    [HttpGet]
    public Task<IEnumerable<PostDTO>> Index()
    {
        return _service.GetAll(User.Identity?.Name);
    }

    [HttpDelete]
    public void Delete(Guid id)
    {
        var post = _service.GetPost(id);
        if (post == null) { return; }
        if (post.Author == User.Identity?.Name)
            _service.Delete(id);
    }

    [HttpPatch("edittext")]
    public void EditText([FromForm] Guid postId, [FromForm] Guid authorId, [FromForm] string text)
    {
        _service.UpdateText(postId, authorId, text);
    }
    [HttpGet("ownposts")]
    public IEnumerable<PostDTO> GetOwnPosts()
    {
        return _service.Get(x => x.Author.Email == User.Identity?.Name).ToList();
    }
    [AllowAnonymous]
    [HttpGet("{id}")]
    public PostDTO? GetPost(Guid id)
    {
        return _service.GetPost(id);
    }
    [AllowAnonymous]
    [HttpGet("comments/{id}")]
    public IEnumerable<PostDTO> GetComments(Guid id)
    {
        return _service.Get(x => x.ParentPostId == id);
    }
    [HttpGet("byemail")]
    public IEnumerable<PostDTO> GetByEmail([FromBody] string email)
    {
        return _service.Get(x => x.Author.Email == email).ToList();
    }

    [HttpPost]
    public async Task<IdentityResult> CreatePost([FromBody] PostDTO postModel)
    {
        return await _service.Create(User.Identity?.Name, postModel);
    }

    [HttpGet("user-reactions")]
    public async Task<IEnumerable<PostDTO>> GetUserReactionsPostsAsync()
    {
        return await _service.GetUserReactionsPosts(User.Identity?.Name);
    }
}