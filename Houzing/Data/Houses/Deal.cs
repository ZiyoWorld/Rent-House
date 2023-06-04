using System.ComponentModel.DataAnnotations.Schema;

namespace Houzing.Data.Houses
{
    public class Deal
    {
        public int Id { get; set; }
        public string? Type { get; set; }
        public string? FromDeal { get; set; }
        public string? ToDeal { get; set; }
        public string? DateDeal { get; set; }
        public decimal? Summa { get; set; }
        public string? PayType { get; set; }
        [ForeignKey("ApartmentId")]
        public int? ApartmentId { get; set; }
        public Apartment? Apartment { get; set; }
        [ForeignKey("CustomerId")]
        public int? CustomerId { get; set; } 
        public Customer? Customer { get; set; }
        [ForeignKey("EmployerId")]
        public int? EmployerId { get; set; }
        public Employer? Employer { get; set; }
    }
}
