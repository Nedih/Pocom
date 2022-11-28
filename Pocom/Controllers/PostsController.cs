using Microsoft.AspNetCore.Mvc;
using Pocom.BLL.Interfaces;
using Pocom.BLL.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Pocom.BLL.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

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
    public IEnumerable<PostDTO> IndexAsync()
    {
        return _service.GetAll(User.FindFirstValue(ClaimTypes.NameIdentifier));
    }

    [HttpDelete]
    public async void DeleteAsync(Guid id)
    {
        var post = await _service.GetPostAsync(id);
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
    public IEnumerable<PostDTO> GetOwnPostsAsync()
    {
        var vm = new RequestViewModel() { Email = User.Identity?.Name };
        if(vm.Email==null) return new List<PostDTO>();
        return _service.Get(vm).ToList();
    }
    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<PostDTO?> GetPost(Guid id)
    {
        return await _service.GetPostAsync(id);
    }
    [AllowAnonymous]
    [HttpGet("comments/{id}")]
    public IEnumerable<PostDTO> GetComments(Guid id)
    {
        return _service.GetComments(id);
    }
    [HttpGet("byemail")]
    public IEnumerable<PostDTO> GetByEmail([FromBody] string email)
    {
        var vm = new RequestViewModel() { Email = email };

        return _service.Get(vm);
    }

    [HttpPost]
    public async Task<IdentityResult> CreatePost([FromBody] PostDTO postModel)
    {
        return await _service.CreateAsync(User.Identity?.Name, postModel);
    }

    [HttpGet("user-reactions")]
    public async Task<IEnumerable<PostDTO>> GetUserReactionsPostsAsync()
    {
        return await _service.GetUserReactionsPostsAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
    }
}