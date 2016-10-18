using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L_OccitaneHugsDomain
{
    public class Hug : BaseEntity
    {
        [Required]
        public string From { get; set; }

        [Required]
        public string To { get; set; }

        [Required]
        public int CityId { get; set; }

        public City City { get; set; }

        public int FeelingId { get; set; }

        public Feeling Feeling { get; set; }

        [Required]
        public string Message { get; set; }

        public string Identifier { get; set; }

        public int Likes { get; set; }

        public string ImageUrl { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormatAttribute(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime CreateDate { get; set; }

    }
}
