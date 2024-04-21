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
    public class UsuarioService : BaseService, IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository,
        INotificador notificador) : base(notificador)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<Usuario> IncluirUsuario(Usuario usuario)
        {
            bool sucesso = await _usuarioRepository.InsereUsuario(usuario);
            if (!sucesso)
                Notificar("Erro ao inserir usuário");

            return usuario;
        }

        public async Task<Usuario> ValidarUsuario(Usuario dadosUsuario)
        {
            var usuario = await _usuarioRepository.ConsultarSenha(dadosUsuario.IdUsuario);

            if (usuario == null)
            {
                throw new Exception("Usuário não encontrado");
            }

            if (dadosUsuario.Senha != usuario.Senha)
            {
                throw new Exception("Senha incorreta");
            }

            return usuario;
        }

    }
}
