
using MecanicaBeneteli.WebApp.Extensions.Cookie;

namespace MecanicaBeneteli.WebApp.Extensions.Exceptions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;


        public ExceptionMiddleware(RequestDelegate next
                                   )
        {
            _next = next;

        }

        public async Task InvokeAsync(HttpContext httpContext, ILeitorCookie leitorCookie, IConfiguration configuration)
        {
            try
            {
                await _next(httpContext);
            }
            catch (ErroInternoApiException)
            {
                TratarErroInternoApi(httpContext);
            }


        }

        private void TratarErroInternoApi(HttpContext context)
        {
            RedirecionarPara(context, "erro/erro-interno");
        }


        private void RedirecionarPara(HttpContext context, string pagina)
        {
            string rotaAtual = context.Request.Path;
            string[] quantidadeNiveisRota = rotaAtual.Split("/");

            string rotaFinal = string.Empty;

            if (quantidadeNiveisRota.Length > 2)
            {
                for (int i = 0; i < quantidadeNiveisRota.Length - 2; i++)
                    rotaFinal += "../";

                rotaFinal += pagina;
            }
            else
                rotaFinal = "/" + pagina;

            context.Response.Redirect(rotaFinal);
        }
    }
}
