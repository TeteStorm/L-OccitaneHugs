using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace L_OccitaneHugsData
{
    using L_OccitaneHugsDomain;
    public class RegionMap : EntityTypeConfiguration<Region>
    {
        public RegionMap()
        {
            HasKey(e => e.Id);
            Property(e => e.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None).HasColumnName("Id");
            ToTable("Region");
        }
    }
}
