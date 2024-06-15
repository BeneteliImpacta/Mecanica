using AutoMapper;
using MecanicaBeneteli.Business.Interfaces.Notificacoes;
using MecanicaBeneteli.Business.Interfaces.Services;
using MecanicaBeneteli.Business.Models;
using MecanicaBeneteli.ViewModel;
using MecanicaBeneteli.WebApp.Extensions.Cookie;
using Microsoft.AspNetCore.Mvc;

namespace MecanicaBeneteli.Controllers
{
    public class ManutencaoController : BaseController
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IEstoqueService _estoqueService;
        private readonly ILeitorCookie _leitorCookie;
        private readonly IMapper _mapper;
        private readonly IManutencaoService _manutencaoService;
        public ManutencaoController(IGeradorCookie geradorCookie,
                               IUsuarioService usuarioService,
                               IEstoqueService estoqueService,
                               ILeitorCookie leitorCookie,
                               IMapper mapper,
                               INotificador notificador,
                               IManutencaoService manutencaoService)
            : base(geradorCookie, usuarioService, leitorCookie, notificador)
        {
            _usuarioService = usuarioService;
            _estoqueService = estoqueService;
            _leitorCookie = leitorCookie;
            _mapper = mapper;
            _manutencaoService = manutencaoService;
        }
        [HttpGet]
        public async Task<IActionResult> ManutencaoInicio()
        {
            var manutencaoViewModel = new ManutencaoViewModel();
            var pecas = await _estoqueService.ConsultarPecas();
            var pecasViewModel = _mapper.Map<List<PecaViewModel>>(pecas);
            manutencaoViewModel.PecaViewModel = pecasViewModel;
            return View("ManutencaoInicio", manutencaoViewModel);

        }

        [HttpPost]
        public async Task<IActionResult> ManutencaoInicio(ManutencaoViewModel manutencaoViewModel)
        {
            var manuetencao = await _manutencaoService.CadastrarManutencao(_mapper.Map<Manutencao>(manutencaoViewModel));

            var manutencaoViewModell = new ManutencaoViewModel();
            var pecas = await _estoqueService.ConsultarPecas();
            var pecasViewModel = _mapper.Map<List<PecaViewModel>>(pecas);
            manutencaoViewModell.PecaViewModel = pecasViewModel;

            ViewData["Sucesso"] = "Manutenção feita com sucesso!";

            return View("ManutencaoInicio", manutencaoViewModell);

        }
    }
}
