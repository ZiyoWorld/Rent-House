using Houzing.Data.Houses;

namespace Houzing.Models
{
    public class AparmentViewModel
    {
        public IEnumerable<Apartment>? ApartmentsView { get; set; }
        public IEnumerable<Owner>? OwnersView { get; set; }
        public IEnumerable<HouseItem>? HouseItemsView { get; set; }
    }
}
