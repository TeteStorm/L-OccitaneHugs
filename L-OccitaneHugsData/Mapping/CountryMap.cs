using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace L_OccitaneHugsData
{

    using L_OccitaneHugsDomain;
    public class CountryMap : EntityTypeConfiguration<Country>
    {
        public CountryMap()
        {
            HasKey(e => e.Id);
            Property(e => e.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None).HasColumnName("Id");
            Property(e => e.Name).HasColumnName("Name").IsRequired();
            Property(e => e.Abbreviation).HasColumnName("Abbreviation").IsRequired();
            HasMany(e => e.States);
            ToTable("Country");
        }
    }
}
