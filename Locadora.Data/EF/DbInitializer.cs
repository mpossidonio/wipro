using Locadora.Domain.Entities;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Locadora.Data.EF
{

    public class DbInitializer
    {
        public static void Initializer(LocadoraDataContext ctx)
        {
            // Se certifica que o BD existe
            ctx.Database.EnsureCreated();

            // Verifica se há dados
            if (!ctx.Clientes.Any())
            {

                ctx.Clientes.AddRange(new List<Cliente>()
                {
                    new Cliente(){Nome = "Cliente 1", CPF = "11111111111" },
                    new Cliente(){Nome = "Cliente 2", CPF = "22222222222" },
                    new Cliente(){Nome = "Cliente 3", CPF = "33333333333" }
                });

                ctx.SaveChanges();
            }

            if (!ctx.Filmes.Any())
            {
                ctx.Filmes.AddRange(new List<Filme>()
                {
                    new Filme(){Titulo = "Era uma vez no Oeste", TituloOriginal = "Once Upon a Time in the West", Ano = 1968},
                    new Filme(){Titulo = "Onde os Fracos Não Tem Vez", TituloOriginal = "No Country for Old Men", Ano = 2007},
                    new Filme(){Titulo = "Um Estranho no Ninho", TituloOriginal = "One Flew Over the Cuckoo's Nest", Ano = 1975},
                    new Filme(){Titulo = "Clube dos Cinco", TituloOriginal = "The Breakfast Club", Ano = 1985},
                    new Filme(){Titulo = "Scarface", TituloOriginal = "Scarface", Ano = 1983},
                    new Filme(){Titulo = "Os Caça-Fantasmas", TituloOriginal = "Ghostbusters", Ano = 1984},
                    new Filme(){Titulo = "Pulp Fiction", TituloOriginal = "Pulp Fiction", Ano = 1994},
                    new Filme(){Titulo = "Clube da Luta", TituloOriginal = "Fight Club", Ano = 1999},
                    new Filme(){Titulo = "O Silêncio dos Inocentes", TituloOriginal = "The Silence of the Lambs", Ano = 1991}
                });
                ctx.SaveChanges();
            }

            if (!ctx.Locacoes.Any())
            {
                ctx.Locacoes.AddRange(new List<Locacoes>()
                {
                    new Locacoes(){IdCliente=1, IdFilme = 1, DataLocacao=Convert.ToDateTime("2020-06-18"), DevolucaoPrevista=Convert.ToDateTime("2020-06-19"), Devolucao=Convert.ToDateTime("2020-06-20")},
                    new Locacoes(){IdCliente=1, IdFilme = 2, DataLocacao=Convert.ToDateTime("2020-06-18"), DevolucaoPrevista=Convert.ToDateTime("2020-06-19"), Devolucao=Convert.ToDateTime("2020-06-20")},
                    new Locacoes(){IdCliente=1, IdFilme = 3, DataLocacao=Convert.ToDateTime("2020-06-18"), DevolucaoPrevista=Convert.ToDateTime("2020-06-20"), Devolucao=Convert.ToDateTime("2020-06-20")},
                    new Locacoes(){IdCliente=1, IdFilme = 4, DataLocacao=Convert.ToDateTime("2020-06-18"), DevolucaoPrevista=Convert.ToDateTime("2020-06-20"), Devolucao=Convert.ToDateTime("2020-06-20")},
                    new Locacoes(){IdCliente=1, IdFilme = 5, DataLocacao=Convert.ToDateTime("2020-06-18"), DevolucaoPrevista=Convert.ToDateTime("2020-06-21"), Devolucao=Convert.ToDateTime("2020-06-20")},
                    new Locacoes(){IdCliente=1, IdFilme = 6, DataLocacao=Convert.ToDateTime("2020-06-18"), DevolucaoPrevista=Convert.ToDateTime("2020-06-21"), Devolucao=Convert.ToDateTime("2020-06-20")},
                    new Locacoes(){IdCliente=2, IdFilme = 7, DataLocacao=Convert.ToDateTime("2020-06-18"), DevolucaoPrevista=Convert.ToDateTime("2020-06-21"), Devolucao=Convert.ToDateTime("2020-06-20")},
                    new Locacoes(){IdCliente=2, IdFilme = 8, DataLocacao=Convert.ToDateTime("2020-06-18"), DevolucaoPrevista=Convert.ToDateTime("2020-06-21"), Devolucao=Convert.ToDateTime("2020-06-20")},
                    new Locacoes(){IdCliente=2, IdFilme = 9, DataLocacao=Convert.ToDateTime("2020-06-18"), DevolucaoPrevista=Convert.ToDateTime("2020-06-22"), Devolucao=Convert.ToDateTime("2020-06-20")},
                    new Locacoes(){IdCliente=3, IdFilme = 1, DataLocacao=Convert.ToDateTime("2020-06-18"), DevolucaoPrevista=Convert.ToDateTime("2020-06-19"), Devolucao=Convert.ToDateTime("2020-06-20")},
                    new Locacoes(){IdCliente=3, IdFilme = 2, DataLocacao=Convert.ToDateTime("2020-06-18"), DevolucaoPrevista=Convert.ToDateTime("2020-06-20"), Devolucao=Convert.ToDateTime("2020-06-20")},
                    new Locacoes(){IdCliente=3, IdFilme = 3, DataLocacao=Convert.ToDateTime("2020-06-18"), DevolucaoPrevista=Convert.ToDateTime("2020-06-21"), Devolucao=Convert.ToDateTime("2020-06-20")},
                    new Locacoes(){IdCliente=3, IdFilme = 4, DataLocacao=Convert.ToDateTime("2020-06-18"), DevolucaoPrevista=Convert.ToDateTime("2020-06-20"), Devolucao=Convert.ToDateTime("2020-06-20")},
                    new Locacoes(){IdCliente=3, IdFilme = 5, DataLocacao=Convert.ToDateTime("2020-06-18"), DevolucaoPrevista=Convert.ToDateTime("2020-06-22"), Devolucao=Convert.ToDateTime("2020-06-20")},
                    new Locacoes(){IdCliente=3, IdFilme = 6, DataLocacao=Convert.ToDateTime("2020-06-18"), DevolucaoPrevista=Convert.ToDateTime("2020-06-22"), Devolucao=Convert.ToDateTime("2020-06-20")}
                });
                ctx.SaveChanges();
            }
        }
    }
}

