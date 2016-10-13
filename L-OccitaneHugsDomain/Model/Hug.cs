using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L_OccitaneHugsDomain
{
    public class Hug : BaseEntity
    {
        public string From { get; set; }

        public string To { get; set; }

        public int CityId { get; set; }

        public string Message { get; set; }

        public string Identificator { get; set; }

        public int Likes { get; set; }

    }
}
