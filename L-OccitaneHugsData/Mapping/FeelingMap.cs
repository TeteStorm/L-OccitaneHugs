
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace L_OccitaneHugsData
{
    using L_OccitaneHugsDomain;
    public class SentimentMap : EntityTypeConfiguration<Feeling>
    {
        public SentimentMap()
        {
            HasKey(t => t.Id);
            Property(e => e.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).HasColumnName("Id");
            Property(t => t.Name).IsRequired();
            Property(t => t.Tags).IsRequired();
            ToTable("Feeling");
        }
    }
}