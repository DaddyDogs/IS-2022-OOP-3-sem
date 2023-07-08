using ApplicationLayer.Exceptions;
using ApplicationLayer.Services.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MessageSourceController : ControllerBase
{
    private readonly IMessageSourceService _messageSourceService;

    public MessageSourceController(IMessageSourceService messageSourceService)
    {
        _messageSourceService = messageSourceService;
    }

    public CancellationToken CancellationToken => HttpContext.RequestAborted;

    [HttpPost("AddEmail")]
    public Task<ActionResult<Guid>> AddEmail(string login)
    {
        return Task.FromResult<ActionResult<Guid>>(_messageSourceService.AddEmail(login));
    }

    [HttpPost("AddPhone")]
    public Task<ActionResult<Guid>> AddPhone(string phone)
    {
        return Task.FromResult<ActionResult<Guid>>(_messageSourceService.AddPhone(phone));
    }

    [HttpPost("AddMessenger")]
    public Task<ActionResult<Guid>> AddMessenger(string userId)
    {
        return Task.FromResult<ActionResult<Guid>>(_messageSourceService.AddMessenger(userId));
    }
}
