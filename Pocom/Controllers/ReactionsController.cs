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
    private readonly IMapper _mapper;

    public ReactionsController(IReactionService service, IMapper mapper)
    {
        this._service = service;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetUserReactions()
    {
        string? email = User.Identity.Name;
        if (string.IsNullOrEmpty(email))
            return BadRequest("Email is empty");
        var profile = await _service.GetUserReactionsAsync(email);
        if (profile == null)
            return NotFound("No such user with this email");
        return Ok(profile);
    }
}
