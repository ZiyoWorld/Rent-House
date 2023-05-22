namespace Houzing.Data.Houses
{
    public class HouseItem
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Room { get; set; }
        public string? Bath { get; set; }
        public string? Garage { get; set; }
        public string? Area { get; set; }
        public string? YearBuilt { get; set; }
        public string? Price { get; set; }
        public string? Parking { get; set; }
        public string? Garden { get; set; }
        public string? Balcony { get; set; }
        public string? SalePrice { get; set; }
        public string? Location { get; set; }
        public ICollection<HouseImg>? HouseImg { get; set; }
        public string? Category { get; set; }
    }
}
