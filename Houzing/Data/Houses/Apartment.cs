using Houzing.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Houzing.Data.Houses
{
    public class Apartment
    {
        public int? Id { get; set; }
        public string? Adress { get; set; }
        public string? Country { get; set; }
        public string? Region { get; set; }
        public string? City { get; set; }
        public string? NumberHouse { get; set; }
        public string? Floor { get; set; }
        public string? TypeHouse { get; set; }
        public string? Repair { get; set; }
        public string? MaxPrice { get; set; }
        public string? MinPrice { get; set; }
        public string? Status { get; set; }
        [ForeignKey("HouseItemId")]
        public int? HouseItemId { get; set; }
        public HouseItem? HouseItem { get; set; }
        [ForeignKey("OwnerId")]
        public int? OwnerId { get; set; }
        public Owner? Owner { get; set; }
    }
}
