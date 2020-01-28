using Conexia.Challenge.Services.Api.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Conexia.Challenge.Services.Api
{
    public class Startup
    {
        readonly string _validOrigins = "conexia-challenge";

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

            services.AddCors(options =>
            {
                options.AddPolicy(_validOrigins,
                builder =>
                {
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseSimpleInjectorConfig();
            app.UseCustomSwaggerConfig();
            app.UseRouting();
            app.UseCors(_validOrigins);
            app.UseAuthorization();
            app.UseStaticFiles();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
