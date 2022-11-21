using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pocom.BLL.Interfaces;
using Pocom.BLL.Extensions;
using Pocom.DAL.Entities;
using Pocom.Api.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Hosting;

namespace Pocom.Api.Controllers;

[Authorize]
[Route("api/v1/[controller]")]
[ApiController]
public class PostsController : ControllerBase
{
    private readonly IPostService _service;

    public PostsController(IPostService service)
    {
        this._service = service;
    }

    [HttpGet]
    public IEnumerable<Post> Index(int page = 1, string? sortBy = null, string? text = null)
    {
        const int pageSize = 5;
        IQueryable<Post> items;
        if (text != null)
        {
            items = _service.GetAsync(x => x.Text.ToLower().Contains(text.ToLower()));
        }
        else
        {
            items = _service.GetAsync(x => true);
        }

        return _service.Sort(items, sortBy).Paginate(page ,pageSize).ToList();
    }

    [HttpDelete]
    public void Delete(Guid id)
    {
        var post = _service.FirstOrDefaultAsync(x => x.Id == id);
        if (post.Author.Email == User.Identity.Name)
            _service.DeleteAsync(id);
    }

    [HttpPatch("edittext")]
    public void EditText([FromForm] Guid id, [FromForm] string text)
    {
        var post = _service.FirstOrDefaultAsync(x => x.Id == id);
        if (post.Author.Email == User.Identity.Name)
        {
            post.Text = text;
            _service.UpdateAsync(post);
        }
    }
    [HttpGet("ownposts")]
    public IEnumerable<Post> GetOwnPosts() {
        return _service.GetAsync(x => x.Author?.Email == User.Identity.Name).ToList();
    }
}