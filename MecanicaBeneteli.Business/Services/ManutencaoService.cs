using MecanicaBeneteli.Business.Interfaces.Notificacoes;
using MecanicaBeneteli.Business.Interfaces.Repository;
using MecanicaBeneteli.Business.Interfaces.Services;
using MecanicaBeneteli.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MecanicaBeneteli.Business.Services
{
    public class ManutencaoService : BaseService, IManutencaoService
    {
        private readonly IManutencaoRepository _manutencaoRepository;

        public ManutencaoService(IManutencaoRepository manutencaoRepository,
        INotificador notificador) : base(notificador)
        {
            _manutencaoRepository = manutencaoRepository;
        }

        public async Task<Manutencao> CadastrarManutencao(Manutencao manutencao)
        {
            var peca = await _manutencaoRepository.ConsultarPecaParaAlterarQuantidade(manutencao.Peca.Id);

            int quantidade = peca.Quantidade - manutencao.Peca.Quantidade;

            bool sucesso = await _manutencaoRepository.AlteraQuantidadePeca(quantidade, peca.Id);

            bool insere = await _manutencaoRepository.InsereManutencao(manutencao);

            return manutencao;
        }
    }
}
