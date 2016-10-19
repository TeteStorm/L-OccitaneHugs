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
                 new Feeling { Name = "Saudade", Tags = "nostalgia aus�ncia despedida encontro reencontro reencontrar distante dor nost�lgico nost�lgica separa��o distanciamento" },
                 new Feeling { Name = "Carinho", Tags = "gentileza carinhosa carinhoso car�ncia cuidado aten��o dengo gentileza sensibilidade afago chamego ternura aconchego aconchegante afeto do�ura afetividade gentil" },
                 new Feeling { Name = "Amor", Tags = "emo��o amoroso amorosa amado amada afei��o familiaridade te amo amo fam�lia" },
                 new Feeling { Name = "Amor", Tags = "emo��o amoroso amorosa amado amada afei��o familiaridade te amo amo fam�lia" },

            };

            feelings.ToList().ForEach(f =>
                context.Set<Feeling>().AddOrUpdate(p => p.Name, f)
                );
            
        }
    }
}
