using ApplicationLayer.Exceptions;
using ApplicationLayer.Services.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MessageController : ControllerBase
{
    private readonly IMessageService _messageService;
    private readonly IValidationService _validationService;

    public MessageController(IMessageService messageService, IValidationService validationService)
    {
        _messageService = messageService;
        _validationService = validationService;
    }

    public CancellationToken CancellationToken => HttpContext.RequestAborted;

    [HttpPost("{requestingId:guid}/Send Message/")]
    public IActionResult SendMessage(string message, string addressee, string sender, string? subject, Guid requestingId)
    {
        try
        {
            _messageService.SendMessage(addressee, sender, message, subject, requestingId);
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

    [HttpPost("{messageId:guid},{requestingId:guid}/Mark As Read/")]
    public IActionResult MarkAsRead(Guid messageId, Guid requestingId)
    {
        try
        {
            _messageService.MarkAsRead(messageId, requestingId);
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

    [HttpPost("{messageId:guid},{requestingId:guid}/Reply On Message/")]
    public IActionResult ReplyOnEmailMessage(string sender, string addressee, Guid messageId, Guid requestingId, string content, string? subject)
    {
        try
        {
            _messageService.ReplayOnMessage(addressee, sender, messageId, content, subject, requestingId);
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

    [HttpPost("{employeeId:guid}/Exit/")]
    public IActionResult Exit(Guid employeeId)
    {
        try
        {
            _messageService.Cancel(employeeId);
        }
        catch (NotFoundException)
        {
            return NotFound(employeeId);
        }

        return Ok();
    }
}