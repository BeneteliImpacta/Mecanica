using MecanicaBeneteli.WebApp.Extensions.Cookie;
using MecanicaBeneteli.WebApp.Extensions;
using System.Text.Json;
using MecanicaBeneteli.Business.Interfaces.Notificacoes;
using MecanicaBeneteli.Business.Interfaces.Repository;
using MecanicaBeneteli.Business.Notificacoes;
using MecanicaBeneteli.Business.Interfaces.Services;

using MecanicaBeneteli.Data.Core;
using MecanicaBeneteli.Data.Repository;


using System.Text.Json;

namespace MecanicaBeneteli.WebApp.Configurations
{
    public static class DependencyInjection
    {
        public static void ResolveDependencies(this IServiceCollection services,
                                               IConfiguration configuration,
                                               string environmentName)
        {

            #region Notificações

            services.AddScoped<INotificador, Notificador>();

            #endregion



            #region Cookie Autenticação

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ILeitorCookie, LeitorCookie>();
            services.AddScoped<IGeradorCookie, GeradorCookie>();

            #endregion

            #region Application Services

            services.AddScoped<MecanicaBeneteli.Business.Interfaces.Services.IUsuarioService, MecanicaBeneteli.Business.Services.UsuarioService>();
            services.AddScoped<MecanicaBeneteli.Business.Interfaces.Services.IEstoqueService, MecanicaBeneteli.Business.Services.EstoqueService>();



            #endregion


            #region Connection Strings

            //Se for ambiente de prod pega das variaveis de ambiente do windows, senão pega a connection string do appsettings
            ConnectionStringsDTO connectionStrings;

             connectionStrings = configuration.GetSection("ConnectionStrings").Get<ConnectionStringsDTO>();


            #endregion

            #region Repository

            services.AddScoped<MecanicaBeneteli.Business.Interfaces.Repository.IUsuarioRepository, MecanicaBeneteli.Data.Repository.UsuarioRepository>(s => new MecanicaBeneteli.Data.Repository.UsuarioRepository(connectionStrings));
            services.AddScoped<MecanicaBeneteli.Business.Interfaces.Repository.IEstoqueRepository, MecanicaBeneteli.Data.Repository.EstoqueRepository>(s => new MecanicaBeneteli.Data.Repository.EstoqueRepository(connectionStrings));

            #endregion

        }
    }
}
