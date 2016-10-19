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
                 new Feeling { Name = "Amor", Tags = "emoção amoroso amorosa amado amada afeição familiaridade te amo amo família" },
                 new Feeling { Name = "Saudade", Tags = "nostalgia ausência despedida encontro reencontro reencontrar distante dor nostálgico nostálgica separação distanciamento" },
                 new Feeling { Name = "Carinho", Tags = "gentileza carinhosa carinhoso carência cuidado atenção dengo gentileza sensibilidade afago chamego ternura aconchego aconchegante afeto doçura afetividade gentil" },
                 new Feeling { Name = "Conforto", Tags = "compaixão equilíbrio aconchego consolo bondade conciliação confortável gostoso consolação ajuda Compreensão sossego urso aperto apoio proteção protegido protegida calma paz tranquilidade tranquilo tranquila segurança serenidade acalma desabafo paciência" },
                 new Feeling { Name = "Alegria", Tags = "felicidade ânimo euforia bom-humor feliz empolgação energia entusiasmo êxtase vitória positividade comemoração vitória carisma explosão encantamento humor humorado humorada surpresa surpreso contentamento celebração graça contente" },
                 new Feeling { Name = "Amizade", Tags = "parceiro empatia parceira cumplicidade Companheirismo confiança amigo amiga camaradagem simpatia camarada empatia companheiro companheira cúmplice coleguismo colega amigos cordialidade cordial" },
                 new Feeling { Name = "Gratidão", Tags = "fé agradecimento orgulho perdão realização humildade agradecimento agradecido agradecida grato grata realizado realizada reconhecimento gratificação" },
                 new Feeling { Name = "Inspiração", Tags = "beleza exemplo coragem equilíbrio luz liberdade valentia valente deslumbrada Solidariedade sabedoria iluminado iluminada ilumina iluminou altruísmo altruista deslumbrado criatividade criativo criativa bravura" },
                 new Feeling { Name = "Paixão", Tags = "atração sensualidade fascínio adoração sensual tesão" },
                 new Feeling { Name = "Esperança", Tags = "espera expectativa Convicção sonho sonhar possibilidade possível crença otimismo" }

            };

            feelings.ToList().ForEach(f =>
                context.Set<Feeling>().AddOrUpdate(p => p.Name, f)
                );
            
        }
    }
}
