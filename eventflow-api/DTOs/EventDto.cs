public class EventDto
{
    public int id {get; set;}
    public string organizerName{get; set;}
    public string title {get; set;}
    public string description {get; set;}
    public string venue {get; set;}
    public string category {get; set;}
    public DateTime eventDate {get; set;}
    public double ticketPrice {get; set;}
    public int totalTickets {get; set;}
    public int availableTickets {get; set;}
    public string imageUrl {get; set;}
    public string attachmentUrl {get; set;}
    public string status {get; set;}
    public string rejectionReason {get; set;}
}