namespace Eventflow.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public int UserId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string QRCode { get; set; } = null!;
        public string uniqueCode { get; set; } = null!;
        public double pricePaid { get; set; }

    public User  User  { get; set; }
    public Events Events { get; set; }
    }
}