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
    public class EstoqueService : BaseService, IEstoqueService
    {
        private readonly IEstoqueRepository _estoqueRepository;

        public EstoqueService(IEstoqueRepository estoqueRepository,
        INotificador notificador) : base(notificador)
        {
            _estoqueRepository = estoqueRepository;
        }

        public async Task<ICollection<Peca>> ConsultarPecas()
        {
            var pecas = await _estoqueRepository.ConsultarPecas();

            return pecas;
        }

        public async Task<Peca> IncluirPeca(Peca peca)
        {
            bool sucesso = await _estoqueRepository.InserePecas(peca);
            if (!sucesso)
                Notificar("Erro ao inserir a peça");

            return peca;
        }

        public async Task<Peca> ConsultarPecaParaAlterar(int id)
        {
            var peca = await _estoqueRepository.ConsultarPecaParaAlterar(id);

            return peca;
        }

        public async Task<Peca> AlterarPeca(Peca peca)
        {
            bool sucesso = await _estoqueRepository.AlteraPeca(peca);
            if (!sucesso)
                Notificar("Erro ao inserir a peça");

            return peca;
        }
    }
}
