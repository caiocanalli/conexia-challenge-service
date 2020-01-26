using Conexia.Challenge.Services.Api.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Conexia.Challenge.Services.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSimpleInjectorConfig();
            services.AddSwaggerConfig();
            services.AddMassTransitConfig(Configuration);
            services.AddAuthenticationConfig(Configuration);
            services.AddAuthorizationConfig();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseSimpleInjectorConfig();
            app.UseCustomSwaggerConfig();
            app.UseRouting();
            app.UseAuthorization();
            app.UseStaticFiles();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseCors(option =>
                option.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod());
        }
    }
}
