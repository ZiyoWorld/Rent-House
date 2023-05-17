namespace Houzing.Data.Houses
{
    public class Contract
    {
        public int Id { get; set; }
        public int? ApartmentId { get; set; }
        public int? CustomerId { get; set; }
        public int? EmployerId { get; set; }
        public string? Type { get; set; }
        public string? Summ { get; set; }
        public string? FromDeal { get; set; }
        public string? ToDeal { get; set; }
        public string? DateDeal { get; set; }
        public string? Status { get; set; }
    }
}
