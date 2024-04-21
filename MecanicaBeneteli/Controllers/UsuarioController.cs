using AutoMapper;
using MecanicaBeneteli.Business.Interfaces.Notificacoes;
using MecanicaBeneteli.Business.Interfaces.Services;
using MecanicaBeneteli.Business.Models;
using MecanicaBeneteli.ViewModel;
using MecanicaBeneteli.WebApp.Extensions.Cookie;
using Microsoft.AspNetCore.Mvc;

namespace MecanicaBeneteli.Controllers
{

    public class UsuarioController : BaseController
    {

        private readonly IUsuarioService _usuarioService;
        private readonly ILeitorCookie _leitorCookie;
        private readonly IMapper _mapper;
        public UsuarioController(IGeradorCookie geradorCookie,
                               IUsuarioService usuarioService,
                               ILeitorCookie leitorCookie,
                               IMapper mapper,
                               INotificador notificador)
            : base(geradorCookie, usuarioService, leitorCookie, notificador)
        {
            _usuarioService = usuarioService;
            _leitorCookie = leitorCookie;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> CadastrarInicio()
        {      

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarSucesso(UsuarioViewModel usuarioViewModel)
        {
            if (!ModelState.IsValid)
            {
                
                return View("CadastrarInicio", usuarioViewModel);
            }

            var usuario = await _usuarioService.IncluirUsuario(_mapper.Map<Usuario>(usuarioViewModel));

            ViewData["Sucesso"] = "Usuário cadastrado com sucesso!";

            return View("CadastrarInicio");
        }


    }
}
