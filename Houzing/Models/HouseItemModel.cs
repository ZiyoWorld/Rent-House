using Houzing.Data.Houses;
using System.ComponentModel.DataAnnotations;

namespace Houzing.Models
{
    public class HouseItemModel
    {
        [Key]
        public int? Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "Room is required")]
        public string? Room { get; set; }
        [Required(ErrorMessage = "Bath is required")]
        public string? Bath { get; set; }
        [Required(ErrorMessage = "Garage is required")]
        public string? Garage { get; set; }
        [Required(ErrorMessage = "Area is required")]
        public string? Area { get; set; }
        [Required(ErrorMessage = "YearBuilt is required")]
        public string? YearBuilt { get; set; }
        [Required(ErrorMessage = "Price is required")]
        public string? Price { get; set; }
        [Required(ErrorMessage = "Parking is required")]
        public string? Parking { get; set; }
        [Required(ErrorMessage = "Garden is required")]
        public string? Garden { get; set; }
        [Required(ErrorMessage = "Balcony is required")]
        public string? Balcony { get; set; }
        [Required(ErrorMessage = "SalePrice is required")]
        public string? SalePrice { get; set; }
        [Required(ErrorMessage = "Location is required")]
        public string? Location { get; set; }
        [Required(ErrorMessage = "Category is required")]
        public string? Category { get; set; }
        [Required(ErrorMessage = "OwnerId is required")]
        public int? OwnerId { get; set; }
        public Owner? Owner { get; set; }
        [Required(ErrorMessage = "ImagePath is required")]
        public IFormFile? ImageFile1 { get; set; }
        public IFormFile? ImageFile2 { get; set; }
        public IFormFile? ImageFile3 { get; set; }
    }
}
