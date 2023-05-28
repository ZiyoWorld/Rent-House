namespace Houzing.Data.Houses
{
    public class Owner
    {
        public int? Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? DateBirthday { get; set; }
        public string? Citizenship { get; set; }
        public string? Email { get; set; }
        public string? PasportNumber { get; set; }
        public string? PhoneNumber { get; set; }
        public List<Apartment> Apartments { get; set; } = new List<Apartment>();
        public List<HouseItem> HouseItems { get; set; } = new List<HouseItem>();
    }
}
