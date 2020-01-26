using Conexia.Challenge.Services.Api.Handlers;
using Conexia.Challenge.Services.Api.Infrastructure;
using GreenPipes;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Conexia.Challenge.Services.Api.Extensions
{
    public static class MassTransitExtension
    {
        public static void AddMassTransitConfig(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddMassTransit(x =>
            {
                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    var uri = new Uri(configuration["RabbitMQ:Hostname"]);

                    var host = cfg.Host(uri, hostConfigurator =>
                    {
                        hostConfigurator.Username(configuration["RabbitMQ:Username"]);
                        hostConfigurator.Password(configuration["RabbitMQ:Password"]);
                    });

                    cfg.ReceiveEndpoint(configuration["Queues:EvaluateDocument"], e =>
                    {
                        e.UseMessageRetry(r =>
                        {
                            r.Interval(10, TimeSpan.FromMilliseconds(200));
                        });

                        e.Consumer<EvaluateDocumentConsumer>();
                    });
                }));
            });

            services.AddSingleton<IHostedService, BusService>();
        }
    }
}
