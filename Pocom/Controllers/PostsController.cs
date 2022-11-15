using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pocom.BLL.Interfaces;
using Pocom.DAL.Entities;
using Pocom.Api.Extensions;

namespace Pocom.Api.Controllers;

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

        if (sortBy != null)
        {
            var sortByArray = sortBy.Split(',');
            for (int i = 0; i < sortByArray.Length; i++)
            {
                items = i == 0
                    ? items.OrderBy(sortByArray[i].Replace(" desc", ""), sortByArray[i].EndsWith("desc"))
                    : items.ThenBy(sortByArray[i].Replace(" desc", ""), sortByArray[i].EndsWith("desc"));
            }
        }

        return items.Skip((page - 1) * pageSize).Take(pageSize).ToList();
    }
}