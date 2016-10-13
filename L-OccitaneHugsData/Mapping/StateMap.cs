using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace L_OccitaneHugsData
{
    
    using L_OccitaneHugsDomain;
    public class StateMap : EntityTypeConfiguration<State>
    {
        public StateMap()
        {
            HasKey(e => e.Id);
            Property(e => e.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None).HasColumnName("Id");
            Property(e => e.Name).HasColumnName("Name").IsRequired();
            Property(e => e.Abbreviation).HasColumnName("Abbreviation").IsRequired();
            HasRequired(e => e.Country).WithMany(e=>e.States).HasForeignKey(e=>e.CountryId);
            HasMany(e => e.Cities);
            ToTable("State");
        }
    }
}
