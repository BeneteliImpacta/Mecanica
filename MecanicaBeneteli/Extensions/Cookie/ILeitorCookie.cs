using System.Security.Claims;

namespace MecanicaBeneteli.WebApp.Extensions.Cookie
{
    public interface ILeitorCookie
    {
        string RetornarCodigoOrgao();
        string RetornarNomeOrgao();
        string RetornarEmpresa();
        HttpContext RetonarHttpContext();

    }

    public class LeitorCookie : ILeitorCookie
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;

        public LeitorCookie(IHttpContextAccessor accessor,
                                   IConfiguration configuration)
        {
            _httpContextAccessor = accessor;
            _configuration = configuration;
        }

        public string RetornarCodigoOrgao()
        {
            return _httpContextAccessor
                        .HttpContext
                        .Request
                        .Cookies["codigoOrgao"];
        }

        public string RetornarNomeOrgao()
        {
            return _httpContextAccessor
                        .HttpContext
                        .Request
                        .Cookies["nomeOrgao"];
        }

        public string RetornarEmpresa()
        {
            return _httpContextAccessor
                        .HttpContext
                        .Request
                        .Cookies["empresa"];
        }

        public HttpContext RetonarHttpContext()
        {
            return _httpContextAccessor.HttpContext;
        }

        private string RetornarCampoDentroDoTokenPorChave(string chave)
        {
            string valor = _httpContextAccessor.HttpContext.User.FindFirstValue(chave);

            if (valor == null)
                return string.Empty;

            return valor;
        }
    }
}
