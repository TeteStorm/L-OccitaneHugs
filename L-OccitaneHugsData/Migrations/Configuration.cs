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
                 new Feeling { Name = "Conforto", Tags = "compaix�o equil�brio aconchego consolo bondade concilia��o confort�vel gostoso consola��o ajuda Compreens�o sossego urso aperto apoio prote��o protegido protegida calma paz tranquilidade tranquilo tranquila seguran�a serenidade acalma desabafo paci�ncia" },
                 new Feeling { Name = "Alegria", Tags = "felicidade �nimo euforia bom-humor feliz empolga��o energia entusiasmo �xtase vit�ria positividade comemora��o vit�ria carisma explos�o encantamento humor humorado humorada surpresa surpreso contentamento celebra��o gra�a contente" },
                 new Feeling { Name = "Amizade", Tags = "parceiro empatia parceira cumplicidade Companheirismo confian�a amigo amiga camaradagem simpatia camarada empatia companheiro companheira c�mplice coleguismo colega amigos cordialidade cordial" },
                 new Feeling { Name = "Gratid�o", Tags = "f� agradecimento orgulho perd�o realiza��o humildade agradecimento agradecido agradecida grato grata realizado realizada reconhecimento gratifica��o" },
                 new Feeling { Name = "Inspira��o", Tags = "beleza exemplo coragem equil�brio luz liberdade valentia valente deslumbrada Solidariedade sabedoria iluminado iluminada ilumina iluminou altru�smo altruista deslumbrado criatividade criativo criativa bravura" },
                 new Feeling { Name = "Paix�o", Tags = "atra��o sensualidade fasc�nio adora��o sensual tes�o" },
                 new Feeling { Name = "Esperan�a", Tags = "espera expectativa Convic��o sonho sonhar possibilidade poss�vel cren�a otimismo" }

            };

            feelings.ToList().ForEach(f =>
                context.Set<Feeling>().AddOrUpdate(p => p.Name, f)
                );
            
        }
    }
}
