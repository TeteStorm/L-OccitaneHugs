using System.Collections.Generic;

namespace L_OccitaneHugsDomain
{
    public class Region : BaseEntity
    {
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public IList<State> States { get; set; }
    }
}
