
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace L_OccitaneHugsData
{
    using L_OccitaneHugsDomain;


    public class CityMap : EntityTypeConfiguration<City>
    {
        public CityMap()
        {
            HasKey(e => e.Id);
            Property(e => e.Id).HasColumnName("Id").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(e => e.Name).HasColumnName("Name").IsRequired();
            HasRequired(e => e.State).WithMany(e => e.Cities).HasForeignKey(e => e.StateId);
            ToTable("City");
        }
    }
}
