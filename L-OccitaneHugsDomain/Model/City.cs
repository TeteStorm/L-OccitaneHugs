using System.Collections.Generic;

namespace L_OccitaneHugsDomain
{
    public class City : BaseEntity
    {
        public string Name { get; set; }
        public int StateId { get; set; }
        public State State { get; set; }
        public bool IsCapital { get; set; }
    }
}