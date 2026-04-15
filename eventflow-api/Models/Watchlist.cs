namespace Eventflow.Models
{
    public class watchlist
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int EventId { get; set; }
        public DateTime savedAt { get; set; }
    }
}