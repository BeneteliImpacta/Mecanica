using MecanicaBeneteli.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MecanicaBeneteli.Business.Interfaces.Repository
{
    public interface IManutencaoRepository
    {
        Task<Peca> ConsultarPecaParaAlterarQuantidade(int id);
        Task<bool> AlteraQuantidadePeca(int quantidade, int id);
        Task<bool> InsereManutencao(Manutencao manutencao);

    }
}
