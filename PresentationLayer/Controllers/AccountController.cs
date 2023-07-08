using ApplicationLayer.Dto;
using ApplicationLayer.Exceptions;
using ApplicationLayer.Services.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models.Accounts;

namespace PresentationLayer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IAccountService _service;

    public AccountController(IAccountService service)
    {
        _service = service;
    }

    public CancellationToken CancellationToken => HttpContext.RequestAborted;

    [HttpPost("/Login")]
    public async Task<ActionResult<Guid>> CreateAsync(string login, string password)
    {
        try
        {
            PrivateAccountDto account = await _service.Login(login, password, CancellationToken);
            return _service.RegisterEmployee(account.Id);
        }
        catch (NotFoundException)
        {
            return NotFound("Invalid login or password");
        }
    }
}