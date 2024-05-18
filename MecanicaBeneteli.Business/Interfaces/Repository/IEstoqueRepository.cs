using MecanicaBeneteli.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MecanicaBeneteli.Business.Interfaces.Repository
{
    public interface IEstoqueRepository
    {
        Task<bool> InserePecas(Peca peca);
        Task<ICollection<Peca>> ConsultarPecas();
        Task<Peca> ConsultarPecaParaAlterar(int id);
        Task<bool> AlteraPeca(Peca peca);
    }
}
