 
 namespace Eventflow.Models
{
    using Eventflow.Models.Enums;
    public class Events
    {
        public int id { get;set;}
        public int organizerId { get; set;}
        public string organizerName { get; set;} = null!;
        public string title { get; set; } = null!;
        public string description { get; set; } = null!;
        public string venue { get;set;} = null!;
        public string category { get; set; } = null!;
        public DateTime eventDate { get; set; }
        public double ticketPrice { get; set;}
        public int totalTickets { get; set;}
        public int availableTickets { get; set;}
        public string imageUrl { get; set;} = null!;
        public string attachmentUrl { get; set;} = null!;
        public EventStatus status { get; set;} = EventStatus.pending;
        public string rejectionReason { get; set;} = null!;
        public DateTime createdAt { get; set; }

    }
}