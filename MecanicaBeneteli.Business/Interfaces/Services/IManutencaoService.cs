using MecanicaBeneteli.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MecanicaBeneteli.Business.Interfaces.Services
{
    public interface IManutencaoService
    {
        Task<Manutencao> CadastrarManutencao(Manutencao manutencao);
    }
}
