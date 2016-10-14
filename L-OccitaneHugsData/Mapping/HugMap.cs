
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace L_OccitaneHugsData
{
    using L_OccitaneHugsDomain;
    public class HugMap : EntityTypeConfiguration<Hug>
    {
        public HugMap()
        {
            HasKey(t => t.Id);
            Property(e => e.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).HasColumnName("Id");
            Property(t => t.From).IsRequired();
            Property(t => t.To).IsRequired();
            Property(t => t.Message).IsRequired();
            Property(t => t.Message).IsRequired();
            Property(t => t.CreateDate).IsRequired();
            HasRequired(e => e.City);
            HasRequired(e => e.Feeling);
            ToTable("Hug");
        }
    }
}