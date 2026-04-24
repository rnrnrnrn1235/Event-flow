using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/tickets")]
[Authorize(Roles = "Participant")]
public class TicketController : ControllerBase
{
    private readonly TicketService _tickets;
    public TicketController(TicketService tickets)
    {
        _tickets = tickets;
    }
    [HttpPost("purchase/{eventId}")]
    public async Task<IActionResult> purchaseTicket(int eventId)
    {
        var userId = JwtHelper.GetUserId(User);
        var ticket = await _tickets.purchaseTicketAsync(userId, eventId);
        return Ok(ticket);
    }
    [HttpGet("mytickets")]
    public async Task<IActionResult> getMyTickets()
    {
        var userId = JwtHelper.GetUserId(User);
        var tickets = await _tickets.getMyTicketsAsync(userId);
        return Ok(tickets);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> getTicket(int id)
    {
        var userId = JwtHelper.GetUserId(User);
        var ticket = await _tickets.getTicketByIdAsync(userId, id);
        return Ok(ticket);
    }

}