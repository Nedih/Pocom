using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pocom.BLL.Interfaces;
using Pocom.BLL.Extensions;
using Pocom.DAL.Entities;
using Pocom.Api.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Hosting;
using Pocom.BLL.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Pocom.BLL.Services;
using Pocom.BLL.Models.ViewModels;
using System.Security.Claims;

namespace Pocom.Api.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ReactionsController : ControllerBase
{
    private readonly IReactionService _service;

    public ReactionsController(IReactionService service)
    {
        this._service = service;
    }

    [HttpGet]
    public IActionResult GetUserReactions()
    {
        string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var profile = _service.GetUserReactions(userId);
        if (profile == null)
            return NotFound("No such user");
        return Ok(profile);
    }

    [HttpPost]
    public IActionResult PostReaction([FromBody] ReactionDTO reaction)
    {
        string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //await _service.CreateAsync(email, reaction);
        return Ok(_service.Create(userId, reaction));
    }

    [HttpPut]
    public IActionResult PutReaction([FromBody] ReactionViewModel reaction)
    {
        _service.Update(User.FindFirstValue(ClaimTypes.NameIdentifier), reaction);
        return Ok();
    }

    [HttpDelete]
    public IActionResult DeleteReaction([FromBody] ReactionViewModel reaction)
    {
        _service.Delete(User.FindFirstValue(ClaimTypes.NameIdentifier), reaction);
        return Ok();
    }
}
