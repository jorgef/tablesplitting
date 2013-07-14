using System.Collections.Generic;

namespace TableSplitting
{
    public class Address
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public User User { get; set; }
        public ICollection<Itinerary> Itineraries { get; set; }
    }
}