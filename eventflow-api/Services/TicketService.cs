using Eventflow.Data;
using Eventflow.Models;
using Eventflow.Models.Enums;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
public class TicketService
{
    private readonly AppDbContext _db;
    private readonly QrCodeHelper _qr;
    private readonly IHubContext<EventFlowHub> _hub;
    
    public TicketService(AppDbContext db, QrCodeHelper qr, IHubContext<EventFlowHub> hub)
    {
        _db = db;
        _qr = qr;
        _hub = hub;
    }

public async Task<TicketDto> purchaseTicketAsync(int userId, int eventId)
{
 await using  var transaction = await _db.Database.BeginTransactionAsync();

   try
        {
            var ev = await _db.Events
            .FirstOrDefaultAsync(e => e.id == eventId && e.status == EventStatus.approved)
            ?? throw new Exception("Event not found or not available.");
            if(ev.availableTickets <=0)
            {
                throw new Exception("Sorry! This event is sold out.");
            }
            //Check if user already has a ticket
            var existingTicket = await _db.Tickets
            .AnyAsync(t => t.UserId == userId && t.EventId == eventId);
            if(existingTicket)
            {
                throw new Exception("You have already purchased a ticket for this event.");
            }

            //Create ticket QR
            var uniqueCode = Guid.NewGuid().ToString();
            var qrBase64 = _qr.GenerateQrCode(uniqueCode);

            var ticket = new Ticket
            {
                UserId = userId,
                EventId = eventId,
                QRCode = qrBase64,
                uniqueCode = uniqueCode,
                PurchaseDate = DateTime.UtcNow
            };
            _db.Tickets.Add(ticket);
            ev.availableTickets--;
            await _db.SaveChangesAsync();
            await transaction.CommitAsync();

            //Broadcast updated count to everyone watching this event
            await _hub.Clients
            .Group($"event-{eventId}")
            .SendAsync("TicketCountUpdated" , new
            {
                eventId = ev.id,
                availableTickets = ev.availableTickets


            });
            return await MapToDto(ticket, ev);
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }     
}

public async Task<List<TicketDto>> getMyTicketsAsync(int userId)
{
    var tickets = await _db.Tickets
    .Include(t => t.Events)
    .Where(t => t.UserId == userId)
    .OrderByDescending(t => t.PurchaseDate)
    .ToListAsync();

    return tickets.Select(t => MapToDto(t, t.Events).Result).ToList();

}

public async Task<TicketDto> getTicketByIdAsync(int userId, int ticketId)
{
        var ticket = await _db.Tickets.Include(t => t.Events)
        .FirstOrDefaultAsync(t => t.Id == ticketId && t.UserId == userId)
        ?? throw new Exception("Ticket not found.");
    return await MapToDto(ticket, ticket.Events);
}

private static Task<TicketDto> MapToDto(Ticket t, Events ev) =>
        Task.FromResult(new TicketDto
        {
            Id          = t.Id,
            EventTitle  = ev.title,
            EventVenue  = ev.venue,
            EventDate   = ev.eventDate,
            pricePaid   = t.pricePaid,
            QRCode      = t.QRCode,
            uniqueCode  = t.uniqueCode,
            PurchaseDate = t.PurchaseDate
        });
}