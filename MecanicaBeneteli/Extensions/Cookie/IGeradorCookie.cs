namespace MecanicaBeneteli.WebApp.Extensions.Cookie
{
    public interface IGeradorCookie
    {
        Task SalvarCookieCodigoOrgao(string codigoOrgao);

        Task SalvarCookieNomeOrgao(string nomeOrgao);

        Task SalvarCookieEmpresa(string empresa);
    }

    public class GeradorCookie : IGeradorCookie
    {
        IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;

        public GeradorCookie(IHttpContextAccessor httpContextAccessor,
                             IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        public async Task SalvarCookieCodigoOrgao(string codigoOrgao)
        {
            _httpContextAccessor.HttpContext.Response.Cookies.Append("codigoOrgao", codigoOrgao);
        }

        public async Task SalvarCookieNomeOrgao(string nomeOrgao)
        {
            _httpContextAccessor.HttpContext.Response.Cookies.Append("nomeOrgao", nomeOrgao);
        }

        public async Task SalvarCookieEmpresa(string empresa)
        {
            _httpContextAccessor.HttpContext.Response.Cookies.Append("empresa", empresa);
        }

    }
}
