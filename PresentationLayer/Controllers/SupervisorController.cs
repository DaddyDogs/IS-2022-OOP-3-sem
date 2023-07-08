using ApplicationLayer.Exceptions;
using ApplicationLayer.Services.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SupervisorController : ControllerBase
{
    private readonly ISupervisorService _supervisorService;
    private readonly IValidationService _validationService;

    public SupervisorController(ISupervisorService supervisorService, IValidationService validationService)
    {
        _supervisorService = supervisorService;
        _validationService = validationService;
    }

    public CancellationToken CancellationToken => HttpContext.RequestAborted;

    [HttpPost("{supervisorId:guid},{firstName},{lastName},{id:guid}/Add new employee")]
    public async Task<IActionResult> AddEmployee(string firstName, string lastName, Guid supervisorId, string login, string password, Guid id, CancellationToken cancellationToken)
    {
        try
        {
            await _supervisorService.AddEmployee(firstName, lastName, supervisorId, id, login, password, cancellationToken);
        }
        catch (NotFoundException exception)
        {
            return NotFound(exception.Message);
        }
        catch (NoPermissionException exception)
        {
            return Unauthorized(exception.Message);
        }

        return Ok();
    }

    [HttpPost("{id:guid},{requestingId:guid}/Fire employee")]
    public async Task<IActionResult> FireEmployee(Guid id, Guid requestingId, CancellationToken cancellationToken)
    {
        try
        {
            await _supervisorService.RemoveEmployee(id, requestingId, cancellationToken);
        }
        catch (NotFoundException exception)
        {
            return NotFound(exception.Message);
        }
        catch (NoPermissionException exception)
        {
            return Unauthorized(exception.Message);
        }

        return Ok();
    }

    [HttpPost("{requestingId:guid},{hours:int}/Make Report")]
    public async Task<ActionResult<Report>> MakeReport(Guid requestingId, int hours, CancellationToken cancellationToken)
    {
        try
        {
            Report report = await _supervisorService.MakeReport(requestingId, new TimeSpan(0, hours, 0, 0));
            return report;
        }
        catch (NotFoundException exception)
        {
            return NotFound(exception.Message);
        }
        catch (NoPermissionException exception)
        {
            return Unauthorized(exception.Message);
        }
    }

    [HttpPost("{requestingId:Guid},{year:int},{month:int},{day:int}/Get Reports By Date")]
    public Task<ActionResult<List<Report>>> GetReports(Guid requestingId, int year, int month, int day, CancellationToken cancellationToken)
    {
        DateOnly date = new DateOnly(year, month, day);
        var reports = _supervisorService.GetReports(requestingId);
        return Task.FromResult<ActionResult<List<Report>>>(reports.Where(x => x.DateTime.DayOfYear == date.DayOfYear && x.DateTime.Year == date.Year).ToList());
    }

    [HttpPost("AddAccount")]
    public Task<ActionResult<Guid>> AddAccount(Guid messageSource, Guid addressee, Guid employeeId, Guid requestingId)
    {
        try
        {
            return Task.FromResult<ActionResult<Guid>>(_supervisorService.AddAccount(messageSource, addressee, employeeId, requestingId));
        }
        catch (NotFoundException exception)
        {
            return Task.FromResult<ActionResult<Guid>>(NotFound(exception.Message));
        }
        catch (NoPermissionException exception)
        {
            return Task.FromResult<ActionResult<Guid>>(Unauthorized(exception.Message));
        }
    }
}