using MecanicaBeneteli.Configurations;
using MecanicaBeneteli.WebApp.Extensions.Exceptions;

namespace MecanicaBeneteli.WebApp.Configurations
{
    public static class MvcConfig
    {
        public static IServiceCollection AddMvcConfiguration(this IServiceCollection services, string environmentName)
        {
            services.AddControllersWithViews();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsDevelopmentPolicy",
                    builder =>
                        builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });

            return services;
        }

        public static IApplicationBuilder UseMvcConfiguration(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseCors("CorsDevelopmentPolicy");
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
                app.UseHttpsRedirection();
                app.UseMiddleware<ExceptionMiddleware>();
            }

            app.Use(async (context, next) =>
            {
                await next();
                if (context.Response.StatusCode == 404)
                {
                    context.Request.Path = "/erro/pagina-nao-encontrada";
                    await next();
                }
            });

            app.UseStaticFiles();

            app.UseRouting();

            app.UseGlobalization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });


            return app;
        }
    }
}
