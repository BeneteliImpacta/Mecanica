using MecanicaBeneteli.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MecanicaBeneteli.Business.Interfaces.Services
{
    public interface IEstoqueService
    {
        Task<ICollection<Peca>> ConsultarPecas();
        Task<Peca> IncluirPeca(Peca peca);
    }
}
