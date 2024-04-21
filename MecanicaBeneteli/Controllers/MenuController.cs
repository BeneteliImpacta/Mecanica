using AutoMapper;
using MecanicaBeneteli.Business.Interfaces.Notificacoes;
using MecanicaBeneteli.Business.Interfaces.Services;
using MecanicaBeneteli.Business.Models;
using MecanicaBeneteli.ViewModel;
using MecanicaBeneteli.WebApp.Extensions.Cookie;
using Microsoft.AspNetCore.Mvc;

namespace MecanicaBeneteli.Controllers
{
    public class MenuController : BaseController
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IEstoqueService _estoqueService;
        private readonly ILeitorCookie _leitorCookie;
        private readonly IMapper _mapper;
        public MenuController(IGeradorCookie geradorCookie,
                               IUsuarioService usuarioService,
                               IEstoqueService estoqueService,
                               ILeitorCookie leitorCookie,
                               IMapper mapper,
                               INotificador notificador)
            : base(geradorCookie, usuarioService, leitorCookie, notificador)
        {
            _usuarioService = usuarioService;
            _estoqueService = estoqueService;
            _leitorCookie = leitorCookie;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> LoginSucesso(UsuarioViewModel usuarioViewModel)
        {

            try
            {
                var usuario = await _usuarioService.ValidarUsuario(_mapper.Map<Usuario>(usuarioViewModel));
                var dadosUsuario = _mapper.Map<UsuarioViewModel>(usuario);
                // Lógica para quando o usuário é validado com sucesso
                return View("MenuInicio", dadosUsuario);
            }
            catch (Exception ex)
            {
                TempData["Erro"] = ex.Message;
                return RedirectToAction("Index", "Home"); // Ou qualquer outra ação que você queira redirecionar
            }

        }

        [HttpGet]
        public async Task<IActionResult> GerenciarPecas()
        {
            var pecas = await _estoqueService.ConsultarPecas();
            var pecasViewModel = _mapper.Map<List<PecaViewModel>>(pecas);
            return View("CadastrarEstoqueInicio", pecasViewModel);

        }

        [HttpGet]
        public async Task<IActionResult> CadastrarPecasInicio()
        {
            return View("CadastrarPecaInicio");

        }

        [HttpPost]
        public async Task<IActionResult> CadastrarPeca(PecaViewModel pecaViewModel)
        {
            var peca = await _estoqueService.IncluirPeca(_mapper.Map<Peca>(pecaViewModel));

            ViewData["Sucesso"] = "Peça cadastrada com sucesso!";

            return View("CadastrarPecaInicio");


        }
    }
}
