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
    public async Task<IActionResult> GetUserReactions()
    {
        string? email = User.Identity?.Name;
        if (string.IsNullOrEmpty(email))
            return BadRequest("Email is empty");
        var profile = _service.GetUserReactions(email);
        if (profile == null)
            return NotFound("No such user with this email");
        return Ok(profile);
    }

    [HttpPost]
    public async Task<IActionResult> PostReaction(ReactionDTO reaction)
    {
        string? email = User.Identity?.Name;
        if (string.IsNullOrEmpty(email))
            return BadRequest("Email is empty");
        //await _service.CreateAsync(email, reaction);
        return Ok(await _service.CreateAsync(email, reaction));
    }
}
