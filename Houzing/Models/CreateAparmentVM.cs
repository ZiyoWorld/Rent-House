using Houzing.Data.Houses;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Houzing.Models
{
    public class CreateAparmentVM
    {
        [Key]
        public int? Id { get; set; }
        [Required(ErrorMessage = "Address is required")]
        public string? Adress { get; set; }
        [Required(ErrorMessage = "Country is required")]
        public string? Country { get; set; }
        [Required(ErrorMessage = "Region is required")]
        public string? Region { get; set; }
        [Required(ErrorMessage = "City is required")]
        public string? City { get; set; }
        [Required(ErrorMessage = "NumberHouse is required")]
        public string? NumberHouse { get; set; }
        [Required(ErrorMessage = "Floor is required")]
        public string? Floor { get; set; }
        [Required(ErrorMessage = "PhoneNumber is required")]
        public string? PhoneNumber { get; set; }
        [Required(ErrorMessage = "Repair is required")]
        public string? Repair { get; set; }
        [Required(ErrorMessage = "Max Price is required")]
        public string? MaxPrice { get; set; }
        [Required(ErrorMessage = "Min Price is required")]
        public string? MinPrice { get; set; }
        [Required(ErrorMessage = "Status Price is required")]
        public string? Status { get; set; }
        [Required(ErrorMessage = "HousItemId is required")]
        public int? HouseItemId { get; set; }
        [ForeignKey("HouseItemId")]
        public HouseItem? HouseItem { get; set; }
        [Required(ErrorMessage = "OwnerId is required")]
        public int? OwnerId { get; set; }
        [ForeignKey("OwnerId")]
        public Owner? Owner { get; set; }
    }
}
