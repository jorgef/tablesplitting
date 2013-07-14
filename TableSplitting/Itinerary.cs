using System.Collections.Generic;

namespace TableSplitting
{
    public class Itinerary
    {
        public int Id { get; set; }
        public ICollection<Address> Addresses { get; set; }
    }
}
