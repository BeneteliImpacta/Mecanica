using AutoMapper;
using MecanicaBeneteli.Business.Models;
using MecanicaBeneteli.ViewModel;

namespace MecanicaBeneteli.Configurations
{
    public class AutoMapper : Profile
    {
        public AutoMapper()        
        {
            CreateMap<UsuarioViewModel, Usuario>().AfterMap((origem, destino) =>
            {
                destino.Nome = origem.Nome;
                destino.Cpf = origem.CPF;
                destino.IdUsuario = origem.IdUsuario;
                destino.Senha = origem.Senha;

            });

        }
    }
}
