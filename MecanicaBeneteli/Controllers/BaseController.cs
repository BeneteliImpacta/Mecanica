using MecanicaBeneteli.Business.Interfaces.Notificacoes;
using MecanicaBeneteli.Business.Interfaces.Services;
using MecanicaBeneteli.WebApp.Extensions.Cookie;
using Microsoft.AspNetCore.Mvc;

namespace MecanicaBeneteli.Controllers
{
    public class BaseController : Controller
    {
        private readonly IGeradorCookie _geradorCookie;
        private readonly IUsuarioService _usuarioService;
        private readonly ILeitorCookie _leitorCookie;
        private readonly INotificador _notificador;

        public BaseController(IGeradorCookie geradorCookie, IUsuarioService usuarioService, ILeitorCookie leitorCookie, INotificador notificador)
        {
            _geradorCookie = geradorCookie;
            _usuarioService = usuarioService;
            _leitorCookie = leitorCookie;
            _notificador = notificador;
        }

        protected BaseController(INotificador notificador)
        {
            _notificador = notificador;
        }

        protected bool TemErros()
        {
            return _notificador.TemNotificacao();
        }

        protected void AdicionaErrosModelState()
        {
            var notificacoes = _notificador.ObterNotificacoes();

            foreach (var notificacao in notificacoes)
                ModelState.AddModelError(string.Empty, notificacao.Mensagem);
        }
    }
}
