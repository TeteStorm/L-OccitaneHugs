using System.Collections.Generic;

namespace L_OccitaneHugsDomain
{
    public class State : BaseEntity
    {
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }
        public IList<City> Cities { get; set; }
        public int RegionId { get; set; }
        public Region Region { get; set; }

    }
}
