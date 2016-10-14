namespace L_OccitaneHugsData.Migrations
{
    using L_OccitaneHugsDomain;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<L_OccitaneHugsData.EFDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(L_OccitaneHugsData.EFDbContext context)
        {
            var feelings = new Feeling[]
            {
                 new Feeling { Name = "Amor", Tags = "emo��o amoroso amorosa amado amada afei��o familiaridade te amo amo fam�lia" },
                
            };

            feelings.ToList().ForEach(f =>
                context.Set<Feeling>().AddOrUpdate(p => p.Name, f)
                );
            
        }
    }
}
