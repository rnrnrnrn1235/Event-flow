public class TicketDto{
    public int Id { get; set; }
    public string EventTitle { get; set; }
    public string EventVenue { get; set; }
    public DateTime EventDate { get; set; }
    public double pricePaid { get; set; }
    public string QRCode { get; set; } //base64 string representation of the QR code image elmafrood
    public string uniqueCode { get; set; }
    public DateTime PurchaseDate { get; set; }
}